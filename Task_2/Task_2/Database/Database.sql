CREATE DATABASE Course_Management;

use Course_Management;

CREATE TABLE Student_Data (
	Student_Number VARCHAR(55) PRIMARY KEY NOT NULL,
	Student_FullName VARCHAR(55) NOT NULL,
	Student_Email VARCHAR(55) NOT NULL,
	Student_Password VARCHAR(55) NOT NULL
);

CREATE TABLE Module_Data(
	Student_Number VARCHAR(55) NOT NULL FOREIGN KEY REFERENCES Student_Data(Student_Number) ,
	Semester_Start_Date VARCHAR(5) NOT NULL,
	Semester_Week_Length int NOT NULL,
	ModuleCode VarChar(20) NOT NULL,
	ModuleName VARCHAR(255) NOT NULL,
	Module_Credit int NOT NULL,
	Module_Class_Hours int NOT NULL,
	selfStudyHours int NOT NULL,
)