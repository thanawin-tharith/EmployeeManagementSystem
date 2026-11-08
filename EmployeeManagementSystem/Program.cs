using System;
using System.Collections.Generic;
using System.Linq;

public class Employee {
	public int Id {get; private set;}
	public string Name {get; private set;}
	public decimal Salary {get; private set;}
	public string Department {get; private set;}
	public virtual int PropertyCount {get; private set;} = 3;

	public Employee (int id, string name, decimal salary, string department) {
		(Id, Name, Salary, Department) = (id, name, salary, department);
	}

	public virtual void DisplayInformation() {
		Console.WriteLine($"Id: {Id}");
		Console.WriteLine($"Employee: {Name}");
		Console.WriteLine($"Salary: {Salary}");
		Console.WriteLine($"Department: {Department}");
	}

	public virtual void Work() {
		Console.WriteLine($"{Name} is working.");
	}

	private decimal CalculateAnnualSalary() {
		return Salary*12;
	}

	public void DisplaySalary () {
		Console.WriteLine($"Name: {Name}");
		Console.WriteLine($"Salary: {Salary}");
		Console.WriteLine($"Annual Salary: {CalculateAnnualSalary()}");
	}

	public virtual void DisplayUpdateChoice () {
		Console.WriteLine("Which data are you updating?");
		Console.WriteLine("[1] - Name");
		Console.WriteLine("[2] - Salary");
		Console.WriteLine("[3] - Department");
	}
}

public class Developer : Employee {
	public string ProgrammingLanguage {get; private set;}
	public int YearsOfExperience {get; private set;}
	public override int PropertyCount => 5;
	
	public Developer (int id, string name, decimal salary, string department, string programmingLanguage, int yearsOfExperience) : base(id, name, salary, department) {
		(ProgrammingLanguage, YearsOfExperience) = (programmingLanguage, yearsOfExperience);
	}
	
	public override void DisplayInformation() {
		base.DisplayInformation();
		Console.WriteLine($"Programming Language: {ProgrammingLanguage}");
		Console.WriteLine($"Years Of Experience: {YearsOfExperience}");
	}

	public override void DisplayUpdateChoice () {
		base.DisplayUpdateChoice();
		Console.WriteLine("[4] - Programming Language");
		Console.WriteLine("[5] - Years of Experience");
	}

	public override void Work () {
		Console.WriteLine($"{Name} is developing software using {ProgrammingLanguage}.");
	}
}

public class Designer : Employee {
	public string DesignTool {get; private set;}
	public string Specialization {get; private set;}
	public override int PropertyCount => 5;
	public Designer (int id, string name, decimal salary, string department, string designTool, string specialization) : base(id, name, salary, department) {
		(DesignTool, Specialization) = (designTool, specialization);
	}
	
	public override void DisplayInformation () {
		base.DisplayInformation();
		Console.WriteLine($"Design Tool: {DesignTool}");
		Console.WriteLine($"Specialization: {Specialization}");
	}
	public override void DisplayUpdateChoice () {
		base.DisplayUpdateChoice();
		Console.WriteLine("[4] - Design Tool");
		Console.WriteLine("[5] - Specialization");
	}

	public override void Work () {
		Console.WriteLine($"{Name} is designing interfaces using {DesignTool}.");
	}
}


public class Program {
	public static bool LoopGetInt (out int intInput, string text, string operation) {
		Console.Write($"{text}: ");
		
		intInput = -1;
		
		while (!int.TryParse(Console.ReadLine() ?? " ", out intInput) || intInput < 0) {
			Console.WriteLine($"Invalid Input! ({text} must be a non-negative integer.)");

			Console.WriteLine("Would you like to try again? (Y/N)");
			string? tryAgain = Console.ReadLine();

			if (tryAgain != "Y") {
				Console.WriteLine($"Cancelled {operation}.");
				return false;
			}
	
			Console.Write($"{text}: ");
		}
		
		return true;
	}

	public static bool LoopGetDecimal (out decimal decimalInput, string text, string operation) {
		Console.Write($"{text}: ");
		
		decimalInput = -1;
		
		while (!decimal.TryParse(Console.ReadLine() ?? " ", out decimalInput) || decimalInput < 0) {
			Console.WriteLine($"Invalid Input! ({text} must be a non-negative decimal.)");

			Console.WriteLine("Would you like to try again? (Y/N)");
			string? tryAgain = Console.ReadLine();

			if (tryAgain != "Y") {
				Console.WriteLine($"Cancelled {operation}.");
				return false;
			}
	
			Console.Write($"{text}: ");
		}
		
		return true;
	}

	public static bool LoopGetString (out string? stringInput, string text, string operation) {
		Console.Write($"{text}: ");
		
		stringInput = (Console.ReadLine()??"").Trim();

		while (stringInput == null || stringInput.Where(x => (x >= 'A' && x <= 'Z') || (x >= 'a' && x <= 'z')).Count() == 0) {
			Console.WriteLine($"Invalid Input! ({text} cannot be a non-blank string.)");

			Console.WriteLine("Would you like to try again? (Y/N)");
			string? tryAgain = Console.ReadLine();

			if (tryAgain != "Y") {
				Console.WriteLine($"Cancelled {operation}.");
				return false;
			}
	
			Console.Write($"{text}: ");
		}
		
		return true;
	}

	public static void Main (string[] args) {
		Console.WriteLine("Welcome to Company's Employee Management System.");
		Console.WriteLine("Please type your order in format below.");
		
		for (int i=1; i<=50; i++) Console.Write('-');
		Console.WriteLine();

		Console.WriteLine("\"Create\" - Create & keying new Employee's information.");
		Console.WriteLine("\"Salary [Id]\" - Show Employee [id]'s salary.");
		Console.WriteLine("\"Work [Id]\" - Show Employee [id]'s work.");
		Console.WriteLine("\"Remove [Id]\" - Remove Employee [Id] from the system.");
		Console.WriteLine("\"Update [Id]\" - Update Employee [Id]'s Information.");
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

				int id;

				if (!LoopGetInt(out id, "ID", "Creating")) continue;
				
				if (employeeList.ContainsKey(id)) {
					Console.WriteLine("There's already an employee with this Id, please try again.");
					continue;
				}

				string? name;
				if (!LoopGetString(out name, "Name", "Creating")) continue;
				
				decimal salary;
				if (!LoopGetDecimal(out salary, "Salary", "Creating")) continue;
				
				string? department;
				if (!LoopGetString(out department, "Department", "Creating")) continue;

				//Developer employee
				if (choice == "1") {
					string? programmingLanguage;

					if (!LoopGetString(out programmingLanguage, "Programming Language", "Creating")) continue;
					
					int yearsOfExperience;
					
					if (!LoopGetInt(out yearsOfExperience, "Years of Experience", "Creating")) continue;

					Employee employee = new Developer (id, name!, salary, department!, programmingLanguage!, yearsOfExperience);				
					employeeList.Add(id,employee);
				}
				else {
					string? designTool;

					if (!LoopGetString(out designTool, "Design Tool", "Creating")) continue;

					string? specialization;

					if (!LoopGetString(out specialization, "Specialization", "Creating")) continue;

					Employee employee = new Designer (id, name!, salary, department!, designTool!, specialization!);			
					employeeList.Add(id,employee);
				}
			}
			
			else if (commandInput == "Print All") {
				int count = employeeList.Count-1;
		        
				if (employeeList.Count == 0) {
					Console.WriteLine("There is no employee yet, please try again later.");
					continue;
				}

				foreach (var employee in employeeList) {
					Employee currentEmployee = employee.Value;

					currentEmployee.DisplayInformation();
					if (count > 0) {	
						for (int i=0; i<30; i++) Console.Write('-');
						Console.WriteLine();
					}
					--count;
                }
				Console.WriteLine($"Number of Employees: {employeeList.Count}");
			}
			else {
                string[] formattedInput = (commandInput.Trim()).Split(' ');
                
                if (formattedInput.Length != 2 || formattedInput[0].Where(x => (x >= 'a' && x <= 'z') || (x >= 'A' && x <= 'Z')).Count() < 4) {
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
				else if (order == "Salary") {
					if (employeeList.ContainsKey(id)) employeeList[id].DisplaySalary();
                    else Console.WriteLine("There's no employee with this Id, please try again later.");
				}
				else if (order == "Update") {
					if (employeeList.ContainsKey(id)) {
						Console.WriteLine($"Updating {employeeList[id].Name}...");
						
						employeeList[id].DisplayUpdateChoice();
						
						int choice;
						if (!int.TryParse(Console.ReadLine() ?? " ", out choice) || choice < 1 || choice > employeeList[id].PropertyCount) {
							Console.Write("Invalid Input! Please try again.");
							Console.WriteLine($"(Update choice must be within range of 1 - {employeeList[id].PropertyCount} !)");
							continue;
						}

						Console.Write("Enter updated data: ");
					}
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