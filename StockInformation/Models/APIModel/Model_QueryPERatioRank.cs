//-----------------------------------------------------------------------
// <copyright file="Model_QueryPERatioRank.cs" company="none">
//     Model_QueryPERatioRank for the qpi QueryPERatioRank
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
    public class Model_QueryPERatioRank
    {
        /// <summary>
        /// Request Class
        /// </summary>
        public class Request
        {
            /// <summary>
            ///  Gets or sets date, format must be 'yyyy-MM-dd'
            /// </summary>
            public string Date { get; set; }

            /// <summary>
            ///  Gets or sets number of rank
            /// </summary>
            public int RankNumber { get; set; }
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