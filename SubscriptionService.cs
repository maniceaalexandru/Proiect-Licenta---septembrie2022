using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Threading.Tasks;
using TestFinal.DAL.Interfaces;
using TestFinal.Data;

namespace TestFinal.Models.Repositores
{
    public class SubscriptionService : ISubscriptionService
    {
		private readonly IGenericRepository<Subscription> subscriptionRepo;

		public SubscriptionService(IGenericRepository<Subscription> _subscriptionRepo)
		{
			subscriptionRepo = _subscriptionRepo;
		}

		public async Task AddSubscriptionAsync(Subscription subscription)
		{
			await subscriptionRepo.Insert(subscription);
		}

		public async Task DeleteSubscriptionAsync(Subscription subscription)
		{
			await subscriptionRepo.Delete(subscription);
		}

		public async Task<Subscription> GetSubscriptionByIdAsync(int id)
		{
			return await subscriptionRepo.GetById(id);
		}
		public List<Subscription> GetSubscriptionByName(string name)
		{
			return subscriptionRepo.GetByCondition(o => o.Type.Equals(name));
		}

		public IEnumerable<Subscription> ListAllSubscription()
		{
			return subscriptionRepo.GetAll();
		}

		public IEnumerable<Subscription> ListAllSubscriptionWith()
		{
			return subscriptionRepo.GetAll(d => d.Type);
		}
		public async Task UpdateSubscriptionAsync(Subscription subscription)
		{
			await subscriptionRepo.Update(subscription);
		}

		public IEnumerable<Subscription> GetAllWhere(string subscription)
        {
			return subscriptionRepo.GetByCondition(s => s.Specification.Contains(subscription) || s.Type.Contains(subscription));
        }


	}
}
