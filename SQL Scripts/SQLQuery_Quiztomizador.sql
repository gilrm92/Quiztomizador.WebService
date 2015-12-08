CREATE TABLE Usuario
(
	IdUsuario INT IDENTITY (1,1) NOT NULL PRIMARY KEY,
	Nome VARCHAR(100),
	Email VARCHAR(100),
	Senha VARCHAR(100)
)

CREATE TABLE Questionario
(
	IdQuestionario INT IDENTITY (1,1) NOT NULL PRIMARY KEY,
	IdUsuarioCriador INT NOT NULL,
	IdCategoria INT,
	Descricao VARCHAR(500),
	Excluido BIT DEFAULT(0) NOT NULL,
	Publico BIT  DEFAULT(0) NOT NULL
)


CREATE TABLE Categoria
(
	IdCategoria INT IDENTITY (1,1) NOT NULL PRIMARY KEY,
	IdUsuarioCriador INT NOT NULL,
	Descricao VARCHAR(100),
	Excluido BIT DEFAULT(0) NOT NULL
)


CREATE TABLE Questao
(
	IdQuestao INT IDENTITY (1,1) NOT NULL PRIMARY KEY,
	IdQuestionario INT NOT NULL,
	Tipo INT NOT NULL,
	Titulo VARCHAR(200),
	Excluido BIT DEFAULT(0) NOT NULL
)

CREATE TABLE Alternativa
(
	IdAlternativa INT IDENTITY (1,1) NOT NULL PRIMARY KEY,
	IdQuestao INT,
	Descricao VARCHAR(100),
	Certa BIT DEFAULT(0) NOT NULL
)

CREATE TABLE Teste
(
	IdTeste INT IDENTITY (1,1) NOT NULL PRIMARY KEY,
    Descricao VARCHAR(100),     
    Inicio DATETIME,
    Termino DATETIME,
    Erros INT,
    Acertos INT,
    IdUsuario INT
)

CREATE TABLE Questionario_Usuario
(
	IdUsuario INT NOT NULL,
	IdQuestionario INT NOT NULL
	CONSTRAINT PK_Questionario_Usuario PRIMARY KEY (IdUsuario, IdQuestionario)
)

CREATE TABLE Questionario_Grupo
(
	IdGrupo INT NOT NULL,
	IdQuestionario INT NOT NULL
)

CREATE TABLE Grupo
(
	IdGrupo INT IDENTITY (1,1) NOT NULL PRIMARY KEY,
	Titulo VARCHAR(150),
)

CREATE TABLE Grupo_Usuario
(
	IdUsuario INT NOT NULL,
	IdGrupo INT NOT NULL,
	Admin BIT,
	Participa BIT
)



--###Relacionamentos.
ALTER TABLE Grupo_Usuario
ADD CONSTRAINT FK_Grupo_Usuario_Grupo
FOREIGN KEY (IdGrupo)
REFERENCES Grupo(IdGrupo)
ON DELETE CASCADE

ALTER TABLE Grupo_Usuario
ADD CONSTRAINT FK_Grupo_Usuario_Usuario
FOREIGN KEY (IdUsuario)
REFERENCES Usuarios(IdUsuario)
ON DELETE CASCADE

ALTER TABLE Grupo_Usuario
ADD CONSTRAINT PK_Grupo_Usuario
PRIMARY KEY (IdGrupo, IdUsuario)


ALTER TABLE Questionario_Grupo
ADD CONSTRAINT FK_Questionario_Grupo_Grupo
FOREIGN KEY (IdGrupo)
REFERENCES Grupo(IdGrupo)
ON DELETE CASCADE

ALTER TABLE Questionario_Grupo
ADD CONSTRAINT FK_Questionario_Grupo_Questionario
FOREIGN KEY (IdQuestionario)
REFERENCES Questionario(IdQuestionario)
ON DELETE CASCADE

ALTER TABLE Questionario_Grupo
ADD CONSTRAINT PK_Questionario_Grupo
PRIMARY KEY (IdQuestionario, IdGrupo)


ALTER TABLE Questionario_Usuario
ADD CONSTRAINT FK_Questionario_Usuario_Usuario
FOREIGN KEY (IdUsuario)
REFERENCES Usuario(IdUsuario)
ON DELETE CASCADE

ALTER TABLE Questionario_Usuario
ADD CONSTRAINT FK_Questionario_Usuario_Questionario
FOREIGN KEY (IdQuestionario)
REFERENCES Questionario(IdQuestionario)
ON DELETE CASCADE

ALTER TABLE Questionario_Usuario
ADD CONSTRAINT PK_Questionario_Usuario
PRIMARY KEY (IdQuestionario, IdUsuario)

ALTER TABLE Questionario
ADD CONSTRAINT FK_Questionario_Categoria
FOREIGN KEY (IdCategoria)
REFERENCES Categoria(IdCategoria)
ON DELETE CASCADE

ALTER TABLE Questionario
ADD CONSTRAINT FK_Usuario_Questionario
FOREIGN KEY (IdUsuarioCriador)
REFERENCES Usuario(IdUsuario)

ALTER TABLE Categoria
ADD CONSTRAINT FK_Usuario_Categoria
FOREIGN KEY (IdUsuarioCriador)
REFERENCES Usuario(IdUsuario)

ALTER TABLE Questao
ADD CONSTRAINT FK_Questao_Questionario
FOREIGN KEY (IdQuestionario)
REFERENCES Questionario(IdQuestionario)
ON DELETE CASCADE

ALTER TABLE Alternativa
ADD CONSTRAINT FK_Alternativa_Questao
FOREIGN KEY (IdQuestao)
REFERENCES Questao(IdQuestao)
ON DELETE CASCADE

ALTER TABLE Teste
ADD CONSTRAINT FK_Usuario_Teste
FOREIGN KEY (IdUsuario)
REFERENCES Usuario(IdUsuario)

