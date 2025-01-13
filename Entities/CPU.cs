namespace Paslauga.Entities
{
    public class CPU
    {

        public int Id { get; set; }
        public string Name { get; set; }
        public int Cores { get; set; }
        public int HyperThreading { get; set; }
        public float GHz { get; set; }
    }
}
