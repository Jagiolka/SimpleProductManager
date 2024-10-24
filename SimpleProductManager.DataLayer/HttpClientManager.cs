using System.Net.Http.Json;
using Microsoft.Extensions.Logging;
using Newtonsoft.Json;

namespace SimpleProductManager.Data;

public class HttpClientManager : IHttpClientManager
{
    private readonly ILogger<HttpClientManager> logger;
    private readonly HttpClient httpClient;

    public HttpClientManager(ILogger<HttpClientManager> logger, HttpClient httpClient = null)
    {
        this.logger = logger;
        this.httpClient = httpClient ?? new HttpClient();
    }

    public async Task<List<SimpleProductStockModel>> GetSimpleProductStockAsync()
    {
        try
        {
            var response = await this.httpClient.GetAsync("https://localhost:7288/SimpleProduct/GetAll");
            response.EnsureSuccessStatusCode();
            var content = await response.Content.ReadAsStringAsync();
            return JsonConvert.DeserializeObject<List<SimpleProductStockModel>>(content);
        }
        catch (Exception exception)
        {
            logger.LogError(exception, exception.Message);
        }

        return new List<SimpleProductStockModel>();
    }

    public async Task AddSimpleProductAsync(SimpleProductStockModel simpleProductStockModel)
    {
        try
        {
            var response = await this.httpClient.PostAsJsonAsync("https://localhost:7288/SimpleProduct/Add", simpleProductStockModel);
        }
        catch (Exception exception)
        {
            logger.LogError(exception, exception.Message);
        }
    }

    public async Task RemoveProductStockAsync(SimpleProductStockModel simpleProductStockModel)
    {
        //TODO: Task < HttpResponseMessage >
         var result = await this.httpClient.DeleteAsync($"https://localhost:7288/SimpleProduct/Remove/{simpleProductStockModel.SimpleProductModelId}");
    }

    public async Task<List<ProductCategoryModel>> GetProductCategoriesAsync()
    {
        try
        {
            var response = await this.httpClient.GetAsync("https://localhost:7288/SimpleProduct/GetProductCategories");
            response.EnsureSuccessStatusCode();
            var content = await response.Content.ReadAsStringAsync();

            return JsonConvert.DeserializeObject<List<ProductCategoryModel>>(content) ?? [];
        }
        catch(Exception exception) 
        {
            logger.LogError(exception, exception.Message);
        }

        return [];
    }
}
