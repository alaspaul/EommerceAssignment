
using Entities;
using Microsoft.EntityFrameworkCore;
using RepositoryContracts;

namespace Repositories
{
    public class SellOrderRepository : ISellOrderRepository
    {
        private readonly TradeDbContext _dbContext;

        public SellOrderRepository(TradeDbContext context)
        {
            _dbContext = context;
        }

        public async Task<SellOrder> CreateSellOrder(SellOrder sellOrder)
        {
            _dbContext.SellOrders.Add(sellOrder);
            await _dbContext.SaveChangesAsync();

            return sellOrder;
        }

        public async Task<List<SellOrder>> GetSellOrders()
        {
            return await _dbContext.SellOrders.ToListAsync();
        }
    }
}
