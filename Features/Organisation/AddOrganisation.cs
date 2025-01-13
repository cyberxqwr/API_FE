using FastEndpoints;
using Paslauga.Data;
using Paslauga.Entities;

namespace Paslauga.Features.Organisation
{

    public class AddOrganisationRequest
    {
        public string Name { get; set; }  
        public string Description { get; set; }  
    }
    public class AddOrganisation : Endpoint<AddOrganisationRequest>
    {

        private readonly CloudDbContext _context;

        public AddOrganisation(CloudDbContext context)
        {
            _context = context;
        }

        public override void Configure()
        {
            Post("/add/organisation"); 
            AllowAnonymous();          
        }

        public override async Task HandleAsync(AddOrganisationRequest req, CancellationToken ct)
        {
            if (string.IsNullOrWhiteSpace(req.Name))
            {
                ThrowError("Organisation Name is required.");
            }

            var organisation = new Paslauga.Entities.Organisation
            {
                Name = req.Name,
                Description = req.Description
            };

            // Add to the database
            await _context.Set<Paslauga.Entities.Organisation>().AddAsync(organisation, ct);
            await _context.SaveChangesAsync(ct);

            await SendAsync(new
            {
                Message = "Organisation created successfully.",
                Organisation = organisation
            }, cancellation: ct);
        }
    }
}
