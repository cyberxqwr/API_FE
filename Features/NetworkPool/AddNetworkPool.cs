using FastEndpoints;
using Paslauga.Data;

namespace Paslauga.Features.NetworkPool
{

    public class AddNetworkPoolRequest
    {
        public string Name { get; set; }  
        public string? Description { get; set; }
        public string? Status { get; set; }
    }
    public class AddNetworkPool : Endpoint<AddNetworkPoolRequest>
    {

        private readonly CloudDbContext _context;

        public AddNetworkPool(CloudDbContext context)
        {
            _context = context;
        }

        public override void Configure()
        {
            Post("/add/networkpool"); 
            AllowAnonymous();          
        }

        public override async Task HandleAsync(AddNetworkPoolRequest req, CancellationToken ct)
        {
            if (string.IsNullOrWhiteSpace(req.Name))
            {
                ThrowError("Nera vardo.");
            }

            var npool = new Entities.NetworkPool
            {
                Name = req.Name,
                Description = req.Description,
                Status = req.Status
            };

            await _context.Set<Entities.NetworkPool>().AddAsync(npool, ct);
            await _context.SaveChangesAsync(ct);

            await SendAsync(new
            {
                Message = "Sukurta.",
                NetworkPool = npool
            }, cancellation: ct);
        }
    }
}
