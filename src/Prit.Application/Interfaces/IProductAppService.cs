using Prit.Domain.Aggregates;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace Prit.Application.Interfaces
{
    public interface IProductAppService
    {
        Task AddOrUpdateAsync(Product product);
        Task<IList<Product>> GetAllAsync();
        Task RemoveAsync(int id);
    }
}
