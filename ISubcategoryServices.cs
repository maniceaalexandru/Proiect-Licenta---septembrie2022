using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace TestFinal.Models.Repositores
{
	public interface ISubcategoryServices
	{
		public Task AddSubCategoryAsync(Subcategory subcategorie);
		public Task DeleteSubCategoryAsync(Subcategory subcategorie);

		public Task UpdateSubCategoryAsync(Subcategory subcategorie);
		public IEnumerable<Subcategory> ListAllSubCategory();
		public Task<Subcategory> GetSubCategoryByIdAsync(int id);
		public List<Subcategory> GetSubCategoryByName(string name);
	}
}
