using Microsoft.EntityFrameworkCore;

namespace Paslauga.Entities
{
    public class NetworkPool
    {
        public int Id { get; set; }

        public ICollection<VLAN>? VLANs { get; set; } = new List<VLAN>();
        public string Name { get; set; }
        public string? Description { get; set; }
        public string Status { get; set; }

    }


}
