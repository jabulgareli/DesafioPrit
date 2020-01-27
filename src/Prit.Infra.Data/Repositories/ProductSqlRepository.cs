using Microsoft.EntityFrameworkCore;
using Prit.Domain.Aggregates;
using Prit.Domain.Interfaces.Repositories;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace Prit.Infra.Data.Repositories
{
    public class ProductSqlRepository : IProductRepository
    {
        private readonly PritContext _context;

        public ProductSqlRepository(PritContext context)
        {
            _context = context;
        }

        public async Task AddOrUpdateAsync(Product product)
        {
            if (product is null)
            {
                throw new ArgumentNullException(nameof(product));
            }

            var current = await _context.Products.FirstOrDefaultAsync(p => p.Id == product.Id);

            if (current is null)
            {
                _context.Add(product);
                await _context.SaveChangesAsync();
                return;
            }

            _context.Entry(current).CurrentValues.SetValues(product);
            await _context.SaveChangesAsync();
        }

        public async Task<IList<Product>> GetAllAsync()
        {
            return await _context.Products.ToListAsync();
        }

        public async Task RemoveAsync(int id)
        {
            var current = await _context.Products.FirstOrDefaultAsync(p => p.Id == id);

            if (current is null)
            {
                return;
            }

            _context.Remove(current);
            await _context.SaveChangesAsync();
        }
    }
}
