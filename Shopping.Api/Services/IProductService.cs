using Shopping.Api.Repositories.Models;

namespace Shopping.Api.Services
{
    public interface IProductService
    {
        IEnumerable<Product> GetAll();
    }
}
