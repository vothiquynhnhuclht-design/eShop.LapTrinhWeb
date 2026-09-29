using eShop.CoreBusiness.Models;
using eShop.UseCases.PluginInterfaces.DataStore;
using System.Collections.Generic;

namespace eShop.DataStore.SQL.Dapper
{
    public class ProductRepository : IProductRepository
    {
        private readonly ISqlDataAccess sqlDataAccess;

        public ProductRepository(ISqlDataAccess sqlDataAccess)
        {
            this.sqlDataAccess = sqlDataAccess;
        }

        public Product GetProduct(int id)
        {
            string sql = "SELECT ProductId AS Id, Brand, Name, Price, ImageLink, Description FROM Product WHERE ProductId = @ProductId";
            return sqlDataAccess.LoadSingleData<Product, object>(sql, new { ProductId = id });
        }

        public IEnumerable<Product> GetProducts(string filter = null)
        {
            string sql = "SELECT ProductId AS Id, Brand, Name, Price, ImageLink, Description FROM Product";
            if (string.IsNullOrWhiteSpace(filter))
            {
                return sqlDataAccess.LoadData<Product, object>(sql, new { });
            }
            else
            {
                sql += " WHERE Name LIKE '%' + @Filter + '%'";
                return sqlDataAccess.LoadData<Product, object>(sql, new { Filter = filter });
            }
        }
    }
}
