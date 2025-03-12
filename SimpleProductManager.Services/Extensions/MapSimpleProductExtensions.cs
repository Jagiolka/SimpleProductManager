﻿using SimpleProductManager.Services.Entities;
using SimpleProductServices.Controllers;
using SimpleProductServices.Model;

namespace SimpleProductServices.Extensions;

public static class MapSimpleProductExtensions
{
    public static SimpleProductModel MapToSimpleProductModel(this SimpleProduct simpleProduct) 
    {
        return new SimpleProductModel(
            simpleProduct.Id,
            simpleProduct.Name,
            simpleProduct.Description,
            simpleProduct.Price,
            simpleProduct.SimpleProductCategory.MapToCategoryModel()
        );
    }

    public static SimpleProduct MapToSimpleProduct(this SimpleProductModel simpleProductModel)
    {
        var simpleProductCategory = simpleProductModel.SimpleProductCategory.MapToCategory();

        return new SimpleProduct()
        {
            Id = simpleProductModel.Id,
            Name = simpleProductModel.Name,            
            Description = simpleProductModel.Description,
            Price = simpleProductModel.Price,
            SimpleProductCategory = simpleProductCategory,
            SimpleProductCategoryId = simpleProductCategory.Id,
        };
    }

    public static SimpleProduct MapToSimpleProduct(this SimpleProductInputModel simpleProductInputModel, Guid simpleProductId, SimpleProductCategory simpleProductCategory)
    {
        return new SimpleProduct()
        {
            Id = simpleProductId,
            Name = simpleProductInputModel.SimpleProductName,            
            Description = simpleProductInputModel.SimpleProductDescription,
            Price = simpleProductInputModel.SimpleProductPrice,
            SimpleProductCategory = simpleProductCategory,
            SimpleProductCategoryId = simpleProductCategory.Id,
        };
    }
}
