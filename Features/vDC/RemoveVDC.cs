using FastEndpoints;
using Paslauga.Data;
using Paslauga.Entities;

namespace Paslauga.Features.vDC
{
    public class RemoveVDCRequest
    {
        public int Id { get; set; }

    }
    public class RemoveVDC : Endpoint<RemoveVDCRequest>
    {
        private readonly CloudDbContext _context;

        public RemoveVDC(CloudDbContext context)
        {
            _context = context;
        }
        public override void Configure()
        {
            Delete("/remove/vdc");
            AllowAnonymous();
        }

        public override async Task HandleAsync(RemoveVDCRequest req, CancellationToken ct)
        {

            var vdc = await _context.Set<VDC>().FindAsync(new object[] { req.Id }, ct);

            if (vdc == null)
            {
                await SendNotFoundAsync(ct);
                return;
            }

            _context.Set<VDC>().Remove(vdc);
            await _context.SaveChangesAsync(ct);

            await SendAsync(new
            {
                Message = "Istrintas."
            }, cancellation: ct);
        }
    }
}
