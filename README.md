# Project - StockInformation

# 個股日本益比、殖利率及股價淨值比 功能實作 
## 開發工具 
* VS 2017 版本15.9.16
* Sqlite 版本3.32.2
* ASP.NET MVC 5
* 相關套件: Dapper, Newtonsoft.Json, system.data.sqlite.core

# 實作功能
利用[台灣證券交易](https://www.twse.com.tw/zh/page/trading/exchange/BWIBBU_d.html)所提供的資料 實作以下功能

* 依照證券代號 搜尋最近n天的資料
* 指定特定日期 顯示當天本益比前n名
* 指定日期範圍、證券代號 顯示這段時間內殖利率 為嚴格遞增的最長天數並顯示開始、結束日期
* 上傳資料
* 自動匯入資料： 利用台灣證券交易的固定下載網址格式匯入資料至Database，可省略上傳檔案的操作

## Feature API
 * QueryStockInfo - 依照證券代號 搜尋最近n天的資料
  input sample
    ```json
    {
        "code": "1101",
        "lastdays": 5
    }
    ```
    output sample
    ```json
    {
        "result": true,
        "info":[
            {
                "Code": "1101",
                "Date": "2020-11-06",
                "Name": "台泥",
                "Dividend_yield": 7.35,
                "Devidend_year": 108,
                "PE_rate": 9.63,
                "PB_rate": 1.26,
                "Fiscal_year_quarter": "109/2"
            },
            {
                "Code": "1101",
                "Date": "2020-11-07",
                "Name": "台泥",
                "Dividend_yield": 7.6,
                "Devidend_year": 108,
                "PE_rate": 10.23,
                "PB_rate": 1.03,
                "Fiscal_year_quarter": "109/2"
            }, ...
        ] ,
        "errormessage": null
    }
    ```

 * QueryPERatioRank - 指定特定日期 顯示當天本益比前n名
      input sample
    ```json
    {
        "date": "2020-11-06",
        "lastdays": 5
    }
    ```
    output sample
    ```json
    {
        "Result": true,
        "Info": [
            {
                "Code": "1103",
                "Date": "2020-11-06",
                "Name": "嘉泥",
                "Dividend_yield": 6.31,
                "Devidend_year": 108,
                "PE_rate": 20.32,
                "PB_rate": 0.48,
                "Fiscal_year_quarter": "109/2"
            },
            {
                "Code": "1101",
                "Date": "2020-11-06",
                "Name": "台泥",
                "Dividend_yield": 7.35,
                "Devidend_year": 108,
                "PE_rate": 9.63,
                "PB_rate": 1.26,
                "Fiscal_year_quarter": "109/2"
            }, ...
        ],
        "ErrorMessage": null
    }
    ```

 * QueryDividendYieldIncreasingDay - 指定日期範圍、證券代號 顯示這段時間內殖利率 為嚴格遞增的最長天數並顯示開始、結束日期
    * 運算邏輯：利用兩個List來記錄目前遞增的集合，與已記錄最大的集合，最後較大的集合即為答案。
      如果有相同大小的最大集合則回傳第一個集合
  
    input sample
    ```json
    {
        "code": "1101",
        "startdate": "2020-11-01",
        "enddate": "2020-11-30"
    }
    ```
    output sample
    ```json
    {
        "Result": true,
        "StartDate": "2020-11-05",
        "EndDate": "2020-11-07",
        "Days": 3,
        "ErrorMessage": null
    }
    ```
# 資料匯入
* QueryPERatioRank - 上傳CSV檔

    output sample
    ```json
    {
        "Result": true,
        "ErrorMessage": null
    }
    ```
    
* AutoUpdateStockInfo - 自動從遠端更新資料，利用台灣證券交易所固定的下載網址來匯入資料
https://www.twse.com.tw/exchangeReport/BWIBBU_d?response=csv&date={date}&selectType=ALL
週末沒資料的日期，會顯示"讀取檔案錯誤"
 
   input sample
    ```json
    {
        "startdate" : "2020-11-01",
        "enddate" : "2020-11-08"
    }
    ```
    output sample
    ```json
    {
        "Result": true,
        "ErrorMessage": "20201101-讀取檔案錯誤, 20201102-檔案上傳成功, 20201103-檔案上傳成功, 20201104-檔案上傳成功, 20201105-檔案上傳成功, 20201106-檔案上傳成功, 20201107-讀取檔案錯誤, 20201108-讀取檔案錯誤, "
    }
    ```


