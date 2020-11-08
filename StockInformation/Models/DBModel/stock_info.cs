//-----------------------------------------------------------------------
// <copyright file="stock_info.cs" company="none">
//     Company copyright tag.
// </copyright>
//-----------------------------------------------------------------------

namespace StockInformation.Models.DBModel
{
    using System;
    using System.Collections.Generic;
    using System.Linq;
    using System.Web;
    using Newtonsoft.Json;

    /// <summary>
    /// Database table 'stock_info' model class
    /// </summary>
    public class Stock_info
    {
        /// <summary>
        /// Gets or sets column code
        /// </summary>
        public string Code { get; set; }

        /// <summary>
        /// Gets or sets column date
        /// </summary>
        public string Date { get; set; }

        /// <summary>
        /// Gets or sets column name
        /// </summary>
        public string Name { get; set; }

        /// <summary>
        /// Gets or sets dividend yield
        /// </summary>
        public double Dividend_yield { get; set; }

        /// <summary>
        /// Gets or sets dividend year
        /// </summary>
        public int Devidend_year { get; set; }

        /// <summary>
        /// Gets or sets P/E rate
        /// </summary>
        public double PE_rate { get; set; }

        /// <summary>
        /// Gets or sets P/B rate
        /// </summary>
        public double PB_rate { get; set; }

        /// <summary>
        /// Gets or sets fiscal_year_quarter
        /// </summary>
        public string Fiscal_year_quarter { get; set; }

        /// <summary>
        /// Gets or sets update time
        /// </summary>
        [JsonIgnore]
        public DateTime Updatetime { get; set; }

        /// <summary>
        /// Gets or sets create time
        /// </summary>
        [JsonIgnore]
        public DateTime Createtime { get; set; }
    }
}