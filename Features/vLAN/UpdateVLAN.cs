using FastEndpoints;
using Paslauga.Data;
using Paslauga.Entities;

namespace Paslauga.Features.vLAN
{

    public class UpdateVLANRequest
    {
        public int Id { get; set; }
        public string? Name { get; set; }
        public string? Description { get; set; }
    }
    public class UpdateVLAN : Endpoint<UpdateVLANRequest>
    {

        private readonly CloudDbContext _context;
        private readonly AutoMapper.IMapper _mapper;

        public UpdateVLAN(CloudDbContext context, AutoMapper.IMapper mapper)
        {
            _context = context;
            _mapper = mapper;
        }

        public override void Configure()
        {
            Put("/update/vlan");
            AllowAnonymous();
        }

        public override async Task HandleAsync(UpdateVLANRequest req, CancellationToken ct)
        {
            var vlan = await _context.Set<VLAN>().FindAsync(new object[] { req.Id }, ct);

            if (vlan == null)
            {
                await SendNotFoundAsync(ct);
                return;
            }

            _mapper.Map(req, vlan);

            await _context.SaveChangesAsync(ct);

            await SendAsync(new
            {
                Message = "Atnaujintas."

            }, cancellation: ct);
        }


    }
}
