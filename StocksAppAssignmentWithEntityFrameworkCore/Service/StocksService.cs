﻿using Service.Helpers;
using ServiceContracts;
using ServiceContracts.DTO.BuyOrderDTO;
using ServiceContracts.DTO.SellOrderDTO;
using Entities;
using Microsoft.EntityFrameworkCore;

namespace Service
{
    public class StocksService : IStocksService
    {
        private readonly List<BuyOrder> _buyOrders;
        private readonly List<SellOrder> _sellOrders;
        private readonly TradeDbContext _db;

        public StocksService(TradeDbContext tradeDbContext)
        {
            _buyOrders = new List<BuyOrder>();
            _sellOrders = new List<SellOrder>();
            _db = tradeDbContext;

            //if (Initialize) 
            //{
            //    _buyOrders.AddRange(new List<BuyOrder>() 
            //    { 
            //        new BuyOrder { BuyOrderId = Guid.Parse("2268D2C5-32EB-4517-91B3-A5E68AB86066"), Price = 100, Quantity = 100, StockName = "something100", StockSymbol = "SMT100", DateAndTimeOfOrder = DateTime.Now },

            //        new BuyOrder { BuyOrderId = Guid.Parse("8ABDFB23-EC13-42B8-898B-198D071E8717"), Price = 10, Quantity = 10, StockName = "something10", StockSymbol = "SMT10", DateAndTimeOfOrder = DateTime.Now },

            //        new BuyOrder { BuyOrderId = Guid.Parse("D6B2539E-CC10-4172-9F39-91DC9585162A"), Price = 1, Quantity = 1, StockName = "something1", StockSymbol = "SMT1", DateAndTimeOfOrder = DateTime.Now },
            //    });

            //    _sellOrders.AddRange(new List<SellOrder>()
            //    {
            //        new SellOrder { SellOrderId = Guid.Parse("1E4ABDE8-CDDB-4BDF-B085-D9505E981618"), Price = 100, Quantity = 100, StockName = "everything100", StockSymbol = "ERT100", DateAndTimeOfOrder = DateTime.Now },

            //        new SellOrder { SellOrderId = Guid.Parse("0534C02E-7C96-4EFF-B194-42DAFA028B73"), Price = 10, Quantity = 10, StockName = "everything10", StockSymbol = "ERT10", DateAndTimeOfOrder = DateTime.Now },

            //        new SellOrder { SellOrderId = Guid.Parse("62C7E53A-6A1A-4C17-9E11-C56229948159"), Price = 1, Quantity = 1, StockName = "everything1", StockSymbol = "ERT1", DateAndTimeOfOrder = DateTime.Now },
            //    });
            //}
        }

        public async Task<BuyOrderResponse> CreateBuyOrder(BuyOrderRequest request)
        {
            if (request == null) { throw new ArgumentNullException(); }


            ValidationHelper.ModelValidation(request);

            BuyOrder buyOrder = request.ToBuyOrder();

            buyOrder.BuyOrderId = Guid.NewGuid();

            _db.Add(buyOrder);
            await _db.SaveChangesAsync();


            return buyOrder.ToBuyOrderResponse();
        }

        public async Task<SellOrderResponse> CreateSellOrder(SellOrderRequest request)
        {
            if (request == null) { throw new ArgumentNullException(); }


            ValidationHelper.ModelValidation(request);

            SellOrder sellOrder = request.ToSellOrder();

            sellOrder.SellOrderId = Guid.NewGuid();
            _db.Add(sellOrder);
            await _db.SaveChangesAsync();

            return sellOrder.ToSellOrderResponse();
        }

        public async Task<List<BuyOrderResponse>> GetBuyOrders()
        {
            return await _db.BuyOrders.Select(x => x.ToBuyOrderResponse()).ToListAsync();
        }

        public async Task<List<SellOrderResponse>> GetSellOrders()
        {
            return await _db.SellOrders.Select(x => x.ToSellOrderResponse()).ToListAsync();
        }
    }
}
