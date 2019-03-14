USE master
IF NOT EXISTS(SELECT * FROM sys.databases WHERE NAME='BenchmarkViewer')
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
	CREATE INDEX Index1 ON Benchmarks (Date, BenchmarkID)
INSERT INTO Benchmarks VALUES ('BenchmarksGame.Fasta_1.RunBench','20190312 10:34:09.000',18272800,'Time','Nanoseconds')