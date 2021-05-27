using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System.Net.Http;
using System.Net.Http.Headers;
using System.Threading.Tasks;

public class HttpOperation
{
    private Config config = new Config();
    public bool isAsyncOver;
    public bool isAsyncOver2;
    private readonly HttpClient httpclient = new HttpClient();
    public string responsedHTTPHead;
    public string responseData;
    public string feebBackData;
    private string lightOpration = "on";





    public async Task<string> getDevice()
    {
        string url = config.baseURL + "/1/devices";
        string token = "Bearer " + config.accessToken;
        httpclient.DefaultRequestHeaders.Add("Authorization", token);

        isAsyncOver = false;
        var responseMessage = await httpclient.GetAsync(url);
        responseData = responseMessage.Content.ReadAsStringAsync().Result;
        isAsyncOver = true;
        // 成功输出http头 Debug.Log(responseMessage);
        // 成功输出设备信息 Debug.Log(responseData);
        //Debug.Log(responseData);
        // 还不能用

        return responseData;
    }

    public async Task<string> getappliances()
    {
        string url = config.baseURL + "/1/appliances";
        string token = "Bearer " + config.accessToken;
        httpclient.DefaultRequestHeaders.Add("Authorization", token);

        isAsyncOver2 = false;
        var responseMessage = await httpclient.GetAsync(url);
        feebBackData = responseMessage.Content.ReadAsStringAsync().Result;
        isAsyncOver2 = true;
        return feebBackData;
    }

    public async void updateSetting()
    {
        string url = config.baseURL + "/1/appliances/" + config.airConID + "/aircon_settings";
        string token = "Bearer " + config.accessToken;
        var content = new FormUrlEncodedContent(new Dictionary<string, string>()
        {
            {"temperature", "24"},
            {"operation_mode", "warm"},
            {"button","power-on" }
        });
        httpclient.DefaultRequestHeaders.Add("Authorization", token);

        var responseMessage = await httpclient.PostAsync(url, content);
        Debug.Log(responseMessage);
        responsedHTTPHead = responseMessage.Content.ReadAsStringAsync().Result;
        isAsyncOver = true;
    }

    public async void lightControl()
    {
        string url = config.baseURL + "/1/appliances/" + config.lightID + "/light";
        string token = "Bearer " + config.accessToken;
        var content = new FormUrlEncodedContent(new Dictionary<string, string>()
        {
            {"button", lightOpration},
        });
        httpclient.DefaultRequestHeaders.Add("Authorization", token);

        var responseMessage = await httpclient.PostAsync(url, content);
        Debug.Log(responseMessage);
        responsedHTTPHead = responseMessage.Content.ReadAsStringAsync().Result;
        isAsyncOver = true;
    }
}
