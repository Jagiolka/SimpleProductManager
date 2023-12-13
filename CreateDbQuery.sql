CREATE TABLE dbo.ProductCategory (
    Id uniqueidentifier PRIMARY KEY,
    Name TEXT NOT NULL
);

CREATE TABLE dbo.SimpleProduct (
    Id uniqueidentifier PRIMARY KEY,
    Name VARCHAR(255) NOT NULL
);

CREATE TABLE dbo.SimpleProductStock (
    SimpleProductId uniqueidentifier NOT NULL,
    Quantity INTEGER NOT NULL,
    FOREIGN KEY (SimpleProductId) REFERENCES SimpleProduct (id)
);

CREATE TABLE dbo.SimpleProductCategory (
    SimpleProductId uniqueidentifier NOT NULL,
    ProductCategoryId VARCHAR(255) NOT NULL,
    PRIMARY KEY (SimpleProductId, ProductCategoryId),
    FOREIGN KEY (SimpleProductId) REFERENCES SimpleProduct (id),
    FOREIGN KEY (ProductCategoryId) REFERENCES ProductCategory (id)
);