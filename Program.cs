using System;
using System.Data.SqlClient;
using System.Data;

namespace SchoolDatabaseSQL;

class Program
{
    static void Main(string[] args)
    {
        string connectionString = "Server=localhost,1433;Database=NorthWind;User Id=sa;Password=12345OHdf%e;TrustServerCertificate=true";

        while (true)
        {
            Console.WriteLine("Select an option:");
            Console.WriteLine("1. Get all students");
            Console.WriteLine("2. Get all students in a certain class");
            Console.WriteLine("3. Add new staff");
            Console.WriteLine("4. Get staff");
            Console.WriteLine("5. Get all grades set in the last month");
            Console.WriteLine("6. Average grade per course");
            Console.WriteLine("7. Add new students");
            Console.WriteLine("8. Exit");

            int choice = Convert.ToInt32(Console.ReadLine());

            switch (choice)
            {
                case 1:
                    GetAllStudents(connectionString);
                    break;
                case 2:
                   // GetAllStudentsInClass(connectionString);
                    break;
                case 3:
                    //AddNewStaff(connectionString);
                    break;
                case 4:
                    //GetStaff(connectionString);
                    break;
                case 5:
                    //GetAllGradesLastMonth(connectionString);
                    break;
                case 6:
                   // AverageGradePerCourse(connectionString);
                    break;
                case 7:
                   // AddNewStudents(connectionString);
                    break;
                case 8:
                    Environment.Exit(0);
                    break;
                default:
                    Console.WriteLine("Invalid option. Please try again.");
                    break;
            }

            Console.WriteLine("Press Enter to return to the main menu.");
            Console.ReadLine(); // Wait for user to press Enter before clearing screen and displaying menu again
            Console.Clear(); // Clear console screen
        }
    }

    static void GetAllStudents(string connectionString)
    {
        Console.WriteLine("Sort by (1. First Name / 2. Last Name):");
        int sortBy = Convert.ToInt32(Console.ReadLine());
        Console.WriteLine("Sort order (1. Ascending / 2. Descending):");
        int sortOrder = Convert.ToInt32(Console.ReadLine());

        string orderBy = sortBy == 1 ? "FirstName" : "LastName";
        string order = sortOrder == 1 ? "ASC" : "DESC";

        string query = $"SELECT FirstName, LastName FROM Pupils ORDER BY {orderBy} {order}";

        ExecuteQuery(connectionString, query, reader =>
        {
            Console.WriteLine("All students:");
            while (reader.Read())
            {
                Console.WriteLine($"{reader["FirstName"]} {reader["LastName"]}");
            }
        });
    }

    
    // Function to execute a SQL query and process the results
    static void ExecuteQuery(string connectionString, string query, Action<SqlDataReader> action)
    {
        using (SqlConnection connection = new SqlConnection(connectionString))
        {
            SqlCommand command = new SqlCommand(query, connection);
            connection.Open();
            SqlDataReader reader = command.ExecuteReader();

            action(reader); // Process the results using the provided action
        }
    }
}

