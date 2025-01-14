using FastEndpoints;
using Paslauga.Data;
using Paslauga.Entities;
using Paslauga.Helpers;

namespace Paslauga.Features.vLAN
{

    public class AddVLANRequest
    {
        
        public string Name { get; set; }
        public int NetworkPoolId { get; set; }
        public string GatewayCIDR { get; set; }
    }
    public class AddVLAN : Endpoint<AddVLANRequest>
    {

        private readonly CloudDbContext _context;

        public AddVLAN(CloudDbContext context)
        {
            _context = context;
        }

        public override void Configure()
        {
            Post("/add/vlan"); 
            AllowAnonymous();          
        }

        public override async Task HandleAsync(AddVLANRequest req, CancellationToken ct)
        {
            if (string.IsNullOrWhiteSpace(req.Name))
            {
                await SendAsync(new
                {
                    Message = "Nenurodytas Network Pool vardas"
                }, cancellation: ct);

                return;
            }

            if (req.NetworkPoolId <= 0)
            {
                await SendAsync(new
                {
                    Message = "Neteisingas Network Pool"
                }, cancellation: ct);

                return;
            }

            if (string.IsNullOrWhiteSpace(req.GatewayCIDR))
            {
                await SendAsync(new
                {
                    Message = "Nenurodytas GatewayCIDR"
                }, cancellation: ct);

                return;
            }

            var vlan = new VLAN
            {
                Name = req.Name,
                NetworkPoolId = req.NetworkPoolId,
                GatewayCIDR = req.GatewayCIDR
            };

            await _context.Set<VLAN>().AddAsync(vlan, ct);
            await _context.SaveChangesAsync(ct);

            vlan = await _context.Set<VLAN>().FindAsync(new object[] { req.Name }, ct);

            if (vlan == null)
            {
                await SendNotFoundAsync(ct);
                return;
            }

            string ip = IPCalc.CalculateIPs(req.GatewayCIDR);

            var availableIPs = new List<AvailableIPs>();
            for (int i = 2; i < 254; i++)
            {

                var availableIP = new AvailableIPs
                {
                    IPAddress = $"{ip}{i}",
                    Status = "Laisvas",
                    VLANId = vlan.Id
                };

                availableIPs.Add(availableIP);
            }

            await _context.Set<AvailableIPs>().AddRangeAsync(availableIPs, ct);
            await _context.SaveChangesAsync(ct);

            await SendAsync(new
            {
                Message = "Sukurta.",
                VLAN = vlan
            }, cancellation: ct);
        }
    }
}
