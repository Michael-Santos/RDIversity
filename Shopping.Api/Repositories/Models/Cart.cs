namespace Shopping.Api.Repositories.Models
{
    public class Cart
    {
        public int Id { get; set; }
        public ICollection<CartItem> Items { get; set; } = new List<CartItem>();
    }
}
