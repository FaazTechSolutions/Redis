namespace Cache.Models
{
    public record Product
    {
        public int Id { get; init; }
        public string Name { get; init; }
        public decimal Price { get; init; }
    }
}
