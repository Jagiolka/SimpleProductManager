using SimpleProductServices.Model;
using System;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace SimpleProductManager.Gui.Manager;

public interface IHttpClientManager
{   
    Task<(string errorMessage, List<SimpleProductModel>)> GetAllSimpleProductAsync();
    Task<string> AddNewSimpleProductAsync(SimpleProductModel simpleProductModel);
    Task<string> UpdateSimpleProductAsync(SimpleProductModel simpleProductModel);
    Task<string> RemoveSimpleProductAsync(Guid simpleProductId);

    Task<(string errorMessage, List<SimpleProductCategoryModel>)> GetAllSimpleProductCategoriesAsync();
    Task<(string errorMessage, SimpleProductCategoryModel? result)> AddNewSimpleProductCategoryAsync(
        string simpleProductCategoryName);
    Task RemoveSimpleProductCategoryAsync(Guid simpleProductCategoryId);
}