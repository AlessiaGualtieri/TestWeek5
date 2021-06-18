--CREATE DATABASE Universita

CREATE TABLE Studente
(
	ID INT IDENTITY(1,1) PRIMARY KEY,
	Nome VARCHAR(30) NOT NULL,
	Cognome VARCHAR(30) NOT NULL,
	AnnoNascita INT NOT NULL,
	CHECK (AnnoNascita > 1900)
)

CREATE TABLE Esame
(
	ID INT IDENTITY(1,1) PRIMARY KEY,
	Nome VARCHAR(40) NOT NULL,
	CFU INT NOT NULL,
	DataEsame DATE NOT NULL,
	Votazione INT NOT NULL,
	Passato VARCHAR(2) NOT NULL,
	CHECK(Votazione >= 0),
	CHECK(Votazione <= 31),
	CHECK(Passato IN ('Si','No')),
	CHECK(CFU > 0)
)

CREATE TABLE StudenteEsame
(
	StudenteID INT NOT NULL,
	EsameID INT NOT NULL,
	FOREIGN KEY(StudenteID) REFERENCES Studente(ID),
	FOREIGN KEY(EsameID) REFERENCES Esame(ID)
)


INSERT INTO Studente VALUES
('Mario','Rossi',1998),
('Maria','Ross', 1996),
('Giuseppe','Galli',1997),
('Signora','Incontrada',1998)

INSERT INTO Esame VALUES
('Analisi Matematica 1', 9, '2021/02/18',30,'Si'),
('Analisi Matematica 2', 9, '2021/06/11',29,'Si'),
('Analisi Matematica 3', 9, '2021/06/18',25,'Si')






