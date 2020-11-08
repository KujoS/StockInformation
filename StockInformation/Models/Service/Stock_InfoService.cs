//-----------------------------------------------------------------------
// <copyright file="Stock_InfoService.cs" company="none">
//     Company copyright tag.
// </copyright>
//-----------------------------------------------------------------------

namespace StockInformation.Models.Service
{
    using System;
    using System.Collections.Generic;
    using System.Configuration;
    using System.Data.SQLite;
    using System.Linq;
    using System.Web;
    using Dapper;
    using StockInformation.Models.DBModel;

    /// <summary>
    /// table  stock_info operation class
    /// </summary>
    public class Stock_InfoService
    {
        /// <summary>
        /// database connection
        /// </summary>
        private SQLiteConnection conn;

        /// <summary>
        /// database connection string
        /// </summary>
        private string connStr = ConfigurationManager.ConnectionStrings["SqliteConn"].ConnectionString;

        /// <summary>
        /// Initializes a new instance of the <see cref="Stock_InfoService" /> class
        /// </summary>
        public Stock_InfoService()
        {
            this.conn = new SQLiteConnection(this.connStr);
        }

        /// <summary>
        /// Query data
        /// </summary>
        /// <param name="code">company code</param>
        /// <param name="date">information date</param>
        /// <returns>table data</returns>
        public Stock_info Query(string code, string date)
        {
            return this.conn.Query<Stock_info>("select * from stock_info where code=@code and date=@date", new { code = code, date = date }).FirstOrDefault();
        }

        /// <summary>
        /// Insert data
        /// </summary>
        /// <param name="value">insert data</param>
        /// <returns>The number of rows affected</returns>
        public int Insert(Stock_info value)
        {
            return this.conn.Execute(@"insert into stock_info (code, date, name, dividend_yield, devidend_year, pe_rate, pb_rate, fiscal_year_quarter) values(@code, @date, @name, @dividend_yield, @devidend_year, @pe_rate, @pb_rate, @fiscal_year_quarter) ON CONFLICT(code, date) DO UPDATE SET name=@name, dividend_yield=@dividend_yield, devidend_year=@devidend_year, pe_rate=@pe_rate, pb_rate=@pb_rate, fiscal_year_quarter=@fiscal_year_quarter;", value);
        }

        /// <summary>
        /// Insert multiple data
        /// </summary>
        /// <param name="value">insert data</param>
        /// <returns>The number of rows affected</returns>
        public int Insert(Stock_info[] value)
        {
            return this.conn.Execute(@"insert into stock_info (code, date, name, dividend_yield, devidend_year, pe_rate, pb_rate, fiscal_year_quarter) values(@code, @date, @name, @dividend_yield, @devidend_year, @pe_rate, @pb_rate, @fiscal_year_quarter) ON CONFLICT(code, date) DO UPDATE SET name=@name, dividend_yield=@dividend_yield, devidend_year=@devidend_year, pe_rate=@pe_rate, pb_rate=@pb_rate, fiscal_year_quarter=@fiscal_year_quarter;", value);
        }

        /// <summary>
        /// Update data
        /// </summary>
        /// <param name="value">update data</param>
        /// <returns>The number of rows affected</returns>
        public int Update(Stock_info value)
        {
            return this.conn.Execute(@"update stock_info set name=@name, dividend_yield=@dividend_yield, devidend_year=@devidend_year, pe_rate=@pe_rate, pb_rate=@pb_rate, fiscal_year_quarter=@fiscal_year_quarter where code=@code and date=@date;", value);
        }

        /// <summary>
        /// Update multiple data
        /// </summary>
        /// <param name="value">update data</param>
        /// <returns>The number of rows affected</returns>
        public int Update(Stock_info[] value)
        {
            return this.conn.Execute(@"update stock_info set name=@name, dividend_yield=@dividend_yield, devidend_year=@devidend_year, pe_rate=@pe_rate, pb_rate=@pb_rate, fiscal_year_quarter=@fiscal_year_quarter where code=@code and date=@date;", value);
        }

        /// <summary>
        /// Delete data
        /// </summary>
        /// <param name="value">delete data</param>
        /// <returns>The number of rows affected</returns>
        public int Delete(Stock_info value)
        {
            return this.conn.Execute("delete from stock_info where code=@code and date=@date;", value);
        }

        /// <summary>
        /// Delete multiple data
        /// </summary>
        /// <param name="value">delete data</param>
        /// <returns>The number of rows affected</returns>
        public int Delete(Stock_info[] value)
        {
            return this.conn.Execute("delete from stock_info where code=@code and date=@date;", value);
        }

        /// <summary>
        /// Query Stock Information by code
        /// </summary>
        /// <param name="code">company code</param>
        /// <param name="lastdays">last day to search</param>
        /// <returns>data of last days</returns>
        public IEnumerable<Stock_info> QueryStockInfo(string code, int lastdays)
        {
            return this.conn.Query<Stock_info>($"select * from stock_info where code=@code and date >= date( 'now', '-{lastdays} days');", new { code = code });
        }

        /// <summary>
        /// Query rank of P/E ratio
        /// </summary>
        /// <param name="date">the date to search</param>
        /// <param name="rankNumber">number of rank</param>
        /// <returns>rank date of the P/E ratio</returns>
        public IEnumerable<Stock_info> QueryPERatioRank(string date, int rankNumber)
        {
            return this.conn.Query<Stock_info>($"select * from stock_info where date=@date order by pe_rate desc limit {rankNumber}", new { date = date });
        }
    }
}