
-- Switch to the system (aka master) database
USE master;
GO

-- Delete the DemoDB Database (IF EXISTS)
IF EXISTS(select * from sys.databases where name='GamePrototype')
DROP DATABASE GamePrototype;
GO

-- Create a new DemoDB Database
CREATE DATABASE GamePrototype;
GO

-- Switch to the DemoDB Database
USE GamePrototype
GO

BEGIN TRANSACTION;

CREATE TABLE users
(
	id			int			identity(1,1),
	username	varchar(50)	not null,
	password	varchar(50)	not null,
	salt		varchar(50)	not null,
	role		varchar(50)	default('student'),

	constraint pk_users primary key (id)
);

CREATE TABLE teachers
(
 teacher_id	int	not null,
 student_id int 

 constraint pk_teachers primary key (teacher_id, student_id)
);

CREATE TABLE students
(
student_id int not null,
scenario_id int

constraint pk_students primary key (student_id, scenario_id)
);

CREATE TABLE scenarios
(
scenario_id int not null identity (1,1),
scenario_name varchar(50) not null,
frames int not null

constraint pk_scenarios primary key (scenario_id)
);

CREATE TABLE questions
(
question_id int not null identity (1,1),
scenario_id int not null,
question_text varchar(200) not null

constraint pk_questions primary key (question_id)
)

COMMIT TRANSACTION;