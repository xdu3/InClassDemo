<Query Kind="Expression">
  <Connection>
    <ID>a4ec4e60-38cc-4562-b7f9-f8c00192e61d</ID>
    <Persist>true</Persist>
    <Server>.</Server>
    <Database>eRestaurant</Database>
  </Connection>
</Query>

//where clause 

//list all table tha hold more then 3 people
//query syntax 
from row in Tables
where row.Capacity >3
select row

//method syntax
Tables.Where(row=>row.Capacity>3)
//List all items with more than 500 calories

from food in Items
where food.Calories >500
select food

//list all items with more than 500 calories and selling for more than 10$
from food in Items
where food.Calories >500 &&
	food.CurrentPrice>10.00m
select food

//List all items with more than 500 calories and are entrees on the menu
//HINT navigational properties of the database are known by LinqPad,
from food in Items
where food.Calories >500 &&
	food.MenuCategory.Description.Equals("Entree")
select food