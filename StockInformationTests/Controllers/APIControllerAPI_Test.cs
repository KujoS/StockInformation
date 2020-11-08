using Microsoft.VisualStudio.TestTools.UnitTesting;
using StockInformation.Controllers;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Web.Mvc;
using StockInformation.Models.APIModel;
using Newtonsoft.Json;

namespace StockInformation.Controllers.Tests
{
    [TestClass()]
    public class APIControllerAPI_Test
    {
        [TestMethod()]
        public void QueryStockInfoQueryStockInfo()
        {
            QueryController api = new QueryController();

            var resutl = (ContentResult)api.QueryStockInfo(new Model_QueryStockInfo.Request { Code = "1101", LastDays = 1 });
            var resp = JsonConvert.DeserializeObject<Model_QueryStockInfo.Response>(resutl.Content);


            Assert.IsNotNull(resp);
        }

        [TestMethod]
        public void QueryPERatioRank()
        {
            QueryController api = new QueryController();

            var resutl = (ContentResult)api.QueryPERatioRank(new Model_QueryPERatioRank.Request { Date = "2020-11-06", RankNumber = 3 });
            var resp = JsonConvert.DeserializeObject<Model_QueryPERatioRank.Response>(resutl.Content);


            Assert.IsNotNull(resp);
        }

        [TestMethod]
        public void QueryDividendYieldIncreasingDay()
        {
            QueryController api = new QueryController();

            var resutl = (ContentResult)api.QueryDividendYieldIncreasingDay(new Model_QueryDividendYieldIncreasingDay.Request { Code = "1101", StartDate = "2020-11-01", EndDate = "2020-11-30" });
            var resp = JsonConvert.DeserializeObject<Model_QueryPERatioRank.Response>(resutl.Content);


            Assert.IsNotNull(resp);
        }
    }
}