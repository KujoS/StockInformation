//-----------------------------------------------------------------------
// <copyright file="MaxStrictlyIncreasingRange.cs" company="none">
//     Company copyright tag.
// </copyright>
//-----------------------------------------------------------------------

namespace StockInformation.Models
{
    using System;
    using System.Collections.Generic;
    using System.Linq;
    using System.Reflection;
    using System.Web;

    /// <summary>
    /// 計算資料集裡 最大嚴格遞增的範圍
    /// </summary>
    /// <typeparam name="T">資料 類別</typeparam>
    public class MaxStrictlyIncreasingRange<T>
    {
        /// <summary>
        /// Gets or sets 資料集合 
        /// </summary>
        public IEnumerable<T> Data { get; set; }

        /// <summary>
        /// Gets or sets 嚴格遞增判斷屬性 
        /// </summary>
        public string IncreasingPropertyName { get; set; }

        /// <summary>
        /// function 計算函式
        /// </summary>
        /// <returns>計算 結果</returns>
        public IEnumerable<T> GetRange()
        {
            if (this.IncreasingPropertyName == null)
            {
                throw new Exception("IncreasingPropertyName is null");
            }

            if (this.Data == null || this.Data.Count() == 0)
            {
                throw new Exception("錯誤的資料輸入");
            }

            Type t = typeof(T);
            PropertyInfo propertyInfo = t.GetProperty(this.IncreasingPropertyName);

            if (propertyInfo == null)
            {
                throw new Exception($"{this.IncreasingPropertyName} is not a property of {t.Name}");
            }

            List<T> tempMain = new List<T>();
            List<T> tempSub = new List<T>();

            for (int i = 0; i < this.Data.Count(); i++)
            {
                if (tempSub.Count == 0)
                {
                    tempSub.Add(this.Data.ElementAt(i));
                    continue;
                }

                var lastValue = propertyInfo.GetValue(this.Data.ElementAt(i - 1));
                var value = propertyInfo.GetValue(this.Data.ElementAt(i));

                var selfValueComparer = value as IComparable;
                if (selfValueComparer == null)
                {
                    throw new Exception($"Property {this.IncreasingPropertyName} cannot compare");
                }

                if (selfValueComparer.CompareTo(lastValue) > 0)
                {
                    tempSub.Add(this.Data.ElementAt(i));
                }
                else
                {
                    if (tempSub.Count > tempMain.Count)
                    {
                        tempMain = null;
                        tempMain = tempSub;
                        tempSub = new List<T>();
                        tempSub.Add(this.Data.ElementAt(i));
                    }
                    else
                    {
                        tempSub.Clear();
                        tempSub.Add(this.Data.ElementAt(i));
                    }
                }
            }

            if (tempSub.Count > tempMain.Count)
            {
                return tempSub;
            }
            else
            {
                return tempMain;
            }
        }
    }
}