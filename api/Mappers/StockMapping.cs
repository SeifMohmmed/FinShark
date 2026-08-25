using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using api.Dtos;
using api.Dtos.Stock;
using api.Models;

namespace api.Mappers
{
    public static class StockMapping
    {
        public static StockDto ToStockDto(this Stock stock)
        {
            return new StockDto
            {
              Id = stock.Id,
              Symbol = stock.Symbol,
              CompanyName = stock.CompanyName,
              Purchase = stock.Purchase,  
              LastDiv = stock.LastDiv,
              Industry = stock.Industry,
              MarketCap = stock.MarketCap
            };
        }
        public static Stock ToStockFromCreateStockDto(this CreateStockRequestDto createStockRequestDto)
        {
            return new Stock
            {
                Symbol = createStockRequestDto.Symbol,
                CompanyName = createStockRequestDto.CompanyName,
                Purchase = createStockRequestDto.Purchase,
                LastDiv = createStockRequestDto.LastDiv,
                Industry = createStockRequestDto.Industry,
                MarketCap = createStockRequestDto.MarketCap
            };
        }
    }
}