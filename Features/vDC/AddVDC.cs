using FastEndpoints;
using Paslauga.Data;
using Paslauga.Entities;
using static FastEndpoints.Ep;

namespace Paslauga.Features.vDC
{

    public class AddVDCRequest
    {
        public string Name { get; set; }  
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

        public AddVDC(CloudDbContext context)
        {
            _context = context;
        }
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
                VCPUMax = req.VCPUMax,
                VCPUAllocated = req.VCPUAllocated,
                VMemoryMax = req.VMemoryMax,
                VMemoryAllocated = req.VMemoryAllocated,
                VStorageMax = req.VStorageMax,
                VStorageUsed = req.VStorageUsed

            };

            await _context.Set<VDC>().AddAsync(vdc, ct);
            await _context.SaveChangesAsync(ct);

            await SendAsync(new
            {
                Message = "Sukurta.",
                VDC = vdc
            }, cancellation: ct);
        }

    }
    
}
