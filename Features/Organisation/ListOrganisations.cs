using FastEndpoints;
using Microsoft.EntityFrameworkCore;
using Paslauga.Data;
using Paslauga.Entities;

namespace Paslauga.Features.Organisation
{
    public class ListOrganisation : EndpointWithoutRequest
    {

        private readonly CloudDbContext _context;

        public ListOrganisation(CloudDbContext context)
        {
            _context = context;
        }
        public override void Configure()
        {
            Get("/list/organisations");
            AllowAnonymous();
        }

        public override async Task HandleAsync(CancellationToken ct)
        {
            if (_context?.Organisation == null)
            {
                await SendAsync("Tuscia", 404, ct);
                return;
            }

            var list = await _context.Set<Entities.Organisation>().ToListAsync(ct);
            await SendAsync(list, 200, ct);

        }
    }
}
