using Prit.Application.Interfaces;
using Prit.Domain.Aggregates;
using Prit.Domain.Interfaces.Repositories;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace Prit.Application.Services
{
    public class ProductAppService : IProductAppService
    {
        private readonly IProductRepository _productRepository;

        public ProductAppService(IProductRepository productRepository)
        {
            _productRepository = productRepository;
        }

        public async Task AddOrUpdateAsync(Product product)
        {
            await _productRepository.AddOrUpdateAsync(product);
        }

        public async Task<IList<Product>> GetAllAsync()
        {
            return await _productRepository.GetAllAsync();
        }

        public async Task RemoveAsync(int id)
        {
            await _productRepository.RemoveAsync(id);
        }
    }
}
