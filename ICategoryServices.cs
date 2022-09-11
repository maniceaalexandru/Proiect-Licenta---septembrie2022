using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace TestFinal.Models.Repositores
{
	public interface ICategoryServices
	{
		public Task AddCategoryAsync(Category category);
		public Task DeleteCategoryAsync(Category category);

		public Task UpdateCategoryAsync(Category category);
		public IEnumerable<Category> ListAllCategory();
		public Task<Category> GetCategoryByIdAsync(int id);
		public List<Category> GetCategoryByName(string name);
	}
}
