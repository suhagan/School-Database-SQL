# School Database Management System

This School Database Management System is a C# console application designed to manage student/pupils, staff, class, grade, and course information in an SQL database. It utilizes console-based interaction and direct SQL database interaction via ADO.NET.

## Method Used

The application employs the following methods:

1. **Console Application**: User interaction occurs through a console-based menu system. Options are displayed on the console, and users input their choice via keyboard input.

2. **ADO.NET for Database Interaction**: To communicate with the SQL database, the application utilizes ADO.NET classes such as `SqlConnection`, `SqlCommand`, and `SqlDataReader` for establishing connections, executing queries, and processing results.

## Main Menu Functionality

Upon launching the application, users are presented with a main menu offering various options:

- **Get all students**: Retrieve a list of all students.
- **Get all students in a certain class**: Retrieve a list of students in a specific class.
- **Add new staff**: Add a new staff member to the database.
- **Get staff**: View staff information, either all staff or by category.
- **Get all grades set in the last month**: Retrieve grades set in the last month.
- **Average grade per course**: Calculate the average grade, highest grade, and lowest grade per course.
- **Add new students**: Add a new student to the database.
- **Exit**: Terminate the application.

## Learning Experience

Throughout the development of this project, several learning experiences were encountered:

- **Learning SQL Database Interactions**: Gained practical experience in integrating C# applications with SQL databases, including establishing connections, executing queries, and processing results using ADO.NET.

- **Understanding User Input Handling**: Implemented user input handling to ensure a smooth user experience, including validation mechanisms for handling invalid inputs and edge cases.

- **Designing Modular and Reusable Code**: Emphasized modularity and code reusability by encapsulating functionalities into separate methods and promoting parameterization for flexibility.

- **Error Handling and Exception Management**: Implemented basic error handling mechanisms, realizing the importance of robust error handling and exception management for enhancing application reliability and resilience.

## Why This Method?

The chosen method offers simplicity, portability, efficiency, and direct interaction with the SQL database. A console-based interface is straightforward to implement, suitable for this small-scale application, and offers broad compatibility. Direct SQL database interaction via ADO.NET provides flexibility and control over database operations.

## Alternatives

While the chosen method serves the project well, alternative approaches could be considered:

1. **Graphical User Interface (GUI)**
2. **Object-Relational Mapping (ORM)**
3. **Web Application**
