﻿using System.Data.SqlClient;

namespace Dapper.Bulk.Tests
{
    public class SqlServerTestSuite
    {
        private static string ConnectionString = "Server=(localdb)\\mssqllocaldb;Database=DapperBulkTest;Trusted_Connection=True;MultipleActiveResultSets=true;";

        public SqlConnection GetConnection() => new SqlConnection(ConnectionString);

        static SqlServerTestSuite()
        {
            using (var connection = new SqlConnection(ConnectionString))
            {
                string DropTable(string name) => $"IF OBJECT_ID('{name}', 'U') IS NOT NULL DROP TABLE [{name}];";
                connection.Open();
                connection.Execute(
                    $@"{DropTable("IdentityAndComputedTests")}
                    CREATE TABLE IdentityAndComputedTests
                    (
	                    [IdKey] BIGINT IDENTITY(1,1) NOT NULL PRIMARY KEY,
	                    [Name] NVARCHAR(100) NULL,
	                    [CreateDate] DATETIME2 NOT NULL DEFAULT(GETDATE())
                    );");
                
                connection.Execute(
                    $@"{DropTable("NoIdentityTests")}
                    CREATE TABLE NoIdentityTests(
	                    [ItemId] BIGINT NULL,
	                    [Name] NVARCHAR(100) NULL
                    );");

                connection.Execute(
                    $@"{DropTable("EnumTests")}
                    CREATE TABLE EnumTests(
	                    [Id] BIGINT IDENTITY(1,1) NOT NULL PRIMARY KEY,
	                    [IntEnum] INT NOT NULL,
	                    [LongEnum] BIGINT NOT NULL
                    );");

                connection.Execute(
                  $@"{DropTable("IdentityAndNotMappedTests")}
                    CREATE TABLE IdentityAndNotMappedTests
                    (
	                    [IdKey] BIGINT IDENTITY(1,1) NOT NULL PRIMARY KEY,
	                    [Name] NVARCHAR(100) NULL
                    );");

                connection.Execute(
                  $@"{DropTable("CustomColumnNames")}
                    CREATE TABLE CustomColumnNames
                    (
	                    [IdKey] BIGINT IDENTITY(1,1) NOT NULL PRIMARY KEY,
	                    [Name_1] NVARCHAR(100) NULL,
                        [Int_Col] INT NOT NULL,
	                    [Long_Col] BIGINT NOT NULL
                    );");

                connection.Execute(
                    $@"{DropTable("10_Escapes")}
                    CREATE TABLE [10_Escapes](
	                    [Id] BIGINT IDENTITY(1,1) NOT NULL PRIMARY KEY,
	                    [10_Name] NVARCHAR(100) NULL
                    );");
            }
        }
    }
}
