using FastEndpoints;
using Microsoft.EntityFrameworkCore;
using Paslauga.Data;
using Paslauga.Entities;

namespace Paslauga.Features.VM
{
    public class ListVM : EndpointWithoutRequest
    {

        private readonly CloudDbContext _context;

        public ListVM(CloudDbContext context)
        {
            _context = context;
        }
        public override void Configure()
        {
            Get("/list/vm");
            AllowAnonymous();
        }

        public override async Task HandleAsync(CancellationToken ct)
        {
            if (_context?.VM == null)
            {
                await SendAsync("Tuscia", 404, ct);
                return;
            }

            var list = await _context.Set<Entities.VM>().ToListAsync(ct);
            await SendAsync(list, 200, ct);

        }
    }
}
