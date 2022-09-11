using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using TestFinal.DAL.Interfaces;

namespace TestFinal.Models.Repositores
{
	public class ProductService : IProductService
	{
		private readonly IGenericRepository<Product> productRepo;

		public ProductService(IGenericRepository<Product> _productRepo)
		{
			productRepo = _productRepo;
		}



		public async Task AddProductAsync(Product product)
		{
			await productRepo.Insert(product);
		}

		public async Task DeleteProductAsync(Product product)
		{
			await productRepo.Delete(product);
		}

		public async Task<Product> GetProductByIdAsync(int id)
		{
			return await productRepo.GetById(id);
		}
		public List<Product> GetProductByName(string name)
		{
			return productRepo.GetByCondition(o => o.ProductName.Equals(name));
		}

		public IEnumerable<Product> ListAllProduct()
		{
			return productRepo.GetAll(c => c.Subcategorie);
		}

		public IEnumerable<Product> ListAllProductWith()
		{
			return productRepo.GetAll(d => d.ProductName);
		}
		public async Task UpdateProductAsync(Product produs)
		{
			await productRepo.Update(produs);
		}

		public IEnumerable<Product> GetAllWhere(string name)
		{
			return productRepo.GetByCondition(s => s.ProductName.Contains(name));
		}



	}
}
