using FastEndpoints;
using Paslauga.Data;

namespace Paslauga.Features.NetworkPool
{

    public class RemoveNetworkPoolRequest
    {
        public int Id { get; set; }

    }
    public class RemoveNetworkPool : Endpoint<RemoveNetworkPoolRequest>
    {
        private readonly CloudDbContext _context;

        public RemoveNetworkPool(CloudDbContext context)
        {
            _context = context;
        }
        public override void Configure()
        {
            Delete("/remove/networkpool");
            AllowAnonymous();
        }

        public override async Task HandleAsync(RemoveNetworkPoolRequest req, CancellationToken ct)
        {

            var org = await _context.Set<Entities.NetworkPool>().FindAsync(new object[] { req.Id }, ct);

            if (org == null)
            {
                await SendNotFoundAsync(ct);
                return;
            }

            _context.Set<Entities.NetworkPool>().Remove(org);
            await _context.SaveChangesAsync(ct);

            await SendAsync(new
            {
                Message = "Istrintas."
            }, cancellation: ct);
        }
    }
}

 
