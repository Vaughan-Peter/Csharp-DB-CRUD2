/* Delete all tables    */    

DROP TABLE IF EXISTS [dbo].[Person];
DROP TABLE IF EXISTS [dbo].[Hobby];
DROP TABLE IF EXISTS [dbo].[Occupation];

/* Create Tables    */    

CREATE TABLE [dbo].[Person] (
    [Id]        INT        IDENTITY (1, 1) NOT NULL,
    [firstName] NCHAR (20) NOT NULL,
    [lastName]  NCHAR (20) NOT NULL,
    [city]      NCHAR (20) NOT NULL,
    [hair]      NCHAR (10) NULL,
    [eyes]      NCHAR (10) NULL,
    [nose]      NCHAR (10) NULL,
    [mouth]     NCHAR (10) NULL,
    [HobbyID]   INT        NULL,
    [JobID]     INT        NULL,
    PRIMARY KEY CLUSTERED ([Id] ASC),
);

CREATE TABLE [dbo].[Hobby] (
    [Id]   INT        IDENTITY (1, 1) NOT NULL,
    [name] NCHAR (10) NULL,
    PRIMARY KEY CLUSTERED ([Id] ASC)
);

CREATE TABLE [dbo].[Occupation] (
    [Id]   INT        IDENTITY (1, 1) NOT NULL,
    [name] NCHAR (10) NULL,
    PRIMARY KEY CLUSTERED ([Id] ASC)
);

/* Create Foreign Key Constraints    */    
ALTER TABLE [dbo].[Person]
ADD CONSTRAINT FK_OccupationID
FOREIGN KEY (jobID) REFERENCES Occupation(Id);

ALTER TABLE [dbo].[Person]
ADD CONSTRAINT FK_HobbyID
FOREIGN KEY (hobbyID) REFERENCES Hobby(Id);

/* Populate tables    */    
INSERT INTO Occupation  (Name)  VALUES ('Teacher');
INSERT INTO Occupation  (Name)  VALUES ('Doctor');
INSERT INTO Occupation  (Name)  VALUES ('Student');
INSERT INTO Occupation  (Name)  VALUES ('Coder');
INSERT INTO Occupation  (Name)  VALUES ('Fighter');

INSERT INTO Hobby  (Name)  VALUES ('Cooking');
INSERT INTO Hobby  (Name)  VALUES ('Student');
INSERT INTO Hobby  (Name)  VALUES ('Idealist');

INSERT INTO Person  (firstName ,lastName , city, hair, eyes, nose, mouth, hobbyID, jobID)  VALUES ('Russ1', 'DaShan1', 'Kentville1', 'hair1', 'eyes1', 'nose1', 'mouth1', 1, 2);
INSERT INTO Person  (firstName ,lastName, city, hair, eyes, nose, mouth, hobbyID, jobID)  VALUES ('Russ2', 'DaShan2', 'Kentville2',  'hair2', 'eyes2', 'nose2', 'mouth2', 2, 1);
INSERT INTO Person  (firstName, lastName, city, hair, eyes, nose, mouth, hobbyID, jobID)  VALUES ('Russ3', 'DaShan3', 'Kentville3',  'hair3', 'eyes3', 'nose3', 'mouth3', 3, 3);

/* Sample select statements    */    
Select * FROM Hobby;
Select * FROM Occupation;
Select * FROM Person;

Select * From Hobby WHERE id = 1;

