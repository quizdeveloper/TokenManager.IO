USE [master]
GO
/****** Object:  Database [TokenManager]    Script Date: 1/2/2022 11:03:00 AM ******/
CREATE DATABASE [TokenManager]
GO
USE [TokenManager]
GO
/****** Object:  Table [dbo].[Token]    Script Date: 1/2/2022 11:03:00 AM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[Token](
	[Id] [int] IDENTITY(8,1) NOT NULL,
	[Symbol] [varchar](5) NOT NULL,
	[Name] [varchar](50) NOT NULL,
	[TotalSupply] [bigint] NOT NULL,
	[ContractAddress] [varchar](66) NOT NULL,
	[TotalHolders] [int] NOT NULL,
	[Price] [decimal](38, 2) NULL,
 CONSTRAINT [PK_Token] PRIMARY KEY CLUSTERED 
(
	[Id] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY]
) ON [PRIMARY]

GO
SET IDENTITY_INSERT [dbo].[Token] ON 
GO
INSERT [dbo].[Token] ([Id], [Symbol], [Name], [TotalSupply], [ContractAddress], [TotalHolders], [Price]) VALUES (8, N'VEN', N'Vechain', 35987133, N'0xd850942ef8811f2a866692a623011bde52a462c1', 65, CAST(0.00 AS Decimal(38, 2)))
GO
INSERT [dbo].[Token] ([Id], [Symbol], [Name], [TotalSupply], [ContractAddress], [TotalHolders], [Price]) VALUES (9, N'ZIR', N'Zilliqa', 53272942, N'0x05f4a42e251f2d52b8ed15e9fedaacfcef1fad27', 54, CAST(0.00 AS Decimal(38, 2)))
GO
INSERT [dbo].[Token] ([Id], [Symbol], [Name], [TotalSupply], [ContractAddress], [TotalHolders], [Price]) VALUES (10, N'MKR', N'Maker', 45987133, N'0x9f8f72aa9304c8b593d555f12ef6589cc3a579a2', 567, CAST(0.00 AS Decimal(38, 2)))
GO
INSERT [dbo].[Token] ([Id], [Symbol], [Name], [TotalSupply], [ContractAddress], [TotalHolders], [Price]) VALUES (11, N'BNB', N'Binance', 16579517, N'0xB8c77482e45F1F44dE1745F52C74426C631bDD52', 4234234, CAST(0.00 AS Decimal(38, 2)))
GO
SET IDENTITY_INSERT [dbo].[Token] OFF
GO
ALTER TABLE [dbo].[Token] ADD  CONSTRAINT [DF_Token_Price]  DEFAULT ((0.0)) FOR [Price]

GO

/****** Object:  StoredProcedure [dbo].[SP_Token_Count]    Script Date: 1/2/2022 11:03:00 AM ******/
CREATE PROCEDURE [dbo].[SP_Token_Count](
  @Keyword VARCHAR(50)
)AS
BEGIN
	 SELECT COUNT(0) AS TotalRecord
	 FROM Token 
	 WHERE ((@Keyword IS NULL OR @Keyword = '') OR ([Name] like '%'+ @Keyword + '%' OR [Symbol] like '%'+ @Keyword + '%'));
END

GO

/****** Object:  StoredProcedure [dbo].[SP_Token_Create]    Script Date: 1/2/2022 11:03:00 AM ******/
CREATE PROCEDURE [dbo].[SP_Token_Create](
  @Name VARCHAR(50),
  @Symbol VARCHAR(5),
  @TotalSupply BIGINT,
  @ContractAddress VARCHAR(66),
  @TotalHolders INT
)AS
BEGIN
	 INSERT Token ([Symbol], [Name], [TotalSupply], [ContractAddress], [TotalHolders], [Price]) 
	 VALUES (@Symbol, @Name, @TotalSupply, @ContractAddress, @TotalHolders, 0)
END

GO

/****** Object:  StoredProcedure [dbo].[SP_Token_Export]    Script Date: 1/2/2022 11:03:00 AM ******/
CREATE PROCEDURE [dbo].[SP_Token_Export](
  @Keyword VARCHAR(50)
)AS
BEGIN
	 SELECT [Id], [Symbol], [Name], [TotalSupply], [ContractAddress], [TotalHolders], [Price]
	 FROM Token 
	 WHERE ((@Keyword IS NULL OR @Keyword = '') OR ([Name] like '%'+ @Keyword + '%' OR [Symbol] like '%'+ @Keyword + '%')) 
	 ORDER BY Id DESC
END

GO

/****** Object:  StoredProcedure [dbo].[SP_Token_Find]    Script Date: 1/2/2022 11:03:00 AM ******/
CREATE PROCEDURE [dbo].[SP_Token_Find](
  @Keyword VARCHAR(50),
  @PageIndex INTEGER,
  @PageItem INTEGER
)AS
BEGIN
	 SELECT [Id], [Symbol], [Name], [TotalSupply], [ContractAddress], [TotalHolders], [Price]
	 FROM Token 
	 WHERE ((@Keyword IS NULL OR @Keyword = '') OR ([Name] like '%'+ @Keyword + '%' OR [Symbol] like '%'+ @Keyword + '%')) 
	 ORDER BY Id DESC
	 OFFSET ((@PageIndex - 1) * @PageItem) 
	 ROWS FETCH NEXT @PageItem Rows ONLY
END

GO

/****** Object:  StoredProcedure [dbo].[SP_Token_GetById]    Script Date: 1/2/2022 11:03:00 AM ******/
CREATE PROCEDURE [dbo].[SP_Token_GetById](
  @Id INTEGER
)AS
BEGIN
	 SELECT [Id], [Symbol], [Name], [TotalSupply], [ContractAddress], [TotalHolders], [Price]
	 FROM Token 
	 WHERE Id = @Id;
END

GO

/****** Object:  StoredProcedure [dbo].[SP_Token_GetBySymbol]    Script Date: 1/2/2022 11:03:00 AM ******/
CREATE PROCEDURE [dbo].[SP_Token_GetBySymbol](
  @Symbol VARCHAR(5)
)AS
BEGIN
	 SELECT [Id], [Symbol], [Name], [TotalSupply], [ContractAddress], [TotalHolders], [Price]
	 FROM Token 
	 WHERE [Symbol] = @Symbol;
END

GO

/****** Object:  StoredProcedure [dbo].[SP_Token_SumSupply]    Script Date: 1/2/2022 11:03:00 AM ******/
CREATE PROCEDURE [dbo].[SP_Token_SumSupply]
AS
BEGIN
	 SELECT SUM([TotalSupply]) AS SumSupply
	 FROM Token
END

GO

/****** Object:  StoredProcedure [dbo].[SP_Token_Update]    Script Date: 1/2/2022 11:03:00 AM ******/
CREATE PROCEDURE [dbo].[SP_Token_Update](
  @Id INTEGER,
  @Name VARCHAR(50),
  @Symbol VARCHAR(5),
  @TotalSupply BIGINT,
  @ContractAddress VARCHAR(66),
  @TotalHolders INTEGER
)AS
BEGIN
   IF(EXISTS(SELECT COUNT(0) FROM Token WHERE [Id] = @Id))
   BEGIN
	 UPDATE Token SET [Symbol] = @Symbol, [Name] = @Name, [TotalSupply] = @TotalSupply, [ContractAddress] = @ContractAddress, [TotalHolders] = @TotalHolders
	 WHERE [Id] = @Id;
   END
END

GO

/****** Object:  StoredProcedure [dbo].[SP_Token_UpdatePrice]    Script Date: 1/2/2022 11:03:00 AM ******/
CREATE PROCEDURE [dbo].[SP_Token_UpdatePrice](
  @Id INTEGER,
  @Price DECIMAL(38,2)
)AS
BEGIN
   IF(EXISTS(SELECT COUNT(0) FROM Token WHERE [Id] = @Id))
   BEGIN
	 UPDATE Token SET [Price] = @Price
	 WHERE [Id] = @Id;
   END
END

