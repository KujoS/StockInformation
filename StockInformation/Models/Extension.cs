//-----------------------------------------------------------------------
// <copyright file="Extension.cs" company="none">
//     Company copyright tag.
// </copyright>
//-----------------------------------------------------------------------
namespace StockInformation.Models
{
    using System;
    using System.Collections.Generic;
    using System.Linq;
    using System.Web;
    using StockInformation.Models.DBModel;

    /// <summary>
    /// class of extension
    /// </summary>
    public static class Extension
    {
        /// <summary>
        /// convert string to stock_info class
        /// </summary>
        /// <param name="str">string data</param>
        /// <param name="date">date of stock_info</param>
        /// <returns>stock_info class</returns>
        public static Stock_info ToStockInfo(this string str, string date)
        {
            str = str.Replace("\"", string.Empty);
            string[] arr_str = str.Split(new char[] { ',' }, StringSplitOptions.RemoveEmptyEntries);
            if (arr_str.Length != 7)
            {
                return null;
            }

            string code = arr_str[0];
            string name = arr_str[1];
            string dividend_yield = arr_str[2];
            string dividend_year = arr_str[3];
            string pe_ratio = arr_str[4];
            string pb_ratio = arr_str[5];
            string fiscal_year_quarter = arr_str[6];

            try
            {
                Stock_info stock = new Stock_info
                {
                    Code = code,
                    Date = date,
                    Name = name,
                    Dividend_yield = dividend_yield.ToDouble(),
                    Devidend_year = dividend_year.ToInt32(),
                    PE_rate = pe_ratio.ToDouble(),
                    PB_rate = pb_ratio.ToDouble(),
                    Fiscal_year_quarter = fiscal_year_quarter
                };

                return stock;
            }
            catch
            {
                return null;
            }
        }

        /// <summary>
        /// convert string to integer
        /// </summary>
        /// <param name="str">string data</param>
        /// <returns>int value</returns>
        public static int ToInt32(this string str)
        {
            return Convert.ToInt32(str);
        }

        /// <summary>
        /// convert string to double
        /// </summary>
        /// <param name="str">string data</param>
        /// <returns>double value</returns>
        public static double ToDouble(this string str)
        {
            return Convert.ToDouble(str);
        }
    }
}