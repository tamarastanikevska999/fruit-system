
namespace Core.Entities
{
    public class Fruit
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public string Family { get; set; }
        public string Order { get; set; }
        public string Genus { get; set; }
        public Dictionary<string, decimal> Nutritions { get; set; }
    }
}
