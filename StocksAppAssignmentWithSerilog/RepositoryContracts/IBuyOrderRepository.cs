using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Entities;

namespace RepositoryContracts
{
    /// <summary>
    /// represents the data access logic for the BuyOrder entity
    /// </summary>
    public interface IBuyOrderRepository
    {
        /// <summary>
        /// Adds the new BuyOrder object to the database
        /// </summary>
        /// <param name="buyOrder">BuyOrder Object to be added</param>
        /// <returns>returns the newly created buy order</returns>
        Task<BuyOrder> CreateBuyOrder(BuyOrder buyOrder);

        /// <summary>
        /// Gets all the buy orders from the database
        /// </summary>
        /// <returns>returns a list of buyorder objects</returns>
        Task<List<BuyOrder>> GetBuyOrders();
    }
}
