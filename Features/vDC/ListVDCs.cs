using FastEndpoints;
using Microsoft.EntityFrameworkCore;
using Paslauga.Data;

namespace Paslauga.Features.vDC
{
    public class ListVDCs : EndpointWithoutRequest
    {

        private readonly CloudDbContext _context;
        public override void Configure()
        {
            Get("/list/vdcs");
            AllowAnonymous();
        }

        public override async Task HandleAsync(CancellationToken ct)
        {
            var list = _context.Cloud.ToListAsync(ct);
            await SendAsync(list, 200, ct);

        }
    }
}
