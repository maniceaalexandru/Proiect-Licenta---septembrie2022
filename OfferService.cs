using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using TestFinal.DAL.Interfaces;
using TestFinal.Data;


namespace TestFinal.Models.Repositores
{
    public class OfferService: IOfferService

	{
		private readonly IGenericRepository<Offer> offerRepo;

		public OfferService(IGenericRepository<Offer> _offerRepo)
		{
			offerRepo = _offerRepo;
		}

		public async Task AddOfferAsync(Offer Offer)
		{
			await offerRepo.Insert(Offer);
		}
		public async Task DeleteOfferAsync(Offer Offer)
		{
			await offerRepo.Delete(Offer);
		}
		public async Task<Offer> GetOfferByIdAsync(int id)
		{
			return await offerRepo.GetById(id);
		}
		public List<Offer> GetOfferByName(string name)
		{
			return offerRepo.GetByCondition(o => o.Title.Equals(name));
		}
		public IEnumerable<Offer> ListAllOffer()
		{
			return offerRepo.GetAll();
		}
		public IEnumerable<Offer> ListAllOfferWith()
		{
			return offerRepo.GetAll(d => d.Title);
		}
		public async Task UpdateOfferAsync(Offer Offer)
		{
			await offerRepo.Update(Offer);
		}



	}
}
