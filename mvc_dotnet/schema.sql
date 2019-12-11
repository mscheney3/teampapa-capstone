
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
description varchar(200) not null,
scenario_image varchar(50),
question varchar(200) not null

constraint pk_scenarios primary key (scenario_id)
);


CREATE TABLE answers
(
answer_id int not null identity (1,1),
scenario_id int not null,
answer_text varchar(50) not null,
response_text varchar(200) not null,
response_image varchar(50) not null

constraint pk_answers primary key (answer_id)
)


ALTER TABLE teachers
ADD FOREIGN KEY (teacher_id) REFERENCES users(id);

ALTER TABLE teachers
ADD FOREIGN KEY (student_id) REFERENCES users(id);

ALTER TABLE students
ADD FOREIGN KEY (student_id) REFERENCES users(id);

ALTER TABLE students
ADD FOREIGN KEY (scenario_id) REFERENCES scenarios(scenario_id);

 ALTER TABLE answers
 ADD FOREIGN KEY (scenario_id) REFERENCES scenarios(scenario_id);


INSERT INTO scenarios (scenario_name, description, scenario_image, question) VALUES ('Classroom', 'Your teacher asks you to read aloud', 'classroomscenario.png', '______ would you like to read?');
INSERT INTO scenarios (scenario_name, description, scenario_image, question) VALUES ('Hallway', 'You want to place your books in your locker. However, your neighbor is blocking your locker.', 'hallwayscenario.png', 'Do you:');
INSERT INTO scenarios (scenario_name, description, scenario_image, question) VALUES ('Soccer', 'You need to choose your next move.', 'outdoorscenario.png', 'Do you:');

INSERT INTO answers (scenario_id, answer_text, response_text, response_image) VALUES (1, 'A) Sure', 'Good job!', 'classroomsure.png');
INSERT INTO answers (scenario_id, answer_text, response_text, response_image) VALUES (1, 'B) No', 'People will support you if you try.', 'classroomno.png');
INSERT INTO answers (scenario_id, answer_text, response_text, response_image) VALUES (1, 'C) Start reading', 'Your teacher expects you to answer before reading', 'classroomstartreading.png');
INSERT INTO answers (scenario_id, answer_text, response_text, response_image) VALUES (2, 'A) Walk away', 'People will help if you ask', 'hallwaywalkaway.png');
INSERT INTO answers (scenario_id, answer_text, response_text, response_image) VALUES (2, 'B) Squeeze in front of classmate', 'Next time ask me to move over and I will', 'hallwaysqueeze.png');
INSERT INTO answers (scenario_id, answer_text, response_text, response_image) VALUES (2, 'C) Say "excuse me"', 'Classmates will always listen if you are nice to them', 'hallwaysayexcuseme.png');
INSERT INTO answers (scenario_id, answer_text, response_text, response_image) VALUES (3, 'A) Steal ball', 'You should ask for the pass. If you hurt others feeling it is good to apologize', 'soccerstealball.png');
INSERT INTO answers (scenario_id, answer_text, response_text, response_image) VALUES (3, 'B) Do/Say nothing', 'The other team scored and everyone is happy for them', 'soccernothing.png');
INSERT INTO answers (scenario_id, answer_text, response_text, response_image) VALUES (3, 'C) Call for ball', 'Nice shot!', 'soccercallforball.png');
 


COMMIT TRANSACTION;