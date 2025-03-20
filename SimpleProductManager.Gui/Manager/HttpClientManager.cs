using System;
using System.Collections.Generic;
using System.Net.Http;
using System.Text;
using System.Threading.Tasks;
using Microsoft.Extensions.Logging;
using Newtonsoft.Json;
using SimpleProductServices.Model;

namespace SimpleProductManager.Gui.Manager;

public class HttpClientManager(ILogger<HttpClientManager> logger, HttpClient httpClient = null) : IHttpClientManager
{
    private readonly ILogger<HttpClientManager> logger = logger;
    private readonly HttpClient httpClient = httpClient ?? new HttpClient();
    // TODO: get ServerUri from appsettings
    private readonly string ServerUri = "https://localhost:7288";

    // SimpleProductCategory
    public async Task<List<SimpleProductCategoryModel>> GetAllSimpleProductCategoriesAsync()
    {
        try
        {
            return await CallHttpClientAndDeserializeAsync<SimpleProductCategoryModel>(httpClient, ServerUri + "/SimpleProductCategory/GetAll");
        }
        catch (Exception exception)
        {
            logger.LogError(exception, exception.Message); 
        }

        return [];
    }

    public async Task<SimpleProductCategoryModel> AddNewSimpleProductCategoryAsync(string simpleProductCategoryName)
    {
        string service = "/SimpleProductCategory/Add";
        string parameter = "?productCategoryName=";
        try
        {
            string serverUrl = ServerUri + service + parameter + simpleProductCategoryName;

            var jsonPayload = JsonConvert.SerializeObject(simpleProductCategoryName);
            var content = new StringContent(jsonPayload, Encoding.UTF8, "application/json");

            HttpResponseMessage response = await httpClient.PostAsync(serverUrl, content);
            var requestMsg = response.RequestMessage;

            response.EnsureSuccessStatusCode();

            string responseBody = await response.Content.ReadAsStringAsync();
            var result = JsonConvert.DeserializeObject<SimpleProductCategoryModel>(responseBody);

            return result;
        }
        catch (Exception exception)
        {
            logger.LogError(exception, exception.Message);
        }

        return null;
    }

    public async Task RemoveSimpleProductCategoryAsync(Guid simpleProductCategoryId)
    {
        string service = $"/SimpleProductCategory/RemoveByProductCategoryId";
        string parameter = $"?simpleProductCategoryId={simpleProductCategoryId}";

        try
        {
            var response = await httpClient.DeleteAsync(ServerUri + service + parameter);
            response.EnsureSuccessStatusCode();
        }
        catch (Exception exception)
        {
            logger.LogError(exception, exception.Message);
        }
    }


    // SimpleProduct
    public async Task<List<SimpleProductModel>> GetAllSimpleProductAsync()
    {
        string service = "/SimpleProduct/GetAll";
        try
        {
            var response = await httpClient.GetAsync(ServerUri + service);
            response.EnsureSuccessStatusCode();
            var content = await response.Content.ReadAsStringAsync();
            return JsonConvert.DeserializeObject<List<SimpleProductModel>>(content) ?? [];
        }
        catch (Exception exception)
        {
            logger.LogError(exception, exception.Message);
        }

        return [];
    }

    public async Task AddNewSimpleProductAsync(SimpleProductModel simpleProductModel)
    {
        string service = "/SimpleProduct/Add";

        try
        {
            var json = JsonConvert.SerializeObject(simpleProductModel);
            var content = new StringContent(json, Encoding.UTF8, "application/json");

            var response = await httpClient.PostAsync(ServerUri + service, content);
            response.EnsureSuccessStatusCode();
        }
        catch (Exception exception)
        {
            logger.LogError(exception, exception.Message);
        }
    }

    public async Task UpdateSimpleProductAsync(SimpleProductModel simpleProductModel)
    {
        // TODO: update simpleProduct
    }

    public async Task RemoveSimpleProductAsync(Guid simpleProductId)
    {
        string service = $"/SimpleProduct/RemoveBySimpleProductId";
        string parameter = $"?simpleProductId={simpleProductId}";

        try
        {
            var response = await httpClient.DeleteAsync(ServerUri + service + parameter);
            response.EnsureSuccessStatusCode();
        }
        catch (Exception exception)
        {
            logger.LogError(exception, exception.Message);
        }
    }

    private async Task<List<T>> CallHttpClientAndDeserializeAsync<T>(HttpClient httpClient, string url)
    {
        var response = await httpClient.GetAsync(url);
        response.EnsureSuccessStatusCode();
        var content = await response.Content.ReadAsStringAsync();
        return JsonConvert.DeserializeObject<List<T>>(content) ?? new List<T>();
    }
}
