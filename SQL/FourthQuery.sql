select *
from Suppliers
where SupplierID in (
	select distinct SupplierID 
	from Products 
	where CategoryID in (
		select CategoryID 
		from Categories 
		where CategoryName = 'Condiments'
	)
)