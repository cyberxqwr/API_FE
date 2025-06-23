using FastEndpoints;
using Microsoft.EntityFrameworkCore;
using Paslauga.Data;
using Paslauga.Entities;

namespace Paslauga.Features.vDC
{
    public class ListVDCs : EndpointWithoutRequest
    {

        private readonly CloudDbContext _context;

        public ListVDCs(CloudDbContext context)
        {
            _context = context;
        }
        public override void Configure()
        {
            Get("/list/vdcs");
            AllowAnonymous();
        }

        public override async Task HandleAsync(CancellationToken ct)
        {
            if (_context?.VDC == null)
            {
                await SendAsync("Tuscia", 500, ct);
                return;
            }

            var list = await _context.Set<VDC>().ToListAsync(ct);
            await SendAsync(list, 200, ct);

        }
    }
}
 
