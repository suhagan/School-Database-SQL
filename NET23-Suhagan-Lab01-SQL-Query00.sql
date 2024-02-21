-- Create Pupils table
CREATE TABLE Pupils (
    PupilID INT PRIMARY KEY,
    FirstName VARCHAR(50),
    LastName VARCHAR(50),
    ClassID INT,  -- Foreign key referencing Classes table
    CONSTRAINT FK_Pupil_Class FOREIGN KEY (ClassID) REFERENCES Classes(ClassID)
);

-- Create Classes table
CREATE TABLE Classes (
    ClassID INT PRIMARY KEY,
    ClassName VARCHAR(50)
);

-- Create Courses table
CREATE TABLE Courses (
    CourseID INT PRIMARY KEY,
    CourseName VARCHAR(100)
);

-- Create Grades table
CREATE TABLE Grades (
    GradeID INT PRIMARY KEY,
    PupilID INT,  -- Foreign key referencing Pupils table
    CourseID INT,  -- Foreign key referencing Courses table
    Grade DECIMAL(5, 2),  -- Assuming grades are stored as decimal numbers
    CONSTRAINT FK_Grade_Pupil FOREIGN KEY (PupilID) REFERENCES Pupils(PupilID),
    CONSTRAINT FK_Grade_Course FOREIGN KEY (CourseID) REFERENCES Courses(CourseID)
);

-- Create Personnel table
CREATE TABLE Personnel (
    PersonnelID INT PRIMARY KEY,
    FirstName VARCHAR(50),
    LastName VARCHAR(50),
    Category VARCHAR(50)  -- Category of personnel (e.g., teacher, administrator, principal)
);
