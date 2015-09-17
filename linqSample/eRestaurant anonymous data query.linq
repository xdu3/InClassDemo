<Query Kind="Expression">
  <Connection>
    <ID>a4ec4e60-38cc-4562-b7f9-f8c00192e61d</ID>
    <Persist>true</Persist>
    <Server>.</Server>
    <Database>eRestaurant</Database>
  </Connection>
</Query>

//anonymous data type queries
from food in Items
where food.MenuCategory.Description.Equals("Entree")
	&& food.Active
orderby food.CurrentPrice descending
select new
	{
		Description = food.Description,
		Price = food.CurrentPrice,
		Cost = food.CurrentCost,
		profit = food.CurrentPrice - food.CurrentCost
	}
	
//diffient
from food in Items
where food.MenuCategory.Description.Equals("Entree")
	&& food.Active
orderby food.CurrentPrice descending
select new
	{
		food.Description,
		food.CurrentPrice,
		food.CurrentCost,
		//profit = food.CurrentPrice - food.CurrentCost
	}
	
	
//...	
from food in Items
where food.MenuCategory.Description.Equals("Entree")
	&& food.Active
orderby food.CurrentPrice descending
select new //POCOObjectName
	{
		Description = food.Description,
		Price = food.CurrentPrice,
		Cost = food.CurrentCost,
		profit = food.CurrentPrice - food.CurrentCost
	}