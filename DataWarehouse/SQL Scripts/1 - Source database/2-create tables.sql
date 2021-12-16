use [sep4-db]

SET ANSI_NULLS ON
GO

SET QUOTED_IDENTIFIER ON
GO


-- create accounts table
CREATE TABLE [dbo].[Accounts](
	[Username] [nvarchar](50) NOT NULL,
	[Password] [nvarchar](50) NOT NULL,
	[DateOfBirth] [datetime] NOT NULL,
	[Gender] [nvarchar](50) NULL,
	[Region] [nvarchar](50) NULL,
 CONSTRAINT [PK_Accounts] PRIMARY KEY CLUSTERED 
(
	[Username] ASC
)WITH (STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, OPTIMIZE_FOR_SEQUENTIAL_KEY = OFF) ON [PRIMARY]
) ON [PRIMARY]
GO

ALTER TABLE [dbo].[Accounts] ADD  DEFAULT ('1900-01-01T00:00:00.0000000') FOR [DateOfBirth]
GO


-- create plants table
SET ANSI_NULLS ON
GO

SET QUOTED_IDENTIFIER ON
GO

CREATE TABLE [dbo].[Plants](
	[Nickname] [nvarchar](50) NULL,
	[DOB] [datetime] NOT NULL,
	[EUI] [nvarchar](50) NOT NULL,
	[AccountUsername] [nvarchar](50) NULL,
	[PlantType] [nvarchar](50) NULL,
 CONSTRAINT [PK_Plants] PRIMARY KEY CLUSTERED 
(
	[EUI] ASC
)WITH (STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, OPTIMIZE_FOR_SEQUENTIAL_KEY = OFF) ON [PRIMARY]
) ON [PRIMARY]
GO

ALTER TABLE [dbo].[Plants] ADD  DEFAULT ('1900-01-01T00:00:00.0000000') FOR [DOB]
GO

ALTER TABLE [dbo].[Plants] ADD  DEFAULT (N'') FOR [EUI]
GO

ALTER TABLE [dbo].[Plants]  WITH CHECK ADD  CONSTRAINT [FK_Plants_Accounts_AccountUsername] FOREIGN KEY([AccountUsername])
REFERENCES [dbo].[Accounts] ([Username])
GO

ALTER TABLE [dbo].[Plants] CHECK CONSTRAINT [FK_Plants_Accounts_AccountUsername]
GO




-- create measurements table
SET ANSI_NULLS ON
GO

SET QUOTED_IDENTIFIER ON
GO

CREATE TABLE [dbo].[Measurements](
	[Id] [int] IDENTITY(1,1) NOT NULL,
	[TimeStamp] [datetime] NOT NULL,
	[Temperature] [decimal](18, 2) NOT NULL,
	[Humidity] [decimal](18, 2) NOT NULL,
	[CO2] [decimal](18, 2) NOT NULL,
	[Light] [decimal](18, 2) NOT NULL,
	[PlantEUI] [nvarchar](50) NULL,
 CONSTRAINT [PK_Measurements] PRIMARY KEY CLUSTERED 
(
	[Id] ASC
)WITH (STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, OPTIMIZE_FOR_SEQUENTIAL_KEY = OFF) ON [PRIMARY]
) ON [PRIMARY]
GO

ALTER TABLE [dbo].[Measurements] ADD  DEFAULT ((0.0)) FOR [Light]
GO

ALTER TABLE [dbo].[Measurements]  WITH CHECK ADD  CONSTRAINT [FK_Measurements_Plants_PlantEUI] FOREIGN KEY([PlantEUI])
REFERENCES [dbo].[Plants] ([EUI])
GO

ALTER TABLE [dbo].[Measurements] CHECK CONSTRAINT [FK_Measurements_Plants_PlantEUI]
GO