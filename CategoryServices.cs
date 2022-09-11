using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using TestFinal.DAL.Interfaces;

namespace TestFinal.Models.Repositores
{
	public class CategoryServices : ICategoryServices
	{
		private readonly IGenericRepository<Category> categorieRepo;

		public CategoryServices(IGenericRepository<Category> _categorieRepo)
		{
			categorieRepo = _categorieRepo;
		}

		public async Task AddCategoryAsync(Category category)
		{
			await categorieRepo.Insert(category);
		}


		public async Task DeleteCategoryAsync(Category category)
		{
			await categorieRepo.Delete(category);
		}

		public async Task<Category> GetCategoryByIdAsync(int id)
		{
			return await categorieRepo.GetById(id);
		}

		public IEnumerable<Category> ListAllCategory()
		{
			return categorieRepo.GetAll();
		}

		public async Task UpdateCategoryAsync(Category category)
		{
			await categorieRepo.Update(category);
		}

		public List<Category> GetCategoryByName(string name)
		{
			return categorieRepo.GetByCondition(o => o.CategoryName.Equals(name));
		}
	}
}
