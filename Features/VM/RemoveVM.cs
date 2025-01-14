using FastEndpoints;
using Paslauga.Data;
using Paslauga.Entities;
using Microsoft.EntityFrameworkCore;

namespace Paslauga.Features.VM
{

    public class RemoveVMRequest
    {

        public int Id { get; set; }

    }

    public class RemoveVM : Endpoint<RemoveVMRequest>
    {
        private readonly CloudDbContext _context;

        public RemoveVM(CloudDbContext context)
        {
            _context = context;
        }
        public override void Configure()
        {
            Delete("/remove/vm");
            AllowAnonymous();
        }

        public override async Task HandleAsync(RemoveVMRequest req, CancellationToken ct)
        {

            var vm = await _context.Set<Entities.VM>().FindAsync(new object[] { req.Id }, ct);

            if (vm == null)
            {
                await SendNotFoundAsync(ct);
                return;
            }

            AvailableIPs ip = await _context.Set<AvailableIPs>().FirstOrDefaultAsync(o => o.IPAddress == vm.AllocatedIP, ct);

            if (ip != null)
            {

                ip.Status = "Laisvas";
                _context.Set<AvailableIPs>().Attach(ip);
                _context.Entry(ip).State = EntityState.Modified;
            }

            VDC vDC = await _context.Set<VDC>().FindAsync(new object[] { vm.VDCId }, ct);

            if (vDC != null)
            {
                vDC.VCPUAllocated -= vm.AllocatedVCPU;
                vDC.VMemoryAllocated -= vm.AllocatedRAM;
                vDC.VStorageUsed -= vm.AllocatedStorage;
                _context.Set<VDC>().Attach(vDC);
                _context.Entry(vDC).State = EntityState.Modified;
            }

            

            _context.Set<Entities.VM>().Remove(vm);
            await _context.SaveChangesAsync(ct);

            await SendAsync(new
            {
                Message = "Istrintas."
            }, cancellation: ct);
        }
    }
}
