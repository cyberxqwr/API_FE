using FastEndpoints;
using Paslauga.Data;

namespace Paslauga.Features.Organisation
{

    public class UpdateOrganisationRequest
    {
        public int Id { get; set; }
        public string? Name { get; set; }
        public string? Description { get; set; }
    }
    public class UpdateOrganisation : Endpoint<UpdateOrganisationRequest>
    {

        private readonly CloudDbContext _context;
        private readonly AutoMapper.IMapper _mapper;

        public UpdateOrganisation(CloudDbContext context, AutoMapper.IMapper mapper)
        {
            _context = context;
            _mapper = mapper;
        }

        public override void Configure()
        {
            Put("/update/organisation");
            AllowAnonymous();
        }

        public override async Task HandleAsync(UpdateOrganisationRequest req, CancellationToken ct)
        {
            var org = await _context.Set<Entities.Organisation>().FindAsync(new object[] { req.Id }, ct);

            if (org == null)
            {
                await SendNotFoundAsync(ct);
                return;
            }

            _mapper.Map(req, org);

            await _context.SaveChangesAsync(ct);

            await SendAsync(new
            {
                Message = "Atnaujintas."

            }, cancellation: ct);
        }


    }
}
