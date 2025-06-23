using FastEndpoints;
using Microsoft.EntityFrameworkCore;
using Paslauga.Data;
using Paslauga.Entities;
using Paslauga.Helpers;

namespace Paslauga.Features.VM
{

    public class AddVMRequest
    {

        public string Name { get; set; }
        public string? Description { get; set; }

        public int VDCId { get; set; }

        public int VLANId { get; set; }
        public string OSName { get; set; }
        public int AllocatedRAM { get; set; }
        public int AllocatedVCPU { get; set; }
        public int AllocatedStorage { get; set; }
    }
    public class AddVM : Endpoint<AddVMRequest>
    {

        private readonly CloudDbContext _context;

        public AddVM(CloudDbContext context)
        {
            _context = context;
        }

        public override void Configure()
        {
            Post("/add/vm"); 
            AllowAnonymous();          
        }

        public override async Task HandleAsync(AddVMRequest req, CancellationToken ct)
        {
            if (string.IsNullOrWhiteSpace(req.Name))
            {
                ThrowError("Nera vardo.");
            }

            var vm = new Entities.VM
            {
                Name = req.Name,
                Description = req.Description,
                VDCId = req.VDCId,
                VDC = await _context.Set<VDC>().FindAsync(new object[] { req.VDCId }, ct),
                VLANId = req.VLANId,
                OSName = req.OSName,
                AllocatedIP = await AllocateIP(await _context.Set<VLAN>().FindAsync(new object[] { req.VLANId }, ct), ct),
                AllocatedRAM = req.AllocatedRAM,
                AllocatedVCPU = req.AllocatedVCPU,
                AllocatedStorage = req.AllocatedStorage,
            };

            if (vm.AllocatedIP == "unassigned") return;

            if (vm.VDC != null)
            {

                int? AvailableRAM = VDCHardware.LeftAvailable(vm.VDC, hT: HardwareType.RAM);
                int? AvailableCPU = VDCHardware.LeftAvailable(vm.VDC, hT: HardwareType.CPU);
                int? AvailableHDD = VDCHardware.LeftAvailable(vm.VDC, hT: HardwareType.HDD);

                if (vm.AllocatedRAM > AvailableRAM)
                {
                    await SendAsync(new
                    {
                        Message = $"Bandoma priskirti daugiau RAM nei yra leistina. Maks. {vm.VDC.VMemoryMax} GB / Laisva {AvailableRAM} GB"
                    }, 405, cancellation: ct);

                    return;
                }

                if (vm.AllocatedVCPU > AvailableCPU)
                {
                    await SendAsync(new
                    {
                        Message = $"Bandoma priskirti daugiau nei yra leistina. Maks. {vm.VDC.VCPUMax} vCPU / Laisva {AvailableCPU} vCPU"
                    }, 405, cancellation: ct);
                    return;
                }

                if (vm.AllocatedStorage > AvailableHDD)
                {
                    await SendAsync(new
                    {
                        Message = $"Bandoma priskirti daugiau talpos nei yra leistina. Maks. {vm.VDC.VStorageMax} GB / Laisva {AvailableHDD} GB"
                    }, 405, cancellation: ct);

                    return;
                }
            } else
            {
                await SendAsync(new
                {
                    Message = "Nera VDC"
                }, 404, ct);

                return;
            }

            AvailableIPs ip = await _context.Set<AvailableIPs>().FirstOrDefaultAsync(o => o.IPAddress == vm.AllocatedIP, ct);
            ip.Status = "Uzimtas";

            _context.Set<AvailableIPs>().Attach(ip);
            _context.Entry(ip).State = EntityState.Modified;

            VDC vDC = vm.VDC;

            vDC.VStorageUsed += vm.AllocatedStorage;
            vDC.VCPUAllocated += vm.AllocatedVCPU;
            vDC.VMemoryAllocated += vm.AllocatedRAM;

            _context.Set<VDC>().Attach(vDC);
            _context.Entry(vDC).State = EntityState.Modified;

            await _context.Set<Entities.VM>().AddAsync(vm, ct);
            await _context.SaveChangesAsync(ct);
            

            await SendAsync(new
            {
                Message = "Sukurta.",
                vm.Id,
                vm.Name,
                vm.Description,
                vm.AllocatedIP,
                vm.AllocatedRAM,
                vm.AllocatedStorage,
                vm.AllocatedVCPU
            }, cancellation: ct);
        }

        private async Task<string> AllocateIP(VLAN vLAN, CancellationToken ct)
        {

            if (vLAN == null)
            {
                await SendAsync(new
                {
                    Message = "Nera VLAN"
                }, 404, ct);

                return "unassigned";
            }

            AvailableIPs allocatedIP = await _context.Set<AvailableIPs>().FirstOrDefaultAsync(o => o.VLANId == vLAN.Id && o.Status == "Laisvas", ct);
            if (allocatedIP != null) return allocatedIP.IPAddress;
            return "unassigned";
        }
    }
}
 
