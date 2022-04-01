
CREATE TABLE [dbo].[Team]
(
	[Stamnummer] INT NOT NULL PRIMARY KEY, 
    [Naam] NVARCHAR(150) NOT NULL, 
    [Bijnaam] NVARCHAR(150) NULL
)

CREATE TABLE [dbo].[Speler]
(
	[Id] INT NOT NULL PRIMARY KEY IDENTITY, 
    [Naam] VARCHAR(150) NOT NULL, 
    [Rugnummer] INT NULL, 
    [Lengte] INT NULL, 
    [Gewicht] INT NULL, 
    [TeamId] INT NULL,
    CONSTRAINT [FK_Speler_Team] FOREIGN KEY ([TeamId]) REFERENCES [Team]([Stamnummer])
)

CREATE TABLE [dbo].[Transfer]
(
	[Id] INT NOT NULL PRIMARY KEY IDENTITY, 
    [SpelerId] INT NOT NULL, 
    [Prijs] INT NOT NULL, 
    [OudTeamId] INT NULL, 
    [NieuwTeamId] INT NULL,
    CONSTRAINT [FK_Transfer_Speler] FOREIGN KEY ([SpelerId]) REFERENCES [Speler]([Id]),
    CONSTRAINT [FK_Transfer_OudTeam] FOREIGN KEY ([OudTeamId]) REFERENCES [Team]([Stamnummer]),
    CONSTRAINT [FK_Transfer_NieuwTeam] FOREIGN KEY ([NieuwTeamId]) REFERENCES [Team]([Stamnummer])
)