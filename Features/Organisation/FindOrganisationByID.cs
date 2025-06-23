using FastEndpoints;
using Microsoft.EntityFrameworkCore;
using Paslauga.Data;
using Paslauga.Entities;

namespace Paslauga.Features.Organisation
{

    public class FindOrganisationByIDRequest
    {
        public int? Id { get; set; }
        public class FindOrganisationByID : Endpoint<FindOrganisationByIDRequest>
        {

            private readonly CloudDbContext _context;

            public FindOrganisationByID(CloudDbContext context)
            {
                _context = context;
            }
            public override void Configure()
            {
                Get("/find/organisation/{id}");
                AllowAnonymous();
            }

            public override async Task HandleAsync(FindOrganisationByIDRequest req, CancellationToken ct)
            {

                var org = await _context.Set<Entities.Organisation>().FindAsync(new object[] { req.Id }, ct);

                if (org == null)
                {

                    await SendAsync(new
                    {
                        Message = "NoN"
                    }, 404, cancellation: ct);

                    return;
                }

                await SendAsync(org, 200, ct);

            }
        }
    }
}
 
