USE master
IF NOT EXISTS(SELECT * FROM sys.databases WHERE NAME='BenchmarkViewer')
	CREATE DATABASE BenchmarkViewer
USE BenchmarkViewer

IF NOT EXISTS (SELECT * FROM sysobjects WHERE name='Benchmarks')
	CREATE TABLE Benchmarks(
		Benchmark VARCHAR(512) NOT NULL UNIQUE,
		BenchmarkID INT NOT NULL PRIMARY KEY IDENTITY (1,1))			

IF NOT EXISTS (SELECT * FROM sysobjects WHERE name='BenchmarkMeasurments')
	CREATE TABLE BenchmarkMeasurments(
		BenchmarkID INT NOT NULL FOREIGN KEY REFERENCES Benchmarks(BenchmarkID),
		Date DATETIME NOT NULL,
		Value DECIMAL NOT NULL,
		MetricName VARCHAR(32) NOT NULL,
		Unit VARCHAR(32) NOT NULL)
	CREATE INDEX Index1 ON BenchmarkMeasurments (BenchmarkID, Date)




