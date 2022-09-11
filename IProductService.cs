using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace TestFinal.Models.Repositores
{
	public interface IProductService
	{
		public Task AddProductAsync(Product product);
		public Task DeleteProductAsync(Product product);
		public Task UpdateProductAsync(Product product);
		public IEnumerable<Product> ListAllProduct();
		public IEnumerable<Product> ListAllProductWith();
		public Task<Product> GetProductByIdAsync(int id);
		public List<Product> GetProductByName(string name);
		public IEnumerable<Product> GetAllWhere(string nume);
	}
}
