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
    using StockInformation.Models;
    using StockInformation.Models.APIModel;
    using StockInformation.Models.DBModel;
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

        /// <summary>
        /// 搜尋指定日期的本益比 前n名公司
        /// </summary>
        /// <param name="request">input data</param>
        /// <returns>JSON 格式的回傳值</returns>
        [HttpPost]
        public ActionResult QueryPERatioRank(Model_QueryPERatioRank.Request request)
        {
            Model_QueryPERatioRank.Response resp = new Model_QueryPERatioRank.Response();
            try
            {
                if (request == null)
                {
                    throw new Exception("無上傳資料");
                }

                if (string.IsNullOrWhiteSpace(request.Date) || DateTime.TryParseExact(request.Date, "yyyy-MM-dd", null, System.Globalization.DateTimeStyles.None, out DateTime result) == false)
                {
                    throw new Exception("參數Date 無效");
                }

                if (request.RankNumber < 1)
                {
                    throw new Exception("參數RankNumber 無效");
                }

                var data = this.stockinfoSrv.QueryPERatioRank(request.Date, request.RankNumber);
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

        /// <summary>
        /// 搜尋日期範圍內的殖利率 嚴格增遞最大區間
        /// </summary>
        /// <param name="request">input data</param>
        /// <returns>JSON 格式的回傳值</returns>
        [HttpPost]
        public ActionResult QueryDividendYieldIncreasingDay(Model_QueryDividendYieldIncreasingDay.Request request)
        {
            Model_QueryDividendYieldIncreasingDay.Response resp = new Model_QueryDividendYieldIncreasingDay.Response();
            try
            {
                if (request == null)
                {
                    throw new Exception("無上傳資料");
                }

                if (string.IsNullOrWhiteSpace(request.StartDate) || DateTime.TryParseExact(request.StartDate, "yyyy-MM-dd", null, System.Globalization.DateTimeStyles.None, out DateTime result) == false)
                {
                    throw new Exception("參數StartDate 無效");
                }

                if (string.IsNullOrWhiteSpace(request.EndDate) || DateTime.TryParseExact(request.EndDate, "yyyy-MM-dd", null, System.Globalization.DateTimeStyles.None, out DateTime result2) == false)
                {
                    throw new Exception("參數EndDate 無效");
                }

                if (string.IsNullOrWhiteSpace(request.Code))
                {
                    throw new Exception("參數Code 無效");
                }

                var data = this.stockinfoSrv.QueryStockInfo(request.Code, request.StartDate, request.EndDate);

                MaxStrictlyIncreasingRange<Stock_info> max = new MaxStrictlyIncreasingRange<Stock_info>();
                max.Data = data.OrderBy(c => c.Date);
                max.IncreasingPropertyName = "Dividend_yield";

                resp.Result = true;
                resp.Info = max.GetRange();
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