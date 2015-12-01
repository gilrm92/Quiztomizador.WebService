USE Quiztomizador_Server
SELECT * FROM Questionario
SELECT * FROM Questionario_Usuario

ALTER TABLE Questionario
ADD IdUsuarioCriador INT NOT NULL

ALTER TABLE Questionario
ADD DataCriacao DATETIME NOT NULL DEFAULT(GETDATE())

ALTER TABLE Questionario
ADD Excluido BIT DEFAULT(0) NOT NULL

ALTER TABLE Questionario
ADD CONSTRAINT FK_Usuario_Questionario
FOREIGN KEY (IdUsuarioCriador)
REFERENCES Usuarios(IdUsuario)


http://bruno.penha.nom.br/cm_java_web/


