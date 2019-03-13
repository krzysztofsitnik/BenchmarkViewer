USE master
IF NOT EXISTS(select * from sys.databases where name='BenchmarkViewer')
	CREATE DATABASE BenchmarkViewer

USE BenchmarkViewer

IF NOT EXISTS (SELECT * FROM sysobjects WHERE name='Benchmarks')
	CREATE TABLE Benchmarks(
		BenchmarkID VARCHAR(512) NOT NULL,
		Date DATETIME NOT NULL,
		Value DECIMAL NOT NULL,
		MetricName VARCHAR(32) NOT NULL,
		Unit VARCHAR(32) NOT NULL,
		PRIMARY KEY (BenchmarkID))
	CREATE INDEX Index1 on Benchmarks (Date, BenchmarkID)
