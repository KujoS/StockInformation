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
    }
}