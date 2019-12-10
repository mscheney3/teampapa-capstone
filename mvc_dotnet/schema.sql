
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
frames int not null,
description varchar(200) not null,
scenario_image varchar(50)

constraint pk_scenarios primary key (scenario_id)
);

CREATE TABLE questions
(
question_id int not null identity (1,1),
frame_id int not null,
question_text varchar(200) not null

constraint pk_questions primary key (question_id)
)

CREATE TABLE frames
(
frame_id int not null identity (1,1),
scenario_id int not null,
situation_text varchar(200) not null,
situation_image varchar(50) not null

constraint pk_frames primary key (frame_id)
)

CREATE TABLE answers
(
answer_id int not null identity (1,1),
question_id int not null,
answer_text varchar(50) not null

constraint pk_answers primary key (answer_id)
)

CREATE TABLE responses
(
response_id int not null identity (1,1),
answer_id int not null,
response_text varchar(100) not null,
response_image varchar(50) not null

constraint pk_responses primary key (response_id)
);

ALTER TABLE teachers
ADD FOREIGN KEY (teacher_id) REFERENCES users(id);

ALTER TABLE teachers
ADD FOREIGN KEY (student_id) REFERENCES users(id);

ALTER TABLE students
ADD FOREIGN KEY (student_id) REFERENCES users(id);

ALTER TABLE students
ADD FOREIGN KEY (scenario_id) REFERENCES scenarios(scenario_id);

ALTER TABLE frames
 ADD FOREIGN KEY (scenario_id) REFERENCES scenarios(scenario_id);

 ALTER TABLE questions
 ADD FOREIGN KEY (frame_id) REFERENCES frames(frame_id);

 ALTER TABLE answers
 ADD FOREIGN KEY (question_id) REFERENCES questions(question_id);

 ALTER TABLE responses
 ADD FOREIGN KEY (answer_id) REFERENCES answers(answer_id);

COMMIT TRANSACTION;