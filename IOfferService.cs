using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace TestFinal.Models.Repositores
{
    public interface IOfferService
	{
		public Task AddOfferAsync(Offer offer);
		public Task DeleteOfferAsync(Offer offer);
		public Task UpdateOfferAsync(Offer offer);
		public IEnumerable<Offer> ListAllOffer();
		public IEnumerable<Offer> ListAllOfferWith();
		public Task<Offer> GetOfferByIdAsync(int id);
		public List<Offer> GetOfferByName(string name);
	}
}
