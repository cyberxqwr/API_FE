using FastEndpoints;
using Microsoft.EntityFrameworkCore;
using Paslauga.Data;
using Paslauga.Entities;
using Paslauga.Helpers;

namespace Paslauga.Features.VM
{

    public class UpdateVMRequest
    {
        public int Id { get; set; }
        public string? Name { get; set; }
        public string? Description { get; set; }
        public int? VLANId { get; set; }
        public int? VDCId { get; set; }
        public string? OSName { get; set; }
        public string? AllocatedIP { get; set; }
        public int? AllocatedRAM { get; set; }
        public int? AllocatedVCPU { get; set; }
        public int? AllocatedStorage { get; set; }
    }
    public class UpdateVM : Endpoint<UpdateVMRequest>
    {

        private readonly CloudDbContext _context;
        private readonly AutoMapper.IMapper _mapper;

        public UpdateVM(CloudDbContext context, AutoMapper.IMapper mapper)
        {
            _context = context;
            _mapper = mapper;
        }

        public override void Configure()
        {
            Put("/update/vm");
            AllowAnonymous();
        }

        public override async Task HandleAsync(UpdateVMRequest req, CancellationToken ct)
        {
            var vm = await _context.Set<Entities.VM>().FindAsync(new object[] { req.Id }, ct);

            if (vm == null)
            {
                await SendNotFoundAsync(ct);
                return;
            }

            VDC vDC = await _context.Set<VDC>().FindAsync(new object[] { vm.VDCId }, ct);

            int? AvailableRAM = VDCHardware.LeftAvailable(vDC, hT: HardwareType.RAM);
            int? AvailableCPU = VDCHardware.LeftAvailable(vDC, hT: HardwareType.CPU);
            int? AvailableHDD = VDCHardware.LeftAvailable(vDC, hT: HardwareType.HDD);

            if (vm.AllocatedRAM > AvailableRAM)
            {
                await SendAsync(new
                {
                    Message = $"Bandoma priskirti daugiau RAM nei yra leistina. Maks. {vDC.VMemoryMax} GB / Laisva {AvailableRAM} GB"
                }, 405, cancellation: ct);

                return;
            }

            if (vm.AllocatedVCPU > AvailableCPU)
            {
                await SendAsync(new
                {
                    Message = $"Bandoma priskirti daugiau nei yra leistina. Maks. {vDC.VCPUMax} vCPU / Laisva {AvailableCPU} vCPU"
                }, 405, cancellation: ct);
                return;
            }

            if (vm.AllocatedStorage > AvailableHDD)
            {
                await SendAsync(new
                {
                    Message = $"Bandoma priskirti daugiau talpos nei yra leistina. Maks. {vDC.VStorageMax} GB / Laisva {AvailableHDD} GB"
                }, 405, cancellation: ct);

                return;
            }

            _context.Set<VDC>().Attach(vDC);
            _context.Entry(vDC).State = EntityState.Modified;

            _mapper.Map(req, vm);

            await _context.SaveChangesAsync(ct);

            await SendAsync(new
            {
                Message = "Atnaujintas."

            }, cancellation: ct);
        }


    }
}
