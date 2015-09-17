<Query Kind="Statements">
  <Connection>
    <ID>a4ec4e60-38cc-4562-b7f9-f8c00192e61d</ID>
    <Persist>true</Persist>
    <Server>.</Server>
    <Database>eRestaurant</Database>
  </Connection>
</Query>

//simpliest
//Waiters


//simple query syntax
from person in Waiters select person 

//simple method syntax
Waiters.Select(person=>person)

//inside our project we will be writting c# statement
var results = from person in Waiters 
	select person;

//to dispaly the contents of a variable in Linqpad
//use the .Dump() method 
results.Dump();

//implemented inside a VS project's class library BLL method
//[DataObjectMethod(DataObjectMethodType.Select,false)]
//public List<Waiters>SomeMedthodName()
//{
	// you will need to connect to your DAL object 
	//this will be done using a new xxxxxx() constructor 
	//assume your connection variable is called connectvariable
	
	//do your query
	//var results = from person in Connectionvariable.Waiters
		//select person;
	//return your results
	//return results.ToList();
//}