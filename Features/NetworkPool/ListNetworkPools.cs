using FastEndpoints;
using Microsoft.EntityFrameworkCore;
using Paslauga.Data;

namespace Paslauga.Features.NetworkPool
{
    public class ListNetworkPools : EndpointWithoutRequest
    {

        private readonly CloudDbContext _context;

        public ListNetworkPools(CloudDbContext context)
        {
            _context = context;
        }
        public override void Configure()
        {
            Get("/list/networkpools");
            AllowAnonymous();
        }

        public override async Task HandleAsync(CancellationToken ct)
        {
            if (_context?.NetworkPool == null)
            {
                await SendAsync("Tuscia", 404, ct);
                return;
            }

            var list = await _context.Set<Entities.NetworkPool>().ToListAsync();
            await SendAsync(list, 200, ct);

        }
    }
} 
