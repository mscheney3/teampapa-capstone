
BEGIN TRANSACTION;

CREATE TABLE users
(
	id			int			identity(1,1),
	username	varchar(50)	not null,
	password	varchar(50)	not null,
	salt		varchar(50)	not null,
	role		varchar(50)	default('Student'),

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
question varchar(200) not null,
isActive bit

constraint pk_scenarios primary key (scenario_id)
);


CREATE TABLE answers
(
answer_id int not null identity (1,1),
scenario_id int not null,
answer_text varchar(50) not null,
response_text varchar(200) not null,
response_image varchar(50) not null,
response_color varchar(50) not null,
emoji varchar(50) not null

constraint pk_answers primary key (answer_id)
)

CREATE TABLE review
(
user_id int not null,
answer_id int not null,
date_answered datetime not null

CONSTRAINT pk_review primary key (user_id, answer_id, date_answered)
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

 ALTER TABLE review
 ADD FOREIGN KEY (user_id) REFERENCES users(id);

 ALTER TABLE review
 ADD FOREIGN KEY (answer_id) REFERENCES answers(answer_id);


INSERT INTO scenarios (scenario_name, description, scenario_image, question, isActive) VALUES ('Classroom', 'Your teacher asks you to read aloud.', 'https://i.ibb.co/BHhfmSs/classroomscenario.png', 'Would you like to read?', 1);
INSERT INTO scenarios (scenario_name, description, scenario_image, question, isActive ) VALUES ('Hallway', 'You want to place your books in your locker. However, your neighbor is blocking your locker.', 'https://i.ibb.co/rGXTKzW/hallwayscenario.png', 'Do you:', 1);
INSERT INTO scenarios (scenario_name, description, scenario_image, question, isActive ) VALUES ('Soccer', 'You need to choose your next move.', 'https://i.ibb.co/PZz8Bcc/soccerstealball.png', 'Do you:', 1);

INSERT INTO answers (scenario_id, answer_text, response_text, response_image, response_color, emoji) VALUES (1, 'A) Sure', 'Good job!', 'https://i.ibb.co/7VzshNp/classroomsure.png', 'green', 'https://i.ibb.co/4WvL2KP/happy-emoji.png');
INSERT INTO answers (scenario_id, answer_text, response_text, response_image, response_color, emoji) VALUES (1, 'B) No', 'People will support you if you try.', 'https://i.ibb.co/JCpQdMm/classroomno.png', 'blue', 'https://i.ibb.co/2NzQMKn/sad-emoji.png');
INSERT INTO answers (scenario_id, answer_text, response_text, response_image, response_color, emoji) VALUES (1, 'C) Start reading', 'Your teacher expects you to answer before reading.', 'https://i.ibb.co/2Z6r6Xc/classroomstartreading.png', 'red', 'https://i.ibb.co/QFL55zM/angry-emoji.png');
INSERT INTO answers (scenario_id, answer_text, response_text, response_image, response_color, emoji) VALUES (2, 'A) Walk away', 'People will help if you ask.', 'https://i.ibb.co/jyq6RTg/hallwaywalkaway.png', 'blue', 'https://i.ibb.co/2NzQMKn/sad-emoji.png');
INSERT INTO answers (scenario_id, answer_text, response_text, response_image, response_color, emoji) VALUES (2, 'B) Squeeze in front of classmate', 'Next time ask me to move over and I will.', 'https://i.ibb.co/FgG63WH/hallwaysqueeze.png', 'red', 'https://i.ibb.co/QFL55zM/angry-emoji.png');
INSERT INTO answers (scenario_id, answer_text, response_text, response_image, response_color, emoji) VALUES (2, 'C) Say "excuse me"', 'Classmates will always listen if you are nice to them', 'https://i.ibb.co/W6w3hm7/hallwaysayexcuseme.png', 'green', 'https://i.ibb.co/4WvL2KP/happy-emoji.png');
INSERT INTO answers (scenario_id, answer_text, response_text, response_image, response_color, emoji) VALUES (3, 'A) Steal ball', 'You should ask for the pass. If you hurt others feelings it is good to apologize.', 'https://i.ibb.co/PZz8Bcc/soccerstealball.png', 'blue', 'https://i.ibb.co/2NzQMKn/sad-emoji.png');
INSERT INTO answers (scenario_id, answer_text, response_text, response_image, response_color, emoji) VALUES (3, 'B) Do/Say nothing', 'The other team scored and everyone is happy for them.', 'https://i.ibb.co/dP7C4vG/soccernothing.png', 'green', 'https://i.ibb.co/4WvL2KP/happy-emoji.png');
INSERT INTO answers (scenario_id, answer_text, response_text, response_image, response_color, emoji) VALUES (3, 'C) Call for ball', 'Nice shot!', 'https://i.ibb.co/F09GG04/soccershot.png', 'green', 'https://i.ibb.co/4WvL2KP/happy-emoji.png');
 
 INSERT INTO users (username, password, salt, role) VALUES ('admin@admin.com', 'UT3zZWao733yZCgthmNJJxs5NCg=', 'NuE0Y6FonAI=', 'Admin');
 INSERT INTO users (username, password, salt, role) VALUES ('teacher@teacher.com', 'UT3zZWao733yZCgthmNJJxs5NCg=', 'NuE0Y6FonAI=', 'Teacher');
  INSERT INTO users (username, password, salt, role) VALUES ('student@student.com', 'UT3zZWao733yZCgthmNJJxs5NCg=', 'NuE0Y6FonAI=', 'Student');

COMMIT TRANSACTION;