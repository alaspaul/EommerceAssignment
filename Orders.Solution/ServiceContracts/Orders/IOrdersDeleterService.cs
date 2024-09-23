
namespace ServiceContracts.Orders
{

    public interface IOrdersDeleterService
    {
        /// <summary>
        /// Deletes an order by its ID.
        /// </summary>
        /// <param name="orderId">The ID of the order to delete.</param>
        /// <returns>A boolean indicating whether the deletion was successful.</returns>
        Task<bool> DeleteOrderByOrderId(Guid orderId);
    }
}