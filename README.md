WebAPI- CRUD Operation With Onion Architecture

Onion Architecture contains four layers,
Domain Layer
Repository Layer
Service Layer
UI/Presentation Layer

Web API with the following characteristics :

ASP.Core 2.2
EntityFramework
FluentValidation
Nlogger
Jwt
XUnit Testing Framework


This application is connected to SQL Server, you need create UserInfo Table in your local sql server

Please ref below script :


CREATE TABLE [dbo].[UserInfo](
	[Id] [int] IDENTITY(1,1) NOT NULL,
	[FirstName] [nvarchar](50) NOT NULL,
	[LastName] [nvarchar](100) NULL,
	[Email] [nvarchar](50) NULL,
	[PhoneNumber] [nvarchar](20) NULL,
	[IsActive] [bit] NULL,
 CONSTRAINT [PK_UserInfo] PRIMARY KEY CLUSTERED 
(
	[Id] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON, FILLFACTOR = 70) ON [PRIMARY]
) ON [PRIMARY]  

