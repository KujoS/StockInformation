using Microsoft.VisualStudio.TestTools.UnitTesting;
using StockInformation.Models.Service;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using StockInformation.Models.DBModel;

namespace StockInformation.Models.Service.Tests
{
    [TestClass()]
    public class SqliteTest
    {
        [TestMethod()]
        public void CRUD()
        {
            Stock_InfoService srv = new Stock_InfoService();

            Stock_info test = new Stock_info()
            {
                Code = "test9999",
                Name = "測試用",
                Date = "20201107",
                Devidend_year = 108,
                Dividend_yield = 9.99,
                PB_rate = 8.88,
                PE_rate = 7.77,
                Fiscal_year_quarter = "109/3"
            };
            int rtn = srv.Insert(test);
            Assert.IsTrue(rtn > 0);

            var query = srv.Query(test.Code, test.Date);
            Assert.IsNotNull(query);

            test.Name += "_update";
            rtn = srv.Update(test);
            Assert.IsTrue(rtn > 0);

            rtn = srv.Delete(test);
            Assert.IsTrue(rtn > 0);
        }
    }
}