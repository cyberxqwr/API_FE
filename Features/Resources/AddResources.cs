using FastEndpoints;
using Paslauga.Data;
using Paslauga.Entities;
using Paslauga.Features.Organisation;
using Paslauga.Helpers;

namespace Paslauga.Features.Resources
{

    public class AddResourcesRequest
    {
        public int ProviderId { get; set; }
        public HardwareType HardwareType { get; set; }
        public int HardwareId { get; set; }

    }
    public class AddResources : Endpoint<AddResourcesRequest>
    {

        private readonly CloudDbContext _context;

        public AddResources(CloudDbContext context)
        {
            _context = context;
        }

        public override void Configure()
        {
            Post("/add/resource/pid={ProviderId}&ht={HardwareType}&hid={HardwareId}");
            AllowAnonymous();
        }

        public override async Task HandleAsync(AddResourcesRequest req, CancellationToken ct)
        {

            var resource = new ProviderResources
            {
                ProviderId = req.ProviderId,
                HardwareType = req.HardwareType,
                HardwareId = req.HardwareId
            };

            switch (req.HardwareType)
            {
                case HardwareType.HDD:
                    await _context.Set<ProviderResources>().AddAsync(resource, ct);
                    break;
                case HardwareType.CPU:
                    await _context.Set<ProviderResources>().AddAsync(resource, ct);
                    break;
                case HardwareType.RAM:
                    await _context.Set<ProviderResources>().AddAsync(resource, ct);
                    break;
                default:
                    await SendAsync(new
                    {
                        Message = "Nerastas irangos tipas"
                    }, 404, cancellation: ct);
                    break;
            }

            await SendAsync(new
            {
                Message = "Sukurta.",
                
            }, cancellation: ct);
        }
    }
}
 
