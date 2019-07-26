--USE master

--IF DB_ID('SchoolManagementSystem') IS NOT NULL
--DROP DATABASE SchoolManagementSystem;

--CREATE DATABASE SchoolManagementSystem;
--GO

----USE SchoolManagementSystem;
--GO

--CREATE TABLE Users
--(
--	UserID			INT				IDENTITY,
--	Name			NVARCHAR(50)		NOT NULL,
--	Email			NVARCHAR(50)		NOT NULL,
--	Password		NVARCHAR(50)		NOT NULL,
--	Phone			NVARCHAR(20)		NOT NULL,
--	Address			NVARCHAR(50)		NOT NULL,
--	IsActive 		BIT,

--	PRIMARY KEY (UserID)	
--);
--GO

CREATE TABLE [dbo].[Teachers] (
    [TeacherID]   INT            IDENTITY (1, 1) NOT NULL,
    [TeacherName] NVARCHAR (50)  NOT NULL,
    [Designation] NVARCHAR (50)  NOT NULL,
    [BirthDay]    NVARCHAR (15)  NOT NULL,
    [Gender]      NVARCHAR (20)  NOT NULL,
    [Address]     NVARCHAR (150) NOT NULL,
    [Image]       NVARCHAR (MAX) NULL,
    [UserID]      NVARCHAR (128) NOT NULL,
    [IsActive]    BIT            NULL,
    PRIMARY KEY CLUSTERED ([TeacherID] ASC),
    FOREIGN KEY ([UserID]) REFERENCES [dbo].[AspNetUsers] ([Id])
);
GO

--select * from dbo.AspNetUsers

CREATE TABLE Accountants
(
	AccountantID	INT				IDENTITY,
	UserID			nvarchar(128)				NOT NULL,
	IsActive 		BIT				NOT NULL,

	PRIMARY KEY (AccountantID),
	FOREIGN KEY (UserID)			REFERENCES dbo.AspNetUsers(Id)
);
GO

CREATE TABLE [dbo].[Parents] (
    [ParentID]   INT            IDENTITY (1, 1) NOT NULL,
    [Name]       NVARCHAR (50)  NOT NULL,
    [Profession] NVARCHAR (50)  NOT NULL,
    [NID]        NVARCHAR (50)  NOT NULL,
    [UserID]     NVARCHAR (128) NOT NULL,
    [IsActive]   BIT            DEFAULT ((1)) NULL,
    PRIMARY KEY CLUSTERED ([ParentID] ASC),
    FOREIGN KEY ([UserID]) REFERENCES [dbo].[AspNetUsers] ([Id])
);


GO


CREATE TABLE Classes
(
	ClassID				INT				IDENTITY,
	ClassName			NVARCHAR(50)	NOT NULL,
	ClassName_Numeric	NVARCHAR(20)	NOT NULL,
	IsActive			BIT,

	PRIMARY KEY (ClassID),
);
GO

CREATE TABLE Sections
(
	SectionID		INT				IDENTITY,
	SectionName		NVARCHAR(20)	NOT NULL,
	NickName		NVARCHAR(20)	NOT NULL,
	IsActive		BIT,

	PRIMARY KEY (SectionID),
);
GO



CREATE TABLE Broker_ClassSection
(
	ID					INT				IDENTITY,
	ClassID				INT				NOT NULL,
	SectionID			INT				NOT NULL,

	PRIMARY KEY (ID),
	FOREIGN KEY (ClassID)		REFERENCES Classes(ClassID),
	FOREIGN KEY (SectionID)		REFERENCES Sections(SectionID)
);
GO


CREATE TABLE [dbo].[Students] (
    [StudentID]    INT            IDENTITY (1, 1) NOT NULL,
    [StudentRegID] NVARCHAR (50)  NOT NULL,
    [StudentName]  NVARCHAR (50)  NOT NULL,
    [ParentID]     INT            NOT NULL,
    [BirthDate]    NVARCHAR (30)  NOT NULL,
    [Gender]       NVARCHAR (30)  NOT NULL,
    [Image]        NVARCHAR (MAX) NULL,
    [UserID]       NVARCHAR (128) NOT NULL,
    [IsActive]     BIT            NOT NULL,
    PRIMARY KEY CLUSTERED ([StudentID] ASC),
    FOREIGN KEY ([ParentID]) REFERENCES [dbo].[Parents] ([ParentID]),
    FOREIGN KEY ([UserID]) REFERENCES [dbo].[AspNetUsers] ([Id])
);


Go


CREATE TABLE [dbo].[AssignStudentdToClass] (
    [AssignID]       INT           IDENTITY (1, 1) NOT NULL,
    [RollNo]         INT           NOT NULL,
    [SessionYear]    NVARCHAR (30) NOT NULL,
    [StudentID]      INT           NOT NULL,
    [ClassSectionID] INT           NOT NULL,
    [PresentStatus]  NVARCHAR (30) NULL,
    PRIMARY KEY CLUSTERED ([AssignID] ASC),
    FOREIGN KEY ([StudentID]) REFERENCES [dbo].[Students] ([StudentID]),
    FOREIGN KEY ([ClassSectionID]) REFERENCES [dbo].[Broker_ClassSection] ([ID])
);


GO

CREATE TABLE ArchivedStudents --PreviousName AuditAssignStudentToClass
(
	ArchivedStudentID 	INT				IDENTITY,
	RollNo				INT				NOT NULL,
	SessionYear			NVARCHAR(15)	NOT NULL,
	StudentRegID		NVARCHAR(30)	NOT NULL,
	ClassName			NVARCHAR(30),
	SectionName			NVARCHAR(30),

	PRIMARY KEY (ArchivedStudentID),
);
GO


CREATE TABLE Broker_TeacherSection
(
	ID					INT				IDENTITY,
	ClassSectionID		INT				NOT NULL,
	TeacherID			INT				NOT NULL,
	IsClassTeacher 		BIT				NOT NULL,

	PRIMARY KEY (ID),
	FOREIGN KEY (ClassSectionID)	REFERENCES Broker_ClassSection(ID),
	FOREIGN KEY (TeacherID)			REFERENCES Teachers(TeacherID),
);
GO


CREATE TABLE Broker_SubjectTeacher
(
	ID				INT				IDENTITY,
	SubjectName 	NVARCHAR(30)		NOT NULL,
	ClassID			INT				NOT NULL,
	TeacherID		INT				NOT NULL,

	PRIMARY KEY (ID),
	FOREIGN KEY (ClassID)			REFERENCES Classes(ClassID),
	FOREIGN KEY (TeacherID)			REFERENCES Teachers(TeacherID),
);
GO

CREATE TABLE Grade 
(
	GradeID			INT				IDENTITY,
	GradeName		NVARCHAR(20)		NOT NULL,
	GradePoint		DECIMAL(3,2)	NOT NULL,
	GradeFrom		INT				NOT NULL,
	GradeUpto		INT				NOT NULL,
	Comment			NVARCHAR(50),

	PRIMARY KEY (GradeID),
);
GO

CREATE TABLE Exams
(
	ExamID			INT				IDENTITY,
	ExamName		NVARCHAR(20)		NOT NULL,
	ExamDate		NVARCHAR(50)		NOT NULL,
	Comment			NVARCHAR(50)		NULL,

	PRIMARY KEY (ExamID),
);
GO

CREATE TABLE Marks 
(
	MarksID			INT				IDENTITY,
	StudentRegID	NVARCHAR(50)		NOT NULL,
	ExamID			INT				NOT NULL,
	ClassName		NVARCHAR(50)		NOT NULL,
	SectionName		NVARCHAR(50)		NOT NULL,
	SubjectName		NVARCHAR(50)		NOT NULL,
	SubjectMark		DECIMAL(4,2)	NOT NULL,
	EntryBy			NVARCHAR(50)	 	NOT NULL,
	EntryDate		NVARCHAR(50)		NOT NULL,
	ModifiedBy		NVARCHAR(50)		NULL,
	ModifiedDate	NVARCHAR(50)		NULL,
	Comment			NVARCHAR(50)		NULL,

	PRIMARY KEY (MarksID),
	FOREIGN KEY (ExamID)			REFERENCES Exams(ExamID),	
);
GO

CREATE TABLE Routine
(
	RoutineID			INT				IDENTITY,
	ClassSectionID		INT				NOT NULL,
	SubjectTeacherID	INT				NOT NULL,
	Day					NVARCHAR(20)		NOT NULL,
	StartTime			NVARCHAR(30)		NOT NULL,
	EndTime				NVARCHAR(30)		NOT NULL,
	CreatedBy			NVARCHAR(30)		NOT NULL,
	IsActive			BIT				NOT NULL,

	PRIMARY KEY (RoutineID),
	FOREIGN KEY (ClassSectionID)	REFERENCES Broker_ClassSection(ID),
	FOREIGN KEY (SubjectTeacherID)	REFERENCES Broker_SubjectTeacher(ID),

);
GO

CREATE TABLE Attendance
(
	AttendanceID	INT				IDENTITY,
	ClassSectionID	INT				NOT NULL,
	StudentID		INT				NOT NULL,
	Date			NVARCHAR(20)		NOT NULL,
	Status			NVARCHAR(30)		NOT NULL,
	UserID			nvarchar(128)				NOT NULL,
	Comment			NVARCHAR(50)		NULL,


	PRIMARY KEY (AttendanceID),
	FOREIGN KEY (ClassSectionID)	REFERENCES Broker_ClassSection(ID),
	FOREIGN KEY (StudentID)			REFERENCES Students(StudentID),
	FOREIGN KEY (UserID)			REFERENCES dbo.AspNetUsers(Id)

);
GO

CREATE TABLE Notice
(
	NoticeID		INT				IDENTITY,
	Title			NVARCHAR(30)		NOT NULL,
	Date			NVARCHAR(20)		NOT NULL,
	ImagePath		NVARCHAR(30)		NULL,
	FileLink		NVARCHAR(30)		NULL,
	IsDisplay		BIT				NOT NULL,
	CreatedBy		NVARCHAR(30)		NOT NULL,

	PRIMARY KEY (NoticeID),

);
GO

CREATE TABLE ExpenseCategory
(
	CategoryID		INT				IDENTITY,
	CategoryName	NVARCHAR(30)		NOT NULL,
	IsActice		BIT				NOT NULL,
	CreatedBy		NVARCHAR(30)		NOT NULL,

	PRIMARY KEY (CategoryID),

);
GO

CREATE TABLE StudentPayment
(
	PaymentID			INT					IDENTITY,
	StudentRegID		NVARCHAR(50)			NOT NULL,
	ClassSectionID		INT					NOT NULL,
	Title				NVARCHAR(30)			NOT NULL,
	Description			NVARCHAR(30)			NOT NULL,
	Date				NVARCHAR(20)			NOT NULL,
	TotalAmmount		DECIMAL(8,2)		NOT NULL,
	PayAmmount			DECIMAL(8,2)		NOT NULL,
	Status				NVARCHAR(20)			NOT NULL,
	PayMethod			NVARCHAR(20)			NOT NULL,
	EntryBy				NVARCHAR(30)			NOT NULL,

	PRIMARY KEY (PaymentID),
	FOREIGN KEY (ClassSectionID)	REFERENCES Broker_ClassSection(ID),

);
GO




