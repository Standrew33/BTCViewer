USE [Cryptomarket]
GO

/****** Object:  Table [dbo].[btc_data]    Script Date: 25.04.2024 0:55:59 ******/
SET ANSI_NULLS ON
GO

SET QUOTED_IDENTIFIER ON
GO

CREATE TABLE [dbo].[btc_data](
	[id] [int] IDENTITY(1,1) NOT NULL,
	[updated] [nvarchar](50) NULL,
	[updated_iso] [nvarchar](50) NULL,
	[updated_uk] [nvarchar](50) NULL,
	[chart_name] [nvarchar](50) NULL,
	[disclaimer] [text] NULL,
 CONSTRAINT [PK_btc_data] PRIMARY KEY CLUSTERED 
(
	[id] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON, OPTIMIZE_FOR_SEQUENTIAL_KEY = OFF) ON [PRIMARY]
) ON [PRIMARY] TEXTIMAGE_ON [PRIMARY]
GO

CREATE TABLE [dbo].[bpi](
	[id] [int] IDENTITY(1,1) NOT NULL,
	[btc_data_id] [int] NOT NULL,
	[code] [nvarchar](50) NULL,
	[symbol] [nvarchar](50) NULL,
	[rate] [nvarchar](50) NULL,
	[description] [nvarchar](100) NULL,
	[rate_float] [decimal](18, 4) NULL,
	[note] [text] NULL,
 CONSTRAINT [PK_bpi] PRIMARY KEY CLUSTERED 
(
	[id] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON, OPTIMIZE_FOR_SEQUENTIAL_KEY = OFF) ON [PRIMARY]
) ON [PRIMARY] TEXTIMAGE_ON [PRIMARY]
GO

ALTER TABLE [dbo].[bpi]  WITH CHECK ADD  CONSTRAINT [FK_bpi_btc_data] FOREIGN KEY([btc_data_id])
REFERENCES [dbo].[btc_data] ([id])
GO

ALTER TABLE [dbo].[bpi] CHECK CONSTRAINT [FK_bpi_btc_data]
GO
