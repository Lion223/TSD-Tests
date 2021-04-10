CREATE TABLE [dbo].[Products]
(
	[ProductID] INT NOT NULL PRIMARY KEY, 
    [ProductName] NVARCHAR(50) NOT NULL, 
    [SupplierID] INT NOT NULL, 
    [CategoryID] INT NOT NULL, 
    [Price] DECIMAL (18, 2) NOT NULL
)

CREATE TABLE [dbo].[Categories]
(
	[CategoryID] INT NOT NULL PRIMARY KEY, 
    [CategoryName] NVARCHAR(50) NOT NULL,
    [Description] NVARCHAR(150) NOT NULL
)

CREATE TABLE [dbo].[Suppliers]
(
	[SupplierID] INT NOT NULL PRIMARY KEY, 
    [SupplierName] NVARCHAR(50) NOT NULL,
    [City] NVARCHAR(50) NOT NULL,
	[Country] NVARCHAR(50) NOT NULL
)

INSERT INTO Products (CustomerName, ContactName, Address, City, PostalCode, Country)
VALUES ('Cardinal', 'Tom B. Erichsen', 'Skagen 21', 'Stavanger', '4006', 'Norway');

INSERT INTO Products
	( ProductName, SupplierID, CategoryID, Price )
VALUES
	('Chais', 1, 1, 18.00), 
	('Chang', 1, 1, 19.00), 
	('Aniseed Syrup', 1, 2, 10.00),
	('Chef Anton''s Cajun Seasoning', 2, 2, 22.00),
	('Chef Anton''s Gumbo Mix', 2, 2, 21.35);
  
INSERT INTO Categories
	( CategoryName, Description )
VALUES
	('Beverages', 'Soft drinks, coffees, teas, beers, and ales'), 
	('Condiments', 'Sweet and savory sauces, relishes, spreads, and seasonings'), 
	('Confections', 'Desserts, candies, and sweet breads'),
	('Dairy Products', 'Cheeses'),
	('Grains/Cereals', 'Breads, crackers, pasta, and cereal');
	
INSERT INTO Suppliers
	( SupplierName, City, Country )
VALUES
	('Exotic Liquid', 'London', 'UK'), 
	('New Orleans Cajun Delights', 'New Orleans', 'USA'), 
	('Grandma Kelly''s Homestead', 'Ann Arbor', 'USA'),
	('Tokyo Traders', 'Tokyo', 'Japan'),
	('Cooperativa de Quesos ''Las Cabras''', 'Oviedo', 'Spain');