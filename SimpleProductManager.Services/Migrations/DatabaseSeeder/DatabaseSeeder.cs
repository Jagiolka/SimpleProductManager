using Microsoft.EntityFrameworkCore;
using SimpleProductManager.Services;
using SimpleProductManager.Services.Entities;

namespace SimpleProductServices.Migrations.DatabaseSeeder;

public static class DatabaseSeeder
{
    public static async Task SeedAsync(IServiceProvider serviceProvider, IConfiguration configuration)
    {
        using var scope = serviceProvider.CreateScope();
        var dbContext = scope.ServiceProvider.GetRequiredService<SimpleProductDatabaseContext>();

        await dbContext.Database.MigrateAsync();

        var seedTestData = configuration.GetValue<bool>("DatabaseSeeding:SeedTestData");
        if (!seedTestData)
        {
            return;
        }

        if (await dbContext.SimpleProductCategories.AnyAsync() || await dbContext.SimpleProducts.AnyAsync())
        {
            return;
        }

        var catSmartphones = new SimpleProductCategory
        {
            Id = Guid.NewGuid(),
            Name = "Smartphones"
        };

        var catLaptops = new SimpleProductCategory
        {
            Id = Guid.NewGuid(),
            Name = "Laptops"
        };

        var catAudio = new SimpleProductCategory
        {
            Id = Guid.NewGuid(),
            Name = "Audio"
        };

        var catGaming = new SimpleProductCategory
        {
            Id = Guid.NewGuid(),
            Name = "Gaming"
        };

        var catKitchen = new SimpleProductCategory
        {
            Id = Guid.NewGuid(),
            Name = "Küche & Haushalt"
        };

        dbContext.SimpleProductCategories.AddRange(
            catSmartphones,
            catLaptops,
            catAudio,
            catGaming,
            catKitchen);

        dbContext.SimpleProducts.AddRange(
            new SimpleProduct
            {
                Id = Guid.NewGuid(),
                Name = "iPhone 15 Pro",
                Description = "Titan-Gehäuse, A17 Pro Chip",
                Price = 1199.00m,
                SimpleProductCategoryId = catSmartphones.Id,
                SimpleProductCategory = catSmartphones
            },
            new SimpleProduct
            {
                Id = Guid.NewGuid(),
                Name = "Samsung Galaxy S24",
                Description = "KI-gestütztes Flaggschiff",
                Price = 949.00m,
                SimpleProductCategoryId = catSmartphones.Id,
                SimpleProductCategory = catSmartphones
            },
            new SimpleProduct
            {
                Id = Guid.NewGuid(),
                Name = "Google Pixel 8",
                Description = "Beste Android-Kamera",
                Price = 750.00m,
                SimpleProductCategoryId = catSmartphones.Id,
                SimpleProductCategory = catSmartphones
            },
            new SimpleProduct
            {
                Id = Guid.NewGuid(),
                Name = "MacBook Air M3",
                Description = "13 Zoll, 16GB RAM",
                Price = 1299.00m,
                SimpleProductCategoryId = catLaptops.Id,
                SimpleProductCategory = catLaptops
            },
            new SimpleProduct
            {
                Id = Guid.NewGuid(),
                Name = "Dell XPS 15",
                Description = "OLED Display, i9 Prozessor",
                Price = 1999.99m,
                SimpleProductCategoryId = catLaptops.Id,
                SimpleProductCategory = catLaptops
            },
            new SimpleProduct
            {
                Id = Guid.NewGuid(),
                Name = "Lenovo ThinkPad X1",
                Description = "Business Klassiker",
                Price = 1650.00m,
                SimpleProductCategoryId = catLaptops.Id,
                SimpleProductCategory = catLaptops
            },
            new SimpleProduct
            {
                Id = Guid.NewGuid(),
                Name = "Sony WH-1000XM5",
                Description = "Noise Cancelling Over-Ear",
                Price = 329.00m,
                SimpleProductCategoryId = catAudio.Id,
                SimpleProductCategory = catAudio
            },
            new SimpleProduct
            {
                Id = Guid.NewGuid(),
                Name = "Bose QuietComfort",
                Description = "Legendärer Komfort",
                Price = 280.00m,
                SimpleProductCategoryId = catAudio.Id,
                SimpleProductCategory = catAudio
            },
            new SimpleProduct
            {
                Id = Guid.NewGuid(),
                Name = "AirPods Pro 2",
                Description = "Perfekt für Apple User",
                Price = 249.00m,
                SimpleProductCategoryId = catAudio.Id,
                SimpleProductCategory = catAudio
            },
            new SimpleProduct
            {
                Id = Guid.NewGuid(),
                Name = "PlayStation 5",
                Description = "Digital Edition",
                Price = 449.00m,
                SimpleProductCategoryId = catGaming.Id,
                SimpleProductCategory = catGaming
            },
            new SimpleProduct
            {
                Id = Guid.NewGuid(),
                Name = "Xbox Series X",
                Description = "4K Gaming Power",
                Price = 499.00m,
                SimpleProductCategoryId = catGaming.Id,
                SimpleProductCategory = catGaming
            },
            new SimpleProduct
            {
                Id = Guid.NewGuid(),
                Name = "Nintendo Switch OLED",
                Description = "Handheld Modus optimiert",
                Price = 333.00m,
                SimpleProductCategoryId = catGaming.Id,
                SimpleProductCategory = catGaming
            },
            new SimpleProduct
            {
                Id = Guid.NewGuid(),
                Name = "KitchenAid Artisan",
                Description = "Küchenmaschine",
                Price = 550.00m,
                SimpleProductCategoryId = catKitchen.Id,
                SimpleProductCategory = catKitchen
            },
            new SimpleProduct
            {
                Id = Guid.NewGuid(),
                Name = "Nespresso Vertuo",
                Description = "Kapselmaschine",
                Price = 99.00m,
                SimpleProductCategoryId = catKitchen.Id,
                SimpleProductCategory = catKitchen
            },
            new SimpleProduct
            {
                Id = Guid.NewGuid(),
                Name = "Dyson V15",
                Description = "Kabelloser Sauger",
                Price = 649.00m,
                SimpleProductCategoryId = catKitchen.Id,
                SimpleProductCategory = catKitchen
            },
            new SimpleProduct
            {
                Id = Guid.NewGuid(),
                Name = "Sodastream Duo",
                Description = "Wassersprudler Glas & Plastik",
                Price = 119.00m,
                SimpleProductCategoryId = catKitchen.Id,
                SimpleProductCategory = catKitchen
            },
            new SimpleProduct
            {
                Id = Guid.NewGuid(),
                Name = "Logitech G Pro X",
                Description = "Gaming Headset",
                Price = 129.00m,
                SimpleProductCategoryId = catAudio.Id,
                SimpleProductCategory = catAudio
            },
            new SimpleProduct
            {
                Id = Guid.NewGuid(),
                Name = "Razer Blade 16",
                Description = "Gaming Laptop der Extraklasse",
                Price = 2899.00m,
                SimpleProductCategoryId = catLaptops.Id,
                SimpleProductCategory = catLaptops
            },
            new SimpleProduct
            {
                Id = Guid.NewGuid(),
                Name = "Asus ROG Ally",
                Description = "Gaming Handheld PC",
                Price = 699.00m,
                SimpleProductCategoryId = catGaming.Id,
                SimpleProductCategory = catGaming
            },
            new SimpleProduct
            {
                Id = Guid.NewGuid(),
                Name = "Nothing Phone (2)",
                Description = "Innovatives Design",
                Price = 620.00m,
                SimpleProductCategoryId = catSmartphones.Id,
                SimpleProductCategory = catSmartphones
            });

        await dbContext.SaveChangesAsync();
    }
}