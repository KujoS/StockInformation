//-----------------------------------------------------------------------
// <copyright file="QueryController.cs" company="none">
//     Company copyright tag.
// </copyright>
//-----------------------------------------------------------------------

namespace StockInformation.Controllers
{
    using System;
    using System.Collections.Generic;
    using System.IO;
    using System.Linq;
    using System.Net;
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

        /// <summary>
        /// upload a csv file, import to database
        /// </summary>
        /// <returns>JSON 格式的回傳值</returns>
        [HttpPost]
        public ActionResult UploadStockInfo()
        {
            var resp = new Model_UploadStockInfo.Response
            {
                Result = false,
                ErrorMessage = string.Empty
            };

            var f = Request.Files["files"];
            foreach (string fileName in Request.Files)
            {
                HttpPostedFileBase file = Request.Files[fileName];

                if (file == null)
                {
                    resp.ErrorMessage += "無檔案上傳";
                    continue;
                }
                else
                {
                    if (!Path.GetExtension(file.FileName).ToUpper().Equals(".CSV"))
                    {
                        resp.ErrorMessage += $"{file.FileName}-不支援的檔案, ";
                        continue;
                    }

                    List<Stock_info> list = new List<Stock_info>();
                    using (StreamReader reader = new StreamReader(file.InputStream, System.Text.Encoding.GetEncoding("BIG5")))
                    {
                        bool firstline = true;
                        string date = string.Empty;
                        while (!reader.EndOfStream)
                        {
                            string s_line = reader.ReadLine();

                            if (firstline)
                            {
                                firstline = false;

                                s_line = s_line.Replace("\"", string.Empty);

                                string str_date = s_line.Split(' ')[0];

                                var ci = new System.Globalization.CultureInfo("zh-TW", true);
                                ci.DateTimeFormat.Calendar = new System.Globalization.TaiwanCalendar();

                                if (DateTime.TryParseExact(str_date, "y年MM月dd日", ci, System.Globalization.DateTimeStyles.None, out DateTime temp))
                                {
                                    date = temp.ToString("yyyy-MM-dd");
                                }
                                else
                                {
                                    reader.ReadToEnd();
                                    resp.ErrorMessage += $"{file.FileName}-讀取檔案錯誤, ";
                                }
                            }
                            else if (date != string.Empty)
                            {
                                var stock = s_line.ToStockInfo(date);
                                if (stock != null)
                                {
                                    list.Add(stock);
                                }
                            }
                        }
                    }

                    if (list.Count() > 0)
                    {
                        this.stockinfoSrv.Insert(list.ToArray());

                        resp.Result = true;
                        resp.ErrorMessage += $"{file.FileName}-檔案上傳成功, ";
                    }
                }
            }

            return this.Content(JsonConvert.SerializeObject(resp), "application/json");
        }

        /// <summary>
        /// 輸入一個日期區間 自動將資料從台灣證券交易所匯入至DB
        /// </summary>
        /// <param name="request">input data</param>
        /// <returns>JSON 格式的回傳值</returns>
        [HttpPost]
        public ActionResult AutoUpdateStockInfo(Model_AutoUpdateStockInfo.Request request)
        {
            ServicePointManager.SecurityProtocol = SecurityProtocolType.Tls12;

            string downloadLink = "https://www.twse.com.tw/exchangeReport/BWIBBU_d?response=csv&date={0}&selectType=ALL";
            string date_format = "yyyyMMdd";

            Model_AutoUpdateStockInfo.Response resp = new Model_AutoUpdateStockInfo.Response();
            try
            {
                if (request == null)
                {
                    throw new Exception("無上傳資料");
                }

                if (string.IsNullOrWhiteSpace(request.StartDate) || DateTime.TryParseExact(request.StartDate, "yyyy-MM-dd", null, System.Globalization.DateTimeStyles.None, out DateTime tempDT) == false)
                {
                    throw new Exception("參數StartDate 無效");
                }

                if (string.IsNullOrWhiteSpace(request.EndDate) || DateTime.TryParseExact(request.EndDate, "yyyy-MM-dd", null, System.Globalization.DateTimeStyles.None, out DateTime tempDT2) == false)
                {
                    throw new Exception("參數EndDate 無效");
                }

                for (var day = tempDT; day.Date <= tempDT2.Date; day = day.AddDays(1))
                {
                    string download = string.Format(downloadLink, day.ToString(date_format));
                    List<Stock_info> list = new List<Stock_info>();
                    using (System.Net.WebClient client = new System.Net.WebClient())
                    {
                        using (Stream stream = client.OpenRead(download))
                        {
                            using (StreamReader reader = new StreamReader(stream, System.Text.Encoding.GetEncoding("BIG5")))
                            {
                                bool firstline = true;
                                string date = string.Empty;
                                while (!reader.EndOfStream)
                                {
                                    string s_line = reader.ReadLine();

                                    if (firstline)
                                    {
                                        firstline = false;

                                        s_line = s_line.Replace("\"", string.Empty);

                                        string str_date = s_line.Split(' ')[0];

                                        var ci = new System.Globalization.CultureInfo("zh-TW", true);
                                        ci.DateTimeFormat.Calendar = new System.Globalization.TaiwanCalendar();

                                        if (DateTime.TryParseExact(str_date, "y年MM月dd日", ci, System.Globalization.DateTimeStyles.None, out DateTime temp))
                                        {
                                            date = temp.ToString("yyyy-MM-dd");
                                        }
                                        else
                                        {
                                            reader.ReadToEnd();
                                            resp.ErrorMessage += $"{day.ToString(date_format)}-讀取檔案錯誤, ";
                                        }
                                    }
                                    else if (date != string.Empty)
                                    {
                                        var stock = s_line.ToStockInfo(date);
                                        if (stock != null)
                                        {
                                            list.Add(stock);
                                        }
                                    }
                                }
                            }
                        }
                    }

                    if (list.Count() > 0)
                    {
                        this.stockinfoSrv.Insert(list.ToArray());

                        resp.Result = true;
                        resp.ErrorMessage += $"{day.ToString(date_format)}-檔案上傳成功, ";
                    }
                }
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