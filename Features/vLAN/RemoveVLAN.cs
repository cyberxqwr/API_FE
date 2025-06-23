using Microsoft.EntityFrameworkCore;
using FastEndpoints;
using Paslauga.Data;
using Paslauga.Entities;

namespace Paslauga.Features.vLAN
{
    public class RemoveVLANRequest
    {
        public int Id { get; set; }

    }

    public class RemoveVLAN : Endpoint<RemoveVLANRequest>
    {
        private readonly CloudDbContext _context;

        public RemoveVLAN(CloudDbContext context)
        {
            _context = context;
        }
        public override void Configure()
        {
            Delete("/remove/vlan");
            AllowAnonymous();
        }

        public override async Task HandleAsync(RemoveVLANRequest req, CancellationToken ct)
        {

            var vlan = await _context.Set<VLAN>().FindAsync(new object[] { req.Id }, ct);

            if (vlan == null)
            {
                await SendNotFoundAsync(ct);
                return;
            }

            var associatedIPsList = await _context.Set<AvailableIPs>()
                .Where(ip => ip.VLANId == vlan.Id)
                .Select(ip => ip.IPAddress)
                .ToListAsync(ct);

            var vmUsingIP = await _context.Set<Entities.VM>()
                .FirstOrDefaultAsync(vm => associatedIPsList.Contains(vm.AllocatedIP), ct);

            if (vmUsingIP != null)
            {
                await SendAsync(new
                {
                    Message = $"Negalima istrinti. VM '{vmUsingIP.Name}' naudoja si IP."
                }, 400, ct);

                return;
            }

            _context.Set<AvailableIPs>().RemoveRange(
                _context.Set<AvailableIPs>().Where(ip => ip.VLANId == vlan.Id)
            );
            _context.Set<VLAN>().Remove(vlan);
            await _context.SaveChangesAsync(ct);

            await SendAsync(new
            {
                Message = "Istrintas."
            }, cancellation: ct);
        }
    }
}
 
