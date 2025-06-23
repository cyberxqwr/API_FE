using FastEndpoints;
using Microsoft.EntityFrameworkCore;
using Paslauga.Data;
using Paslauga.Entities;

namespace Paslauga.Features.vLAN
{
    public class ListVLAN : EndpointWithoutRequest
    {

        private readonly CloudDbContext _context;

        public ListVLAN(CloudDbContext context)
        {
            _context = context;
        }
        public override void Configure()
        {
            Get("/list/vlan");
            AllowAnonymous();
        }

        public override async Task HandleAsync(CancellationToken ct)
        {
            if (_context?.VLAN == null)
            {
                await SendAsync("Tuscia", 404, ct);
                return;
            }

            var list = await _context.Set<VLAN>().ToListAsync(ct);
            await SendAsync(list, 200, ct);

        }
    }
}
 
