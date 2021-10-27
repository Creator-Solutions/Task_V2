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
	ModuleName VARCHAR(20) NOT NULL,
	Module_Credit int NOT NULL,
	Module_Class_Hours int NOT NULL,
	selfStudyHours int NOT NULL,
)

ALTER TABLE Module_data
ALTER column Semester_Start_Date VARCHAR(20) NOT NULL



SELECT * FROM Student_Data;

DELETE FROM Module_Data 

UPDATE Module_Data SET selfStudyHours = 3 WHERE ModuleCode = 'PROG6212'

SELECT selfStudyHours, Required_Hours, ModuleCode FROM Module_Data WHERE Required_Hours > selfStudyHours

SELECT Top(1) selfStudyHours, Required_Hours, ModuleCode FROM Module_Data WHERE selfStudyHours > Required_Hours 


SELECT COUNT(*) AS Total_Modules FROM Module_Data WHERE Student_Number='20117648'

SELECT SUM(Module_Credit) AS TOTAL_CREDITS FROM Module_Data WHERE Student_Number='20117648'

SELECT * FROM Module_Data

SELECT AVG(selfStudyHours) AS TOTAL_CREDITS FROM Module_Data WHERE Student_Number='20117648'

	
