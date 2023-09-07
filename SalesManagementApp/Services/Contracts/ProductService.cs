using SalesManagementApp.Data;
using SalesManagementApp.Extensions;
using SalesManagementApp.Models;

namespace SalesManagementApp.Services.Contracts
{
    public class ProductService : IProductService
    {
        private readonly SalesManagementDbContext salesManagementDbContext;
        public ProductService(SalesManagementDbContext salesManagementDbContext)
        {

        }
        public async Task<List<ProductModel>> GetProducts()
        {

			try
			{
                var products = await this.salesManagementDbContext.Products.Convert(salesManagementDbContext);
                return products;
			}
			catch (Exception)
			{

				throw;
			}
        }
    }
}
