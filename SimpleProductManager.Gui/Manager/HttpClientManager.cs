using System;
using System.Collections.Generic;
using System.Net.Http;
using System.Text;
using System.Threading.Tasks;
using Microsoft.Extensions.Logging;
using Newtonsoft.Json;
using SimpleProductServices.Model;

namespace SimpleProductManager.Gui.Manager;

public class HttpClientManager(ILogger<HttpClientManager> logger, HttpClient? httpClient = null) : IHttpClientManager
{
    private readonly HttpClient httpClient = httpClient ?? new HttpClient();
    private readonly string serverUri = "https://localhost:7288";

    // SimpleProductCategory
    public async Task<(string errorMessage, List<SimpleProductCategoryModel>)> GetAllSimpleProductCategoriesAsync()
    {
        var service = "/SimpleProductCategory/GetAll";
        var errorMessage = string.Empty;
        try
        {
            return (errorMessage, await this.CallGetHttpClientAndDeserializeAsync<SimpleProductCategoryModel>(httpClient, serverUri + service));
        }
        catch (Exception exception)
        {
            logger.LogError(exception, exception.Message);
            errorMessage = exception.Message;
        }

        return (errorMessage, []);
    }

    public async Task<(string errorMessage, SimpleProductCategoryModel? result)> AddNewSimpleProductCategoryAsync(string simpleProductCategoryName)
    {
        var service = "/SimpleProductCategory/Add";
        var parameter = "?productCategoryName=";
        var errorMessage = string.Empty;
        
        try
        {
            string serverUrl = serverUri + service + parameter + simpleProductCategoryName;

            var jsonPayload = JsonConvert.SerializeObject(simpleProductCategoryName);
            var content = new StringContent(jsonPayload, Encoding.UTF8, "application/json");

            HttpResponseMessage response = await httpClient.PostAsync(serverUrl, content);
            var requestMsg = response.RequestMessage;

            response.EnsureSuccessStatusCode();

            var responseBody = await response.Content.ReadAsStringAsync();
            var result = JsonConvert.DeserializeObject<SimpleProductCategoryModel>(responseBody);

            return (errorMessage, result);
        }
        catch (Exception exception)
        {
            logger.LogError(exception, exception.Message);
            errorMessage = exception.Message;
        }

        return (errorMessage, null);
    }

    public async Task RemoveSimpleProductCategoryAsync(Guid simpleProductCategoryId)
    {
        var service = $"/SimpleProductCategory/RemoveByProductCategoryId";
        var parameter = $"?simpleProductCategoryId={simpleProductCategoryId}";

        try
        {
            var response = await httpClient.DeleteAsync(serverUri + service + parameter);
            response.EnsureSuccessStatusCode();
        }
        catch (Exception exception)
        {
            logger.LogError(exception, exception.Message);
        }
    }


    // SimpleProduct
    public async Task<(string errorMessage, List<SimpleProductModel>)> GetAllSimpleProductAsync()
    {
        var service = "/SimpleProduct/GetAll";
        var errorMessage = string.Empty;
        try
        {
            var response = await httpClient.GetAsync(serverUri + service);
            response.EnsureSuccessStatusCode();
            var content = await response.Content.ReadAsStringAsync();
            return (errorMessage, JsonConvert.DeserializeObject<List<SimpleProductModel>>(content) ?? []);
        }
        catch (Exception exception)
        {
            logger.LogError(exception, exception.Message);
            errorMessage = exception.Message;
        }
        
        return (errorMessage, []);       
    }

    public async Task<string> AddNewSimpleProductAsync(SimpleProductModel simpleProductModel)
    {
        var service = "/SimpleProduct/Add";
        var errorMessage = string.Empty;

        try
        {
            var json = JsonConvert.SerializeObject(simpleProductModel);
            var content = new StringContent(json, Encoding.UTF8, "application/json");

            var response = await httpClient.PostAsync(serverUri + service, content);
            response.EnsureSuccessStatusCode();
        }
        catch (Exception exception)
        {
            logger.LogError(exception, exception.Message);
            errorMessage = exception.Message;
        }
        
        return errorMessage;
    }

    public async Task<string> UpdateSimpleProductAsync(SimpleProductModel simpleProductModel)
    {
        string errorMessage = string.Empty;
        return errorMessage;
        // TODO: update simpleProduct
    }

    public async Task<string> RemoveSimpleProductAsync(Guid simpleProductId)
    {
        var service = $"/SimpleProduct/RemoveBySimpleProductId";
        var parameter = $"?simpleProductId={simpleProductId}";
        string errorMessage = string.Empty;
        
        try
        {
            var response = await httpClient.DeleteAsync(serverUri + service + parameter);
            response.EnsureSuccessStatusCode();
        }
        catch (Exception exception)
        {
            logger.LogError(exception, exception.Message);
            errorMessage = exception.Message;
        }
        
        return errorMessage;
    }

    private async Task<List<T>> CallGetHttpClientAndDeserializeAsync<T>(HttpClient httpClient, string url)
    {
        var response = await httpClient.GetAsync(url);
        response.EnsureSuccessStatusCode();
        var content = await response.Content.ReadAsStringAsync();
        return JsonConvert.DeserializeObject<List<T>>(content) ?? [];
    }
}
