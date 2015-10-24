<Query Kind="Expression">
  <Connection>
    <ID>a4ec4e60-38cc-4562-b7f9-f8c00192e61d</ID>
    <Persist>true</Persist>
    <Server>.</Server>
    <Database>eRestaurant</Database>
  </Connection>
</Query>

from abillrow in Bills
where abillrow.BillDate.Month == 5
orderby abillrow.BillDate,
		abillrow.Waiter.LastName,
		abillrow.Waiter.FirstName
select new
	{
		BillDate = new DateTime(abillrow.BillDate.Year,
								abillrow.BillDate.Month,
								abillrow.BillDate.Day),
		WaiterName = abillrow.Waiter.LastName+ ", "+abillrow.Waiter.FirstName,
		BillID = abillrow.BillID,
		BillTotal = abillrow.BillItems.Sum(eachbillitemrow =>
			eachbillitemrow.Quantity * eachbillitemrow.SalePrice),
		PartySize = abillrow.NumberInParty,
		Contact = abillrow.Reservation.CustomerName
	}