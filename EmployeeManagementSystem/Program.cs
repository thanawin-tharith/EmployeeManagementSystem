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

	public void UpdateName (string name) {
		Name = name;
	}
	public void UpdateSalary (decimal salary) {
		Salary = salary;
	}
	public void UpdateDepartment (string department) {
		Department = department;
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

	public void UpdateProgrammingLanguage (string programmingLanguage) {
		ProgrammingLanguage = programmingLanguage;
	}

	public void UpdateYearsOfExperience (int yearsOfExperience) {
		YearsOfExperience = yearsOfExperience;
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

	public void UpdateDesignTool (string designTool) {
		DesignTool = designTool;
	}

	public void UpdateSpecialization (string specialization) {
		Specialization = specialization;
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

		while (string.IsNullOrWhiteSpace(stringInput) || stringInput.Length == 0) {
			Console.WriteLine($"Invalid Input!");
			if (stringInput.Length > 50) Console.WriteLine($"({text} length cannot exceed 50.)");
			else Console.WriteLine($"({text} cannot be a blank string.)");

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

	private static readonly Dictionary<string, string> Commands = new()
{
    { "\"Create\"", "Create & keying new Employee's information." },
    { "\"Salary [Id]\"", "Show Employee [id]'s salary." },
    { "\"Work [Id]\"", "Show Employee [id]'s work." },
    { "\"Remove [Id]\"", "Remove Employee [Id] from the system." },
    { "\"Update [Id]\"", "Update Employee [Id]'s Information." },
    { "\"Print [Id]\"", "Output Employee [Id]'s Information." },
    { "\"Print All\"", "Output all Employee's Information." },
    { "\"Help\"", "Display all commands." },
    { "\"Exit\"", "Exit the program." }
};
	public static void DisplayAllCommand () {
		Console.Write(' ');

		for (int i=1; i<80; i++) Console.Write('-');

		Console.WriteLine();		
		foreach (var command in Commands) {
			Console.Write('|');
			for (int j=1; j<=13; j++) Console.Write(' ');
			
			Console.Write(command.Key);

			for (int j = 0; j < 15 - (command.Key).Length; j++) Console.Write(' ');
			
			Console.Write("-   " + command.Value);

			int len = 14 + 19 + (command.Value).Length;
			for (int j = len; j<80; j++) Console.Write(' ');
			
			Console.WriteLine('|');
		}

		Console.Write(' ');
		for (int i=1; i<80; i++) Console.Write('-');
		Console.WriteLine();
	}

	public static void Main (string[] args) {
		Console.WriteLine("Welcome to Company's Employee Management System.");
		Console.WriteLine("Please type your order in format below.");


		DisplayAllCommand();

		string? commandInput = "";

		// Well, input validations may not be that detailed since it's merely a barebone to test my OOP concept program.

		SortedDictionary<int,Employee>employeeList = new SortedDictionary<int,Employee>();

		bool printedEnd = true;

		while (true) {
			if (printedEnd == false) {
				for (int i=0; i<50; i++) Console.Write('-');
				Console.WriteLine();
			}
			else printedEnd = false;

			Console.Write("Enter your command: ");

			commandInput = (Console.ReadLine()??"").Trim();
			
			if (commandInput != "Help") {
				for (int i=0; i<50; i++) Console.Write('-');
				Console.WriteLine();
			}

			if (string.IsNullOrWhiteSpace(commandInput) || commandInput.Length == 0) {
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
				
				if (string.IsNullOrWhiteSpace(choice) || choice.Length == 0 || (choice != "1" && choice != "2")) {
					Console.Write("Invalid Input! Please try again.");
					
					if (string.IsNullOrWhiteSpace(choice) || choice.Length == 0) Console.WriteLine(" (Role option cannot be blank!)");
					else Console.WriteLine(" (Role option must be either 1 or 2!)");

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
			else if (commandInput == "Help") {
				DisplayAllCommand();
				printedEnd = true;
				continue;
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

						if (choice == 1) {
							string? name;

							if (!LoopGetString(out name, "Updated Name", "Updating")) continue;					
							
							employeeList[id].UpdateName(name!);
						}
						else if (choice == 2) {
							decimal salary;
							if (!LoopGetDecimal(out salary, "Updated Salary", "Updating")) continue;					
							
							employeeList[id].UpdateSalary(salary);
						}
						else if (choice == 3) {
							string? department;
							if (!LoopGetString(out department, "Updated Department", "Updating")) continue;					
							employeeList[id].UpdateDepartment(department!);
						}
						else if (choice == 4) {
							if (employeeList[id] is Developer) {
								string? programmingLanguage;
								if (!LoopGetString(out programmingLanguage, "Updated Programming Language", "Updating")) continue;					
								((Developer)employeeList[id]).UpdateProgrammingLanguage(programmingLanguage!);
							}
							else {
								string? designTool;
								if (!LoopGetString(out designTool, "Updated Design Tool", "Updating")) continue;
								((Designer)employeeList[id]).UpdateDesignTool(designTool!);
							}
						}
						else {
							if (employeeList[id] is Developer) {
								int yearsOfExperience;
								if (!LoopGetInt(out yearsOfExperience, "Updated Years of Experience", "Updating")) continue;					
								((Developer)employeeList[id]).UpdateYearsOfExperience(yearsOfExperience);
							}
							else {
								string? specialization;
								if (!LoopGetString(out specialization, "Updated Specialization", "Updating")) continue;
								((Designer)employeeList[id]).UpdateSpecialization(specialization!);
							}
						}
					}

                    else Console.WriteLine("There's no employee with this Id, please try again later.");
				
				}
                else {
					Console.WriteLine("Not a valid command, please try again.");
					continue;
                }
			}
			
			printedEnd = true;
			for (int i=0; i<50; i++) Console.Write('-');
			Console.WriteLine();
		}

		Console.WriteLine("System exited.");
	}
}