using System.Text.Json.Serialization;

namespace Shopping.Api.Repositories.Models
{
    public class CartItem
    {
        [JsonIgnore]
        public int Id { get; set; }
        [JsonIgnore]
        public int CartId { get; set; }
        [JsonIgnore]
        public int ProductId { get; set; }
        public Product Product { get; set; }
        public int Quantity { get; set; }
        public decimal TotalPrice => Product.Price * Quantity;
        [JsonIgnore]
        public Cart Cart { get; set; }
    }
}
