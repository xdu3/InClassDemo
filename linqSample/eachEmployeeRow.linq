<Query Kind="Statements">
  <Connection>
    <ID>fa10415d-1f82-4d86-811d-fbc33a1e1b61</ID>
    <Persist>true</Persist>
    <Server>.</Server>
    <Database>WorkSchedule</Database>
    <ShowServer>true</ShowServer>
  </Connection>
</Query>

//list of employees and their YearsOfExperience
//sum the rows containing the YOE for an employee 
var employeeYOE = from eachEmployeerow in Employees
select new 
{
	Name = eachEmployeerow.FirstName + " "+ eachEmployeerow.LastName,
	YOE = eachEmployeerow.EmployeeSkills.Sum(
			eachEmployeeSkillrow => eachEmployeeSkillrow.YearsOfExperience)
};
employeeYOE.Dump();
//from the list of employeesYOE find the Max()

var MaxYOE = employeeYOE.Max(eachEYOErow => eachEYOErow.YOE);
MaxYOE.Dump();

//using employeesYOE and YOEMax create a final list of most experiences employees

var fianlresult = from eachEYOErow in employeeYOE
					where compare eachEYOE to the max value
					select 
						name