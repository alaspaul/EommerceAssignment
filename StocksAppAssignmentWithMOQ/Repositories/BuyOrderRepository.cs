
using Entities;
using Microsoft.EntityFrameworkCore;
using RepositoryContracts;

namespace Repositories
{
    public class BuyOrderRepository : IBuyOrderRepository
    {
        private readonly TradeDbContext _dbContext;

        public BuyOrderRepository(TradeDbContext context)
        {
            _dbContext = context;
        }

        public async Task<BuyOrder> CreateBuyOrder(BuyOrder buyOrder)
        {
            _dbContext.BuyOrders.Add(buyOrder);
            await _dbContext.SaveChangesAsync();

            return buyOrder;
        }

        public async Task<List<BuyOrder>> GetBuyOrders()
        {
            return await _dbContext.BuyOrders.ToListAsync();
        }
    }
}
