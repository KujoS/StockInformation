//-----------------------------------------------------------------------
// <copyright file="Model_UploadStockInfo.cs" company="none">
//     Model_UploadStockInfo for the qpi UploadStockInfo
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
    public class Model_UploadStockInfo
    {
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