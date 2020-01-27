using Prit.Domain.Aggregates;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace Prit.Domain.Interfaces.Repositories
{
    public interface IProductRepository
    {
        Task AddOrUpdateAsync(Product product);
        Task GetAll();
    }
}
