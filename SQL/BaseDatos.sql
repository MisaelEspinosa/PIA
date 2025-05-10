USE [master]
GO

CREATE DATABASE [DBFACPYA]
GO
USE [DBFACPYA]
GO

-- Tabla: Pais
CREATE TABLE Pais (
    IdPais INT PRIMARY KEY IDENTITY(1,1),
    Nombre VARCHAR(100) NOT NULL,
    Region VARCHAR(100) NULL
);

-- Tabla: Cliente
CREATE TABLE Cliente (
    IdCliente INT PRIMARY KEY IDENTITY(1,1),
    NombreCompleto VARCHAR(150) NOT NULL,
    Correo VARCHAR(150) NOT NULL,
    Telefono VARCHAR(20),
    PaisOrigenId INT NOT NULL,
    FOREIGN KEY (PaisOrigenId) REFERENCES Pais(IdPais)
);

-- Tabla: PaqueteViaje
CREATE TABLE PaqueteViaje (
    IdPaquete INT PRIMARY KEY IDENTITY(1,1),
    Nombre VARCHAR(150) NOT NULL,
    Descripcion VARCHAR(250),
    Precio DECIMAL(10,2) NOT NULL,
    DestinoPaisId INT NOT NULL,
    FOREIGN KEY (DestinoPaisId) REFERENCES Pais(IdPais)
);

-- Tabla: Reservacion
CREATE TABLE Reservacion (
    IdReservacion INT PRIMARY KEY IDENTITY(1,1),
    ClienteId INT NOT NULL,
    PaqueteId INT NOT NULL,
    FechaReservacion DATETIME DEFAULT GETDATE(),
    FOREIGN KEY (ClienteId) REFERENCES Cliente(IdCliente),
    FOREIGN KEY (PaqueteId) REFERENCES PaqueteViaje(IdPaquete)
);
