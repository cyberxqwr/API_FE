using FastEndpoints;
using Paslauga.Data;

namespace Paslauga.Features.NetworkPool
{

    public class UpdateNetworkPoolRequest
    {
        public int Id { get; set; }
        public string? Name { get; set; }
        public string? Description { get; set; }
        public string? Status { get; set; }
    }
    public class UpdateNetworkPool : Endpoint<UpdateNetworkPoolRequest>
    {

        private readonly CloudDbContext _context;
        private readonly AutoMapper.IMapper _mapper;

        public UpdateNetworkPool(CloudDbContext context, AutoMapper.IMapper mapper)
        {
            _context = context;
            _mapper = mapper;
        }

        public override void Configure()
        {
            Put("/update/networkpool");
            AllowAnonymous();
        }

        public override async Task HandleAsync(UpdateNetworkPoolRequest req, CancellationToken ct)
        {
            var npool = await _context.Set<Entities.NetworkPool>().FindAsync(new object[] { req.Id }, ct);

            if (npool == null)
            {
                await SendNotFoundAsync(ct);
                return;
            }

            _mapper.Map(req, npool);

            await _context.SaveChangesAsync(ct);

            await SendAsync(new
            {
                Message = "Atnaujintas."

            }, cancellation: ct);
        }


    }
}
