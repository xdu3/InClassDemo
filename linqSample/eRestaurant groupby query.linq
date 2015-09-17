<Query Kind="Expression">
  <Connection>
    <ID>a4ec4e60-38cc-4562-b7f9-f8c00192e61d</ID>
    <Persist>true</Persist>
    <Server>.</Server>
    <Database>eRestaurant</Database>
  </Connection>
</Query>

//groupby

from food in Items 
group food by food.MenuCategory.Description

//this creates a key with a value and the row collection for that key value

//more then on field 
from food in Items
 group food by new {food.MenuCategory.Description, food.CurrentPrice}