using FastEndpoints;
using Microsoft.EntityFrameworkCore;
using Paslauga.Data;
using Paslauga.Entities;

namespace Paslauga.Features.IPs
{

    public class ListIP : EndpointWithoutRequest
    {

        private readonly CloudDbContext _context;

        public ListIP(CloudDbContext context)
        {
            _context = context;
        }

        public override void Configure()
        {
            Get("/list/ip");
            AllowAnonymous();
        }

        public override async Task HandleAsync(CancellationToken ct)
        {
            if (_context?.AvailableIPs == null)
            {
                await SendAsync("Tuscia", 404, ct);
                return;
            }

            var list = await _context.Set<AvailableIPs>().ToListAsync(ct);
            await SendAsync(list, 200, ct);

        }
    }
}
