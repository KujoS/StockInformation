using Microsoft.VisualStudio.TestTools.UnitTesting;
using StockInformation.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using StockInformation.Models.DBModel;

namespace StockInformation.Models.Tests
{
    [TestClass()]
    public class MaxStrictlyIncreasingRangeAPI_Test
    {
        [TestMethod()]
        public void GetRangeQueryStockInfo()
        {
            MaxStrictlyIncreasingRange<Stock_info> max = new MaxStrictlyIncreasingRange<Stock_info>();
            max.IncreasingPropertyName = "Dividend_yield";
            max.Data = new List<Stock_info>
            {
                new Stock_info
                {
                    Date="2020-11-01",
                    Dividend_yield=1
                },
                new Stock_info
                {
                    Date="2020-11-02",
                    Dividend_yield=2
                },
                new Stock_info
                {
                    Date="2020-11-03",
                    Dividend_yield=3
                },
                new Stock_info
                {
                    Date="2020-11-04",
                    Dividend_yield=4
                },
                new Stock_info
                {
                    Date="2020-11-05",
                    Dividend_yield=5
                },
                new Stock_info
                {
                    Date="2020-11-06",
                    Dividend_yield=6
                },
                new Stock_info
                {
                    Date="2020-11-07",
                    Dividend_yield=7
                },
                new Stock_info
                {
                    Date="2020-11-08",
                    Dividend_yield=2
                },
                new Stock_info
                {
                    Date="2020-11-09",
                    Dividend_yield=3
                },
                new Stock_info
                {
                    Date="2020-11-10",
                    Dividend_yield=4
                },
                new Stock_info
                {
                    Date="2020-11-11",
                    Dividend_yield=5
                },
                new Stock_info
                {
                    Date="2020-11-12",
                    Dividend_yield=6
                },
                new Stock_info
                {
                    Date="2020-11-13",
                    Dividend_yield=7
                }
            };
            max.Data = new List<Stock_info>();
            var a = max.GetRange();

            Assert.IsNotNull(a);
        }
    }
}