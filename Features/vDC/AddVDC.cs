using FastEndpoints;
using Paslauga.Data;
using Paslauga.Entities;
using static FastEndpoints.Ep;

namespace Paslauga.Features.vDC
{

    public class AddVDCRequest
    {
        public string Name { get; set; }  
        public string? Description { get; set; }  
        public int? OrganisationId { get; set; }
        public int? VCPUMax { get; set; }
        public int? VCPUAllocated { get; set; }
        public int? VMemoryMax { get; set; }
        public int? VMemoryAllocated { get; set; }
        public int? VStorageMax { get; set; }
        public int? VStorageUsed { get; set; }
        public int? NetworkPoolId { get; set; }
    }
    public class AddVDC : Endpoint<AddVDCRequest>
    {
        private readonly CloudDbContext _context;
        public override void Configure()
        {
            Post("/add/vdc");
            AllowAnonymous();
        }

        public override async Task HandleAsync(AddVDCRequest req, CancellationToken ct)
        {

            var vdc = new VDC
            {
                Name = req.Name,
                OrganisationId = req.OrganisationId,

            };

            // Add to the database
            await _context.Set<VDC>().AddAsync(organisation, ct);
            await _context.SaveChangesAsync(ct);

            await SendAsync(new
            {
                Message = "Sukurta.",
                Organisation = organisation
            }, cancellation: ct);
        }

    }
    }
}
