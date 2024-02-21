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
                   GetAllStudentsInClass(connectionString);
                    break;
                case 3:
                    AddNewStaff(connectionString);
                    break;
                case 4:
                    GetStaff(connectionString);
                    break;
                case 5:
                    GetAllGradesLastMonth(connectionString);
                    break;
                case 6:
                   AverageGradePerCourse(connectionString);
                    break;
                case 7:
                   AddNewStudents(connectionString);
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

    static void GetAllStudentsInClass(string connectionString)
    {
        
        // Display a message to prompt the user to select a class
        Console.WriteLine("Select a class:");

        // Fetch the list of available classes from the database
        string query = "SELECT ClassID, ClassName FROM Classes";
        Console.WriteLine("Available Classes:");
        using (SqlConnection connection = new SqlConnection(connectionString))
        {
            SqlCommand command = new SqlCommand(query, connection);
            connection.Open();
            SqlDataReader reader = command.ExecuteReader();

            // Display the available classes for user selection
            while (reader.Read())
            {
                Console.WriteLine($"{reader["ClassID"]}. {reader["ClassName"]}");
            }
        }

        // Prompt the user to enter the class ID
        Console.WriteLine("Enter the Class ID to view students:");
        int classID = Convert.ToInt32(Console.ReadLine());

        // Query to retrieve students in the selected class
        string studentQuery = "SELECT FirstName, LastName FROM Pupils WHERE ClassID = @ClassID";
        using (SqlConnection connection = new SqlConnection(connectionString))
        {
            SqlCommand command = new SqlCommand(studentQuery, connection);
            command.Parameters.AddWithValue("@ClassID", classID);
            connection.Open();
            SqlDataReader reader = command.ExecuteReader();

            // Display students in the selected class
            Console.WriteLine($"Students in Class {classID}:");
            while (reader.Read())
            {
                Console.WriteLine($"{reader["FirstName"]} {reader["LastName"]}");
            }
        }
    }

    static void AddNewStaff(string connectionString)
    {
        

        Console.WriteLine("Enter the details of the new staff member:");

        // Prompt the user to enter information about the new staff
        Console.Write("First Name: ");
        string firstName = Console.ReadLine();

        Console.Write("Last Name: ");
        string lastName = Console.ReadLine();

        Console.Write("Category (e.g., teacher, administrator, principal): ");
        string category = Console.ReadLine();

        // Query to insert the new staff member into the Personnel table
        string insertQuery = "INSERT INTO Personnel (FirstName, LastName, Category) VALUES (@FirstName, @LastName, @Category)";

        // Execute the insert query to add the new staff member to the database
        using (SqlConnection connection = new SqlConnection(connectionString))
        {
            SqlCommand command = new SqlCommand(insertQuery, connection);
            command.Parameters.AddWithValue("@FirstName", firstName);
            command.Parameters.AddWithValue("@LastName", lastName);
            command.Parameters.AddWithValue("@Category", category);

            connection.Open();
            int rowsAffected = command.ExecuteNonQuery();

            if (rowsAffected > 0)
            {
                Console.WriteLine("New staff member added successfully!");
            }
            else
            {
                Console.WriteLine("Failed to add new staff member.");
            }
        }
    }

    static void GetStaff(string connectionString)
    {
        Console.WriteLine("Select an option:");
        Console.WriteLine("1. View all staff");
        Console.WriteLine("2. View staff by category");
        int option = Convert.ToInt32(Console.ReadLine());

        // Query to retrieve all staff members
        string query = "SELECT FirstName, LastName, Category FROM Personnel";

        // If the user chooses to view staff by category, prompt for the category
        if (option == 2)
        {
            Console.WriteLine("Enter the category to view staff:");
            string category = Console.ReadLine();
            query = $"SELECT FirstName, LastName, Category FROM Personnel WHERE Category = '{category}'";
        }

        // Execute the query to retrieve staff information
        using (SqlConnection connection = new SqlConnection(connectionString))
        {
            SqlCommand command = new SqlCommand(query, connection);
            connection.Open();
            SqlDataReader reader = command.ExecuteReader();

            // Display the staff information
            Console.WriteLine("Staff Information:");
            while (reader.Read())
            {
                Console.WriteLine($"{reader["FirstName"]} {reader["LastName"]} - {reader["Category"]}");
            }
        }
    }

    static void GetAllGradesLastMonth(string connectionString)
    {
        // Get the start date of the last month
        DateTime lastMonthStart = DateTime.Now.AddMonths(-1);
        lastMonthStart = new DateTime(lastMonthStart.Year, lastMonthStart.Month, 1); // Set the day to the first day of the month

        // Get the end date of the last month
        DateTime lastMonthEnd = new DateTime(DateTime.Now.Year, DateTime.Now.Month, 1).AddDays(-1); // Set the day to the last day of the previous month

        // Query to retrieve grades set in the last month
        string query = "SELECT P.FirstName AS 'StudentFirstName', P.LastName AS 'StudentLastName', " +
                       "C.CourseName, G.Grade " +
                       "FROM Grades G " +
                       "INNER JOIN Pupils P ON G.PupilID = P.PupilID " +
                       "INNER JOIN Courses C ON G.CourseID = C.CourseID " +
                       "WHERE CONVERT(DATE, GradeDate) BETWEEN @StartDate AND @EndDate";

        // Execute the query to retrieve grades set in the last month
        using (SqlConnection connection = new SqlConnection(connectionString))
        {
            SqlCommand command = new SqlCommand(query, connection);
            command.Parameters.AddWithValue("@StartDate", lastMonthStart);
            command.Parameters.AddWithValue("@EndDate", lastMonthEnd);

            connection.Open();
            SqlDataReader reader = command.ExecuteReader();

            // Display the grades set in the last month
            Console.WriteLine("Grades set in the last month:");
            while (reader.Read())
            {
                Console.WriteLine($"{reader["StudentFirstName"]} {reader["StudentLastName"]} - " +
                                  $"{reader["CourseName"]}: {reader["Grade"]}");
            }
        }
    }

    static void AverageGradePerCourse(string connectionString)
    {
        // Query to calculate average, highest, and lowest grades per course
        string query = "SELECT C.CourseName, " +
                       "AVG(G.Grade) AS 'AverageGrade', " +
                       "MAX(G.Grade) AS 'HighestGrade', " +
                       "MIN(G.Grade) AS 'LowestGrade' " +
                       "FROM Grades G " +
                       "INNER JOIN Courses C ON G.CourseID = C.CourseID " +
                       "GROUP BY C.CourseName";

        // Execute the query to calculate average, highest, and lowest grades per course
        using (SqlConnection connection = new SqlConnection(connectionString))
        {
            SqlCommand command = new SqlCommand(query, connection);
            connection.Open();
            SqlDataReader reader = command.ExecuteReader();

            // Display the average, highest, and lowest grades per course
            Console.WriteLine("Average grade per course:");
            Console.WriteLine("Course Name\tAverage Grade\tHighest Grade\tLowest Grade");
            while (reader.Read())
            {
                Console.WriteLine($"{reader["CourseName"]}\t" +
                                  $"{reader["AverageGrade"]}\t\t" +
                                  $"{reader["HighestGrade"]}\t\t" +
                                  $"{reader["LowestGrade"]}");
            }
        }
    }

    static void AddNewStudents(string connectionString)
    {
        Console.WriteLine("Enter the details of the new student:");

        // Prompt the user to enter information about the new student
        Console.Write("First Name: ");
        string firstName = Console.ReadLine();

        Console.Write("Last Name: ");
        string lastName = Console.ReadLine();

        Console.Write("Class ID: ");
        int classID = Convert.ToInt32(Console.ReadLine());

        // Query to insert the new student into the Pupils table
        string insertQuery = "INSERT INTO Pupils (FirstName, LastName, ClassID) VALUES (@FirstName, @LastName, @ClassID)";

        // Execute the insert query to add the new student to the database
        using (SqlConnection connection = new SqlConnection(connectionString))
        {
            SqlCommand command = new SqlCommand(insertQuery, connection);
            command.Parameters.AddWithValue("@FirstName", firstName);
            command.Parameters.AddWithValue("@LastName", lastName);
            command.Parameters.AddWithValue("@ClassID", classID);

            connection.Open();
            int rowsAffected = command.ExecuteNonQuery();

            if (rowsAffected > 0)
            {
                Console.WriteLine("New student added successfully!");
            }
            else
            {
                Console.WriteLine("Failed to add new student.");
            }
        }
    }


}

