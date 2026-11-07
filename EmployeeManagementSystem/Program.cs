using System;
using System.Collections.Generic;
using System.Linq;

public class Employee {
	public int Id {get; private set;}
	public string Name {get; private set;}
	public decimal Salary {get; private set;}
	public string Department {get; private set;}

	public Employee (int id, string name, decimal salary, string department) {
		(Id, Name, Salary, Department) = (id, name, salary, department);
	}
	
	public virtual void DisplayInformation() {
		Console.WriteLine($"Employee [ID: {Id}] {Name}");
		Console.WriteLine($"Salary: {Salary}");
		Console.WriteLine($"Department: {Department}");
	}

	public virtual void Work() {
		Console.WriteLine($"{Name} is working.");
	}

	public decimal CalculateAnnualSalary() {
		return Salary*12;
	}
}

public class Developer : Employee {
	public string ProgrammingLanguage {get; private set;}
	public int YearsOfExperience {get; private set;}
	
	public Developer (int id, string name, decimal salary, string department, string programmingLanguage, int yearsOfExperience) : base(id, name, salary, department) {
		(ProgrammingLanguage, YearsOfExperience) = (programmingLanguage, yearsOfExperience);
	}
	
	public override void DisplayInformation() {
		base.DisplayInformation();
		Console.WriteLine($"Programming Language: {ProgrammingLanguage}");
		Console.WriteLine($"Years Of Experience: {YearsOfExperience}");
	}

	public override void Work () {
		Console.WriteLine($"{Name} is developing software using {ProgrammingLanguage}.");
	}
}

public class Designer : Employee {
	public string DesignTool {get; private set;}
	public string Specialization {get; private set;}
	
	public Designer (int id, string name, decimal salary, string department, string designTool, string specialization) : base(id, name, salary, department) {
		(DesignTool, Specialization) = (designTool, specialization);
	}
	
	public override void DisplayInformation () {
		base.DisplayInformation();
		Console.WriteLine($"Design Tool: {DesignTool}");
		Console.WriteLine($"Specialization: {Specialization}");
	}

	public override void Work () {
		Console.WriteLine($"{Name} is designing interfaces using {DesignTool}.");
	}
}


public class Program {
	public static void Main (string[] args) {
		Console.WriteLine("Welcome to Company's Employee Management System.");
		Console.WriteLine("Please type your order in format below.");
		
		for (int i=1; i<=50; i++) Console.Write('-');
		Console.WriteLine();

		Console.WriteLine("\"Create\" - Create & keying new Employee's information.");
		Console.WriteLine("\"Work [Id]\" - Showing Employee [id]'s work.");
		Console.WriteLine("\"Remove [Id]\" - Remove Employee [Id] from the system.");
		Console.WriteLine("\"Print [Id]\" - Output Employee [Id]'s Information.");
		Console.WriteLine("\"Print All\" - Output all Employee's Information.");
		Console.WriteLine("\"Exit\" - Exit the program.");

		for (int i=1; i<=50; i++) Console.Write('-');
		Console.WriteLine();
		
		string? commandInput = "";

		// Well, input validations may not be that detailed since it's merely a barebone to test my OOP concept program.

		SortedDictionary<int,Employee>employeeList = new SortedDictionary<int,Employee>();

		while (true) {
			Console.Write("Enter your command: ");

			commandInput = Console.ReadLine();
			
			for (int i=0; i<50; i++) Console.Write('-');
			Console.WriteLine();

			if (commandInput == null) {
				Console.WriteLine("Invalid command, please try again. (A command cannot be blank!)");
			}
			
			else if (commandInput == "Exit") {
				break;
			}
			
			else if (commandInput == "Create") {
				Console.WriteLine("Which role is your employee?");
				Console.WriteLine("[1] - Developer");
				Console.WriteLine("[2] - Designer");

				string? choice = Console.ReadLine();
				
				if (choice == null || (choice != "1" && choice != "2")) {
					Console.Write("Invalid Input! Please try again.");
					
					if (choice == null) Console.WriteLine("(Role option cannot be blank!)");
					else Console.WriteLine("(Role option must be either 1 or 2!)");

					continue;
				}

				Console.Write("ID: ");
				
				if (!int.TryParse(Console.ReadLine() ?? " ", out int id)) {
					Console.WriteLine("Invalid Input! Please try again. (ID must be an integer.)");
					continue;
				}
				
				if (employeeList.ContainsKey(id)) {
					Console.WriteLine("There's already an employee with this Id, please try again.");
					continue;
				}

				Console.Write("Name: ");
				string? name = Console.ReadLine();
				
				if (name == null) {
					Console.WriteLine("Invalid input, please try again. (A name cannot be blank!)");
					continue;
				}
				
				Console.Write("Salary: ");
				
				if (!decimal.TryParse(Console.ReadLine() ?? " ",out decimal salary)) {
					Console.WriteLine("Invalid Input! Please try again. (Salary must be a decimal.)");
					continue;
				}
				
				Console.Write("Department: ");
				string? department = Console.ReadLine();
				
				if (department == null) {
					Console.WriteLine("Invalid input, please try again. (A department cannot be blank!)");
					continue;
				}

				//Developer employee
				if (choice == "1") {
					Console.Write("Programming Language: ");
					string? programmingLanguage = Console.ReadLine();

					if (programmingLanguage == null) {
						Console.WriteLine("Invalid input, please try again. (A programming language cannot be blank!)");
						continue;
					}
					
					Console.Write("Years Of Experiences: ");
					
					if (!int.TryParse(Console.ReadLine() ?? " ", out int yearsOfExperience)) {
						Console.WriteLine("Invalid Input! Please try again. (Years of Experiences must be an integer.)");
						continue;
					}

					Employee employee = new Developer (id, name, salary, department, programmingLanguage, yearsOfExperience);				
					employeeList.Add(id,employee);
				}
				else {
					Console.Write("Design Tool: ");
					string? designTool = Console.ReadLine();
					
					if (designTool == null) {
						Console.WriteLine("Invalid Input! Please try again. (Design tool cannot be blank!)");
						continue;
					}

					Console.Write("Specialization: ");
					string? specialization = Console.ReadLine();
					
					if (specialization == null) {
						Console.WriteLine("Invalid Input! Please try again. (Specialization cannot be blank!)");
						continue;
					}

					Employee employee = new Designer (id, name, salary, department, designTool, specialization);			
					employeeList.Add(id,employee);
				}
			}
			
			else if (commandInput == "Print All") {
				int count = employeeList.Count-1;
		        
				if (employeeList.Count == 0) Console.WriteLine("There is no employee yet, please try again later.");

				foreach (var employee in employeeList) {
					Employee currentEmployee = employee.Value;

					currentEmployee.DisplayInformation();
					if (count > 0) {	
						for (int i=0; i<30; i++) Console.Write('-');
						Console.WriteLine();
					}
					--count;
                }
			}
			else {
                string[] formattedInput = (commandInput.Trim()).Split(' ');
                
                if (formattedInput.Length != 2 || formattedInput[0].Where(x => (x >= 'a' && x <= 'z') || (x >= 'A' && x <= 'Z')).Count() < 5) {
                    Console.WriteLine("Not a valid command, please try again.");
                    continue;
                }
                
                string order = formattedInput[0];
                
				if (!int.TryParse(formattedInput[1],out int id))
				{
					Console.WriteLine("Invalid Input! Please try again. (ID must be an integer.)");
					continue;
				}
                
                if (order == "Print") {
                    if (employeeList.ContainsKey(id)) employeeList[id].DisplayInformation();
                    else Console.WriteLine("There's no employee with this Id, please try again later.");
                }
                else if (order == "Remove") {
                    if (employeeList.ContainsKey(id)) {
                        employeeList.Remove(id);
                        Console.WriteLine("Removal Completed.");
                    }
                    else Console.WriteLine("There's no employee with this Id, please try again later.");
                }
                else if (order == "Work") {
                    if (employeeList.ContainsKey(id)) employeeList[id].Work();
                    else Console.WriteLine("There's no employee with this Id, please try again later.");
                }
                else {
    				Console.WriteLine("Not a valid command, please try again.");
    				continue;
                }
			}
			
			for (int i=0; i<50; i++) Console.Write('-');
			Console.WriteLine();
		}

		Console.WriteLine("System exited.");
	}
}