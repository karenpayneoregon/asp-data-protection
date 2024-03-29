USE [MockupApplication]
GO
/****** Object:  Table [dbo].[UserLogin]    Script Date: 3/4/2024 8:00:59 AM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[UserLogin](
	[Id] [int] IDENTITY(1,1) NOT NULL,
	[EmailAddress] [nvarchar](max) NOT NULL,
	[Pin] [nchar](5) NOT NULL,
 CONSTRAINT [PK_UserLogin] PRIMARY KEY CLUSTERED 
(
	[Id] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON, OPTIMIZE_FOR_SEQUENTIAL_KEY = OFF) ON [PRIMARY]
) ON [PRIMARY] TEXTIMAGE_ON [PRIMARY]
GO
SET IDENTITY_INSERT [dbo].[UserLogin] ON 

INSERT [dbo].[UserLogin] ([Id], [EmailAddress], [Pin]) VALUES (1, N'payne@comcast.net', N'12345')
INSERT [dbo].[UserLogin] ([Id], [EmailAddress], [Pin]) VALUES (2, N'billybob@gmail.com', N'55556')
INSERT [dbo].[UserLogin] ([Id], [EmailAddress], [Pin]) VALUES (3, N'mary@yahoo.com', N'97865')
INSERT [dbo].[UserLogin] ([Id], [EmailAddress], [Pin]) VALUES (4, N'jimj@gmail.com', N'37179')
INSERT [dbo].[UserLogin] ([Id], [EmailAddress], [Pin]) VALUES (5, N'adam@comcast.net', N'66666')
SET IDENTITY_INSERT [dbo].[UserLogin] OFF
GO
USE [master]
GO
ALTER DATABASE [MockupApplication] SET  READ_WRITE 
GO
