<Query Kind="Expression">
  <Connection>
    <ID>0940d1fc-fb1c-41ca-965c-e58dbdcc88e7</ID>
    <Persist>true</Persist>
    <Server>.</Server>
    <Database>WorkSchedule</Database>
    <ShowServer>true</ShowServer>
  </Connection>
</Query>

from item in Skills
	where item.RequiresTicket == true
		select new
		{
			Description = item.Description,
			Employees = from detail in item.EmployeeSkills
						//where Desciption1 = detail.EmployeeSkills.Skill.Description
			orderby detail.YearsOfExperience descending
					select new						
					{
						Name = detail.Employee.FirstName + " "+ detail.Employee.LastName,
						Level = detail.Level,
						YearsExperience = detail.YearsOfExperience							
					}
				}
					
from item in Skills
	orderby item.Description select item.Description 
	
from item in Skills
	where item.EmployeeSkills.Count()==0
		select item.Description


from item in Shifts
	where item.PlacementContract.Location.Name.Contains("NAIT")
	group item by item.DayOfWeek into day
	select new
					{
						Day = day.Key,
						EmployeesNeeded = day.Sum(x=>x.NumberOfEmployees) 
					}
					
from item
in EmployeeSkills where item.YearsOfExperience ==
(from item2 in EmployeeSkills
	select item2.YearsOfExperience).Max()
	select new
					{
						Name = item.Employee.FirstName+ " "+ item.Employee.LastName
					}
	