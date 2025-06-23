using FastEndpoints;
using Paslauga.Data;
using Paslauga.Entities;
using Microsoft.EntityFrameworkCore;
using System.Text.Json.Serialization;

namespace Paslauga.Features.Organisation
{

    public class RemoveOrganisationRequest
    {

        [JsonPropertyName("id")]
        public int Id { get; set; }

    }

    public class RemoveOrganisation : Endpoint<RemoveOrganisationRequest>
    {
        private readonly CloudDbContext _context;

        public RemoveOrganisation(CloudDbContext context)
        {
            _context = context;
        }
        public override void Configure()
        {
            Delete("/remove/organisation");
            AllowAnonymous();
        }

        public override async Task HandleAsync(RemoveOrganisationRequest req, CancellationToken ct)
        {

            var org = await _context.Set<Entities.Organisation>().FindAsync(new object[] { req.Id }, ct);

            if (org == null)
            {
                await SendNotFoundAsync(ct);
                return;
            }

            _context.Set<Entities.Organisation>().Remove(org);
            await _context.SaveChangesAsync(ct);

            await SendAsync(new
            {
                Message = "Istrintas."
            }, cancellation: ct);
        }
    }
}
 
