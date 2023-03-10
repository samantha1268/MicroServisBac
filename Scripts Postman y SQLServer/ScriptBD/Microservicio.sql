USE [MicroServicio]
GO
/****** Object:  Table [dbo].[Cliente]    Script Date: 11/3/2023 23:44:32 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[Cliente](
	[clienteid] [int] NOT NULL,
	[contraseña] [nchar](30) NOT NULL,
	[estado] [nchar](20) NULL,
	[IdPersona] [int] NOT NULL,
PRIMARY KEY CLUSTERED 
(
	[clienteid] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON, OPTIMIZE_FOR_SEQUENTIAL_KEY = OFF) ON [PRIMARY]
) ON [PRIMARY]
GO
/****** Object:  Table [dbo].[Cuenta]    Script Date: 11/3/2023 23:44:32 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[Cuenta](
	[IdNúmeroCuenta] [int] NOT NULL,
	[númeroCuenta] [int] NULL,
	[tipoCuenta] [nchar](50) NULL,
	[saldoInicial] [int] NULL,
	[estado] [nchar](20) NULL,
	[Idcliente] [int] NULL,
PRIMARY KEY CLUSTERED 
(
	[IdNúmeroCuenta] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON, OPTIMIZE_FOR_SEQUENTIAL_KEY = OFF) ON [PRIMARY]
) ON [PRIMARY]
GO
/****** Object:  Table [dbo].[Movimento]    Script Date: 11/3/2023 23:44:32 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[Movimento](
	[IdMovimiento] [int] NOT NULL,
	[Fecha] [date] NULL,
	[tipoMovimiento] [nchar](20) NULL,
	[valor] [int] NULL,
	[saldo] [int] NULL,
	[IdCuenta] [int] NULL,
PRIMARY KEY CLUSTERED 
(
	[IdMovimiento] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON, OPTIMIZE_FOR_SEQUENTIAL_KEY = OFF) ON [PRIMARY]
) ON [PRIMARY]
GO
/****** Object:  Table [dbo].[Persona]    Script Date: 11/3/2023 23:44:32 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[Persona](
	[IdPesona] [int] NOT NULL,
	[nombre] [nchar](30) NULL,
	[genero] [nchar](15) NULL,
	[edad] [int] NULL,
	[identificación] [nchar](30) NULL,
	[dirección] [nchar](100) NULL,
	[teléfono] [nchar](13) NULL,
PRIMARY KEY CLUSTERED 
(
	[IdPesona] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON, OPTIMIZE_FOR_SEQUENTIAL_KEY = OFF) ON [PRIMARY]
) ON [PRIMARY]
GO
ALTER TABLE [dbo].[Cliente]  WITH CHECK ADD FOREIGN KEY([IdPersona])
REFERENCES [dbo].[Persona] ([IdPesona])
GO
ALTER TABLE [dbo].[Cuenta]  WITH CHECK ADD FOREIGN KEY([Idcliente])
REFERENCES [dbo].[Cliente] ([clienteid])
GO
ALTER TABLE [dbo].[Movimento]  WITH CHECK ADD FOREIGN KEY([IdCuenta])
REFERENCES [dbo].[Cuenta] ([IdNúmeroCuenta])
GO
/****** Object:  StoredProcedure [dbo].[ReportMovimientoPorFecha]    Script Date: 11/3/2023 23:44:32 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE PROCEDURE [dbo].[ReportMovimientoPorFecha] @fecha date,@usuario int
AS
SELECT  Movimento.Fecha, Persona.nombre,Cuenta.númeroCuenta,Cuenta.tipoCuenta,Cuenta.saldoInicial,Cuenta.estado,Movimento.valor,Cuenta.saldoInicial  FROM Movimento
INNER JOIN Cuenta ON Movimento.IdMovimiento = Cuenta.IdNúmeroCuenta
INNER JOIN Cliente ON Cuenta.Idcliente = Cliente.clienteid 
INNER JOIN Persona ON Persona.IdPesona = Cliente.clienteid 
where @fecha =Movimento.Fecha and @usuario= Cliente.clienteid  
GO
