using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using TestFinal.DAL.Interfaces;

namespace TestFinal.Models.Repositores
{
	public class SubcategoryService : ISubcategoryServices
	{

		private readonly IGenericRepository<Subcategory> subcategoriRepo;

		public SubcategoryService(IGenericRepository<Subcategory> _subcategoriRepo)
		{
			subcategoriRepo = _subcategoriRepo;
		}

		public async Task AddSubCategoryAsync(Subcategory subcategorie)
		{
			await subcategoriRepo.Insert(subcategorie);
		}


		public async Task DeleteSubCategoryAsync(Subcategory subcategorie)
		{
			await subcategoriRepo.Delete(subcategorie);
		}
		/*
		public async Task DeleteCategoryAsync(int id)
		{
			await categorieRepo.DeleteById(id);
		}
		*/
		public async Task<Subcategory> GetSubCategoryByIdAsync(int id)
		{
			return await subcategoriRepo.GetById(id);
		}

		public IEnumerable<Subcategory> ListAllSubCategory()
		{
			return subcategoriRepo.GetAll(c => c.Categories);
		}

		public async Task UpdateSubCategoryAsync(Subcategory subcategorie)
		{
			await subcategoriRepo.Update(subcategorie);
		}

		public List<Subcategory> GetSubCategoryByName(string name)
		{
			return subcategoriRepo.GetByCondition(o => o.Nume.Equals(name));
		}
	}
}
