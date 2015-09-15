<Query Kind="Program" />

void Main()
{
	//simple concatenation C#
	//"hello world"
	//5+7
	
	//simple C# statements
	//string name = "xin";
	//string message = "hello " +name +".";
	//message.Dump();
	
	//simple c# program
	SayHello("xin");
}

// Define other methods and classes here
public void SayHello(string name)
{
	string message= "hello " +name;
	message.Dump();
}