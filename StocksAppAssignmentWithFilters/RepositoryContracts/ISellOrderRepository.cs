using Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace RepositoryContracts
{
    /// <summary>
    /// Represents the Data access logic for the SellOrder entity
    /// </summary>
    public interface ISellOrderRepository
    {
        /// <summary>
        /// Adds the new SellOrder object to the database
        /// </summary>
        /// <param name="sellOrder">SellOrder object to be added</param>
        /// <returns>Returns the newly added sellorder</returns>
        Task<SellOrder> CreateSellOrder(SellOrder sellOrder);

        /// <summary>
        /// gets all the sell orders from the database
        /// </summary>
        /// <returns>returns a list of sellorder object</returns>
        Task<List<SellOrder>> GetSellOrders();
    }
}
