//-----------------------------------------------------------------------
// <copyright file="Model_QueryDividendYieldIncreasingDay.cs" company="none">
//     Model_QueryDividendYieldIncreasingDay for the qpi QueryDividendYieldIncreasingDay
//     Company copyright tag.
// Model_QueryStockInfo
// </copyright>
//-----------------------------------------------------------------------

namespace StockInformation.Models.APIModel
{
    using System;
    using System.Collections.Generic;
    using System.Linq;
    using System.Web;
    using StockInformation.Models.DBModel;

    /// <summary>
    /// API Model
    /// </summary>
    public class Model_QueryDividendYieldIncreasingDay
    {
        /// <summary>
        /// Request Class
        /// </summary>
        public class Request
        {
            /// <summary>
            /// Gets or sets Company code
            /// </summary>
            public string Code { get; set; }

            /// <summary>
            /// Gets or sets the start date
            /// </summary>
            public string StartDate { get; set; }

            /// <summary>
            /// Gets or sets the end date
            /// </summary>
            public string EndDate { get; set; }
        }

        /// <summary>
        /// Response Class
        /// </summary>
        public class Response
        {
            /// <summary>
            /// Gets or sets a value indicating whether api result
            /// </summary>
            public bool Result { get; set; }

            /// <summary>
            /// Gets or sets the search data
            /// </summary>
            public IEnumerable<Stock_info> Info { get; set; }

            /// <summary>
            /// Gets or sets error message
            /// </summary>
            public string ErrorMessage { get; set; }
        }
    }
}