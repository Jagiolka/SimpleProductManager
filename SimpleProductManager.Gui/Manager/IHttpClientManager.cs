using SimpleProductServices.Model;
using System;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace SimpleProductManager.Gui.Manager;

public interface IHttpClientManager
{   
    Task<List<SimpleProductModel>> GetAllSimpleProductAsync();
    Task AddNewSimpleProductAsync(SimpleProductModel simpleProductModel);
    Task UpdateSimpleProductAsync(SimpleProductModel simpleProductModel);
    Task RemoveSimpleProductAsync(Guid simpleProductId);

    Task<List<SimpleProductCategoryModel>> GetAllSimpleProductCategoriesAsync();
    Task<SimpleProductCategoryModel> AddNewSimpleProductCategoryAsync(string simpleProductCategoryName);
    Task RemoveSimpleProductCategoryAsync(Guid simpleProductCategoryId);
}