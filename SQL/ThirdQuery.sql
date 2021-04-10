select sum(Price)
from Products
where SupplierID in (
	select SupplierID 
	from Suppliers
	where Country = 'USA'
)