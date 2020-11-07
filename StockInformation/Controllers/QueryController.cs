//-----------------------------------------------------------------------
// <copyright file="QueryController.cs" company="none">
//     Company copyright tag.
// </copyright>
//-----------------------------------------------------------------------

namespace StockInformation.Controllers
{
    using System;
    using System.Collections.Generic;
    using System.Linq;
    using System.Web;
    using System.Web.Mvc;
    using Newtonsoft.Json;
    using StockInformation.Models.APIModel;
    using StockInformation.Models.Service;
    
    /// <summary>
    /// Query Controller
    /// </summary>
    public class QueryController : Controller
    {
        /// <summary>
        /// Database access class
        /// </summary>
        private Stock_InfoService stockinfoSrv = new Stock_InfoService();

        /// <summary>
        /// 依照證券代號 搜尋最近n天的資料
        /// </summary>
        /// <param name="request">input data</param>
        /// <returns>JSON 格式的回傳值</returns>
        [HttpPost]
        public ActionResult QueryStockInfo(Model_QueryStockInfo.Request request)
        {
            Model_QueryStockInfo.Response resp = new Model_QueryStockInfo.Response();
            try
            {
                if (request == null)
                {
                    throw new Exception("無上傳資料");
                }

                if (string.IsNullOrWhiteSpace(request.Code))
                {
                    throw new Exception("參數Code 無效");
                }

                if (request.LastDays < 1)
                {
                    throw new Exception("參數LastDays 無效");
                }

                var data = this.stockinfoSrv.QueryStockInfo(request.Code, request.LastDays);
                resp.Result = true;
                resp.Info = data;
            }
            catch (Exception ex)
            {
                resp.Result = false;
                resp.ErrorMessage = ex.Message;
            }

            return this.Content(JsonConvert.SerializeObject(resp), "application/json");
        }
    }
}