<Query Kind="Expression">
  <Connection>
    <ID>a4ec4e60-38cc-4562-b7f9-f8c00192e61d</ID>
    <Persist>true</Persist>
    <Server>.</Server>
    <Database>eRestaurant</Database>
  </Connection>
</Query>

//order by
from food in Items//food can call anything
 orderby food.Description
 select food 
 // also available decending 
from food in Items
orderby food.CurrentPrice descending
select food
//can use both ascending and descending
from food in Items
orderby food.CurrentPrice descending , food.Calories ascending
select food

// you can use where and orderby together
from food in Items
orderby food.CurrentPrice descending , food.Calories ascending
where food.MenuCategory.Description.Equals("Entree")
select food
//another one 
from food in Items
where food.MenuCategory.Description.Equals("Entree")
orderby food.CurrentPrice descending , food.Calories ascending
select food