using AutoMapper;
using FastEndpoints;
using Paslauga.Data;
using Paslauga.Entities;

namespace Paslauga.Features.vDC
{

    public class UpdateVDCRequest
    {
        public int Id { get; set; }
        public string? Name { get; set; }
        public int? OrganisationId { get; set; }
        public int? VCPUMax { get; set; }
        public int? VCPUAllocated { get; set; }
        public int? VMemoryMax { get; set; }
        public int? VMemoryAllocated { get; set; }
        public int? VStorageMax { get; set; }
        public int? VStorageUsed { get; set; }
        public int? NetworkPoolId { get; set; }
    }

    public class UpdateVDC : Endpoint<UpdateVDCRequest>
    {
        private readonly CloudDbContext _context;
        private readonly AutoMapper.IMapper _mapper;

        public UpdateVDC(CloudDbContext context, AutoMapper.IMapper mapper)
        {
            _context = context;
            _mapper = mapper;
        }

        public override void Configure()
        {
            Put("/update/vdc");
            AllowAnonymous();
        }

        public override async Task HandleAsync(UpdateVDCRequest req, CancellationToken ct)
        {
            var vdc = await _context.Set<VDC>().FindAsync(new object[] { req.Id }, ct);

            if (vdc == null)
            {
                await SendNotFoundAsync(ct);
                return;
            }

            _mapper.Map(req, vdc);

            await _context.SaveChangesAsync(ct);

            await SendAsync(new
            {
                Message = "Atnaujintas."
            }, cancellation: ct);
        }
    }
}

