using System;
using System.Collections.Generic;
using System.IO;

namespace Expense_tracker
{
    internal class Program
    {
        static void Main(string[] args)
        {
          
            List<Expense> Expenses = new List<Expense>();

            while (true)
            {
               
                Console.WriteLine("Enter name: ");
                string Name = Console.ReadLine();

                decimal amount = 0;
                while (true)
                {
                    Console.Write("Enter Amount: ");
                    if (decimal.TryParse(Console.ReadLine(), out amount) && amount > 0)
                    {
                        break;
                    }
                    else
                    {
                        Console.WriteLine("Iltimos son kiriting");
                    }
                }

              
                DateTime expenseTime;
                while (true)
                {
                    Console.Write("Enter Time (HH:mm): ");
                    string userInput = Console.ReadLine();

                    if (DateTime.TryParseExact(userInput, "HH:mm", null, System.Globalization.DateTimeStyles.None, out expenseTime))
                    {
                        Console.WriteLine("Valid time entered: " + expenseTime.ToString("HH:mm"));
                        break;
                    }
                    else
                    {
                        Console.WriteLine("Invalid time format. Please use 'HH:mm'.");
                    }
                }

               
                AddExpense(Expenses, Name, amount, expenseTime);

               
                Console.WriteLine("\nExpenses List:");
                string pattern = "| {0,-10} | {1,-10} | {2,-10} |";
                Console.WriteLine(string.Format(pattern, "Expense", "Amount", "Date"));
                Console.WriteLine("|------------|------------|------------|");
                foreach (var expense in Expenses)
                {
                    Console.WriteLine(string.Format(pattern, expense.Name, expense.Amount, expense.Time.ToString("HH:mm")));
                }

                Console.WriteLine("\nHit Enter to add another expense, or type 'stop' to save and exit.");
                var userInputForExit = Console.ReadLine();
                if (userInputForExit.ToLower() == "stop")
                {
                    
                    SaveExpensesToFile(Expenses);
                    break;
                }
            }
        }

      
        static void AddExpense(List<Expense> expenses, string name, decimal amount, DateTime time)
        {
            expenses.Add(new Expense { Name = name, Amount = amount, Time = time });
        }

        static void SaveExpensesToFile(List<Expense> expenses)
        {
            string filePath = "expenses.txt";  

            using (StreamWriter writer = new StreamWriter(filePath))
            {
                string pattern = "| {0,-10} | {1,-10} | {2,-10} |";
                writer.WriteLine(string.Format(pattern, "Expense", "Amount", "Date"));
                writer.WriteLine("|------------|------------|------------|");

                foreach (var expense in expenses)
                {
                    writer.WriteLine(string.Format(pattern, expense.Name, expense.Amount, expense.Time.ToString("HH:mm")));
                }
            }

            Console.WriteLine("All expenses have been saved to expenses.txt");
        }
    }

    public class Expense
    {
        public string Name { get; set; }
        public decimal Amount { get; set; }
        public DateTime Time { get; set; }
    }
}

