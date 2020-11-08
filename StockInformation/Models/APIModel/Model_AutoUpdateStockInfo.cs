//-----------------------------------------------------------------------
// <copyright file="Model_AutoUpdateStockInfo.cs" company="none">
//     Model_AutoUpdateStockInfo for the qpi AutoUpdateStockInfo
//     Company copyright tag.
// </copyright>
//-----------------------------------------------------------------------

namespace StockInformation.Models.APIModel
{
    using System;
    using System.Collections.Generic;
    using System.Linq;
    using System.Web;

    /// <summary>
    /// API Model
    /// </summary>
    public class Model_AutoUpdateStockInfo
    {
        /// <summary>
        /// Request Class
        /// </summary>
        public class Request
        {
            /// <summary>
            ///  Gets or sets date, format must be 'yyyy-MM-dd'
            /// </summary>
            public string StartDate { get; set; }

            /// <summary>
            ///  Gets or sets number of rank
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
            /// Gets or sets error message
            /// </summary>
            public string ErrorMessage { get; set; }
        }
    }
}