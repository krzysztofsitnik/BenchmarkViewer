USE master
IF NOT EXISTS(SELECT * FROM sys.databases WHERE NAME='BenchmarkViewer')
	CREATE DATABASE BenchmarkViewer
USE BenchmarkViewer

DROP TABLE BenchmarkMeasurments
DROP TABLE Benchmarks

IF NOT EXISTS (SELECT * FROM sysobjects WHERE name='Benchmarks')
	CREATE TABLE Benchmarks(
		Name NVARCHAR(512) NOT NULL UNIQUE,
		Id INT NOT NULL PRIMARY KEY IDENTITY (1,1))			

IF NOT EXISTS (SELECT * FROM sysobjects WHERE name='BenchmarkMeasurments')
	CREATE TABLE BenchmarkMeasurments(
		BenchmarkID INT NOT NULL FOREIGN KEY REFERENCES Benchmarks(Id),
		Date DATETIME NOT NULL,
		Value DECIMAL NOT NULL,
		MetricName VARCHAR(32) NOT NULL,
		Unit VARCHAR(32) NOT NULL)
	CREATE INDEX Index1 ON BenchmarkMeasurments (BenchmarkID, Date)




