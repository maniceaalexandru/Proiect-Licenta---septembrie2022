using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace TestFinal.Models.Repositores
{
    public interface ISubscriptionService 
	{
		public Task AddSubscriptionAsync(Subscription subscription);
		public Task DeleteSubscriptionAsync(Subscription subscription);
		public Task UpdateSubscriptionAsync(Subscription subscription);
		public IEnumerable<Subscription> ListAllSubscription();
		public IEnumerable<Subscription> ListAllSubscriptionWith();
		public Task<Subscription> GetSubscriptionByIdAsync(int id);
		public List<Subscription> GetSubscriptionByName(string name);
		public IEnumerable<Subscription> GetAllWhere(string subscription);


	}
}
