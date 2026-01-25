using Microsoft.VisualStudio.TestTools.UnitTesting;
using Prakrishta.Infrastructure.Extensions;
using Prakrishta.Infrastructure.Helper;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Data;
using System.Linq;

namespace Prakrishta.Infrastructure.Test
{
    [TestClass]
    public class ExtensionTests
    {
        [TestMethod]
        public void CollectionContainsAnyTest1()
        {
            //Arrange
            var stringCollection = new Collection<string>
            {
                "Arul",
                "Prakrishta",
                "Sengottaiyan",
                "Microsoft",
                "Oracle",
                "Extensions"
            };

            //Act
            var found = stringCollection.ContainsAny<string>(null, "Prakrishta", "Oracle");

            //Assert
            Assert.IsTrue(found, "No items present");

        }

        [TestMethod]
        public void CollectionContainsAnyTest2()
        {
            //Arrange
            var userCollection = new Collection<User>
            {
                new User { FirstName = "Arul", LastName = "Sengottaiyan" },
                new User {FirstName = "Prakrishta", LastName = "Technologies" },
                new User {FirstName ="Microsoft", LastName ="Technologies" },
                new User {FirstName = "Oracle", LastName = "Systems" },
                new User {FirstName = "Extensions", LastName = "Methods" }
            };

            //Act
            var found = userCollection.ContainsAny<User>(new DelegateComparer<User>(
                (source, target) => source.FirstName == target.FirstName && source.LastName == target.LastName,
                x => x.FirstName.GetHashCode()),
                new User { FirstName = "Microsoft", LastName = "Technologies" },
                new User { FirstName = "Oracle", LastName = "Systems" });

            //Assert
            Assert.IsTrue(found, "No items present");
        }

        [TestMethod]
        public void CollectionContainsAnyTest3()
        {
            //Arrange
            var userCollection = new Collection<User>
            {
                new User { FirstName = "Arul", LastName = "Sengottaiyan" },
                new User {FirstName = "Prakrishta", LastName = "Technologies" },
                new User {FirstName ="Microsoft", LastName ="Technologies" },
                new User {FirstName = "Oracle", LastName = "Systems" },
                new User {FirstName = "Extensions", LastName = "Methods" }
            };

            //Act
            var found = userCollection.ContainsAny<User>(new AutoComparer<User, object>(
                x => new { x.FirstName, x.LastName }),
                new User { FirstName = "Microsoft", LastName = "Technologies" },
                new User { FirstName = "Oracle", LastName = "Systems" });

            //Assert
            Assert.IsTrue(found, "No items present");
        }

        [TestMethod]
        public void CollectionDistinctTest()
        {
            //Arrange
            var userCollection = new Collection<User>
            {
                new User { FirstName = "Arul", LastName = "Sengottaiyan" },
                new User { FirstName = "Prakrishta", LastName = "Technologies" },
                new User { FirstName ="Microsoft", LastName ="Technologies" },
                new User { FirstName = "Prakrishta", LastName = "Technologies" },
                new User { FirstName = "Microsoft", LastName = "Technologies" },
                new User { FirstName = "Arul", LastName = "Technologies" },
                new User { FirstName = "Prakrishta", LastName = "Technologies" },
                new User { FirstName ="Microsoft", LastName ="Arul" },
                new User { FirstName = "Prakrishta", LastName = "Technologies" },
                new User { FirstName = "Microsoft", LastName = "Technologies" },
                new User { FirstName = "Arul", LastName = "Sengottaiyan" },
                new User { FirstName = "Prakrishta", LastName = "Technologies" },
                new User { FirstName ="Microsoft", LastName ="Technologies" },
                new User { FirstName = "Prakrishta", LastName = "Microsoft" },
                new User { FirstName = "Microsoft", LastName = "Technologies" },
                new User { FirstName = "Arul", LastName = "Sengottaiyan" },
                new User { FirstName = "Prakrishta", LastName = "Microsoft" },
                new User { FirstName ="Microsoft", LastName ="Technologies" },
                new User { FirstName = "Prakrishta", LastName = "Technologies" },
                new User { FirstName = "Microsoft", LastName = "Technologies" },
                new User { FirstName = "Arul", LastName = "Sengottaiyan" },
                new User { FirstName = "Prakrishta", LastName = "Technologies" },
                new User { FirstName ="Microsoft", LastName ="Technologies" },
                new User { FirstName = "Prakrishta", LastName = "Technologies" },
                new User { FirstName = "Microsoft", LastName = "Arul" }
            };

            //Act
            var distinctRecords = userCollection.DistinctBy<User>(x => new { x.FirstName, x.LastName });

            //Assert
            Assert.AreEqual(6, distinctRecords.Count(), "No items present");
        }

        [TestMethod]
        public void CollectionDistinctWithReflectionTest()
        {
            //Arrange
            var userCollection = new Collection<User>
            {
                new User { FirstName = "Arul", LastName = "Sengottaiyan" },
                new User { FirstName = "Prakrishta", LastName = "Technologies" },
                new User { FirstName ="Microsoft", LastName ="Technologies" },
                new User { FirstName = "Prakrishta", LastName = "Technologies" },
                new User { FirstName = "Microsoft", LastName = "Technologies" },
                new User { FirstName = "Arul", LastName = "Sengottaiyan" },
                new User { FirstName = "Prakrishta", LastName = "Technologies" },
                new User { FirstName ="Microsoft", LastName ="Technologies" },
                new User { FirstName = "Prakrishta", LastName = "Technologies" },
                new User { FirstName = "Microsoft", LastName = "Technologies" },
                new User { FirstName = "Arul", LastName = "Sengottaiyan" },
                new User { FirstName = "Prakrishta", LastName = "Technologies" },
                new User { FirstName ="Microsoft", LastName ="Technologies" },
                new User { FirstName = "Prakrishta", LastName = "Technologies" },
                new User { FirstName = "Microsoft", LastName = "Technologies" },
                new User { FirstName = "Arul", LastName = "Sengottaiyan" },
                new User { FirstName = "Prakrishta", LastName = "Technologies" },
                new User { FirstName ="Microsoft", LastName ="Technologies" },
                new User { FirstName = "Prakrishta", LastName = "Technologies" },
                new User { FirstName = "Microsoft", LastName = "Technologies" }
            };

            //Act
            var distinctRecords = userCollection.DistinctBy<User>("FirstName");

            //Assert
            Assert.AreEqual(3, distinctRecords.Count(), "No items present");
        }

        [TestMethod]
        public void CollectionContainsAllTest1()
        {
            //Arrange
            var userCollection = new Collection<User>
            {
                new User { FirstName = "Arul", LastName = "Sengottaiyan" },
                new User {FirstName = "Prakrishta", LastName = "Technologies" },
                new User {FirstName ="Microsoft", LastName ="Technologies" },
                new User {FirstName = "Oracle", LastName = "Systems" },
                new User {FirstName = "Extensions", LastName = "Methods" }
            };

            //Act
            var found = userCollection.ContainsAll<User>(new PropertyComparer<User>("FirstName"),
                new User { FirstName = "Microsoft", LastName = "Technologies" },
                new User { FirstName = "Oracle", LastName = "Systems" });

            //Assert
            Assert.IsTrue(found, "No items present");
        }

        [TestMethod]
        public void CollectionRemoveTest()
        {
            //Arrange
            var userCollection = new Collection<User>
            {
                new User { FirstName = "Arul", LastName = "Sengottaiyan" },
                new User {FirstName = "Prakrishta", LastName = "Technologies" },
                new User {FirstName ="Microsoft", LastName ="Technologies" },
                new User {FirstName = "Oracle", LastName = "Systems" },
                new User {FirstName = "Extensions", LastName = "Methods" },
                new User {FirstName = "Extensions", LastName = "Methods" },
                new User {FirstName = "Extensions", LastName = "Methods" }
            };

            //Act
            userCollection.Remove(x => x.FirstName == "Extensions");

            //Assert
            Assert.HasCount(4, userCollection, "No items present");
        }

        [TestMethod]
        [DataRow("53210-5", false)]
        [DataRow("53210-56", false)]
        [DataRow("53210-563", false)]
        [DataRow("53210-5564", true)]
        [DataRow("53210-54321", false)]
        [DataRow("53210-", false)]
        [DataRow("53210", true)]
        [DataRow("5321", false)]
        public void ValidateUSZip(string zipCode, bool expectedResult)
        {
            //Act
            var result = zipCode.IsValidUSAZip();

            //Assert
            Assert.AreEqual<bool>(expectedResult, result, "Not a valid zip code");
        }

        [TestMethod]
        public void JoinStringTest()
        {
            //Arrange
            var list = new Collection<string>
            {
                "Test",
                "Void",
                "Zip",
                "Class"
            };

            var delimit = "Test,Void,Zip,Class";

            //Act
            var delimit1 = list.Join<string>(itemOutput: null);

            //Assert
            Assert.AreEqual<string>(delimit, delimit1);
        }

        [TestMethod]
        public void DataTableToEntityTest()
        {
            //Arrange
            DataTable dataTable = new DataTable("Users");
            dataTable.Columns.Add(new DataColumn("FirstName"));
            dataTable.Columns.Add(new DataColumn("LastName"));

            var row = dataTable.NewRow();
            row["FirstName"] = "Arul";
            row["LastName"] = "Sengottaiyan";
            dataTable.Rows.Add(row);

            row = dataTable.NewRow();
            row["FirstName"] = "Prakrishta";
            row["LastName"] = "Technologies";
            dataTable.Rows.Add(row);

            row = dataTable.NewRow();
            row["FirstName"] = "Microsoft";
            row["LastName"] = "Technologies";
            dataTable.Rows.Add(row);

            //Act
            var users = dataTable.ToEntities<User>();

            //Assert
            Assert.AreEqual(3, users.Count(), "No data conversion");
        }

        [TestMethod]
        [DataRow("53210", typeof(int))]
        [DataRow("555.30", typeof(double))]
        [DataRow("53210563", typeof(Int64))]
        public void StringToNullableTypeConversionTest(string value, Type expectedType)
        {
            //Act
            dynamic result = null;

            if (expectedType == typeof(int))
                result = value.GetValueOrNull<int>();
            else if (expectedType == typeof(double))
                result = value.GetValueOrNull<double>();
            else if (expectedType == typeof(Int64))
                result = value.GetValueOrNull<Int64>();

            //Assert
            Assert.AreEqual<Type>(expectedType, result?.GetType());
        }

        [TestMethod]
        public void GetValueFromDataRowTest()
        {
            //Arrange
            DataTable dataTable = new DataTable("Users");
            dataTable.Columns.Add(new DataColumn("FirstName"));
            dataTable.Columns.Add(new DataColumn("LastName"));
            dataTable.Columns.Add(new DataColumn("Age"));

            var row = dataTable.NewRow();
            row["FirstName"] = "Arul";
            row["LastName"] = "Sengottaiyan";
            row["Age"] = 10;
            dataTable.Rows.Add(row);

            row = dataTable.NewRow();
            row["FirstName"] = "Prakrishta";
            row["LastName"] = "Technologies";
            row["Age"] = 1;
            dataTable.Rows.Add(row);

            row = dataTable.NewRow();
            row["FirstName"] = "Microsoft";
            row["LastName"] = "Technologies";
            row["Age"] = 30;
            dataTable.Rows.Add(row);

            //Act
            var result1 = dataTable.Rows[0].GetValue<int>("Age");
            var result2 = dataTable.Rows[1].GetValue<string>(0);
            var result3 = dataTable.Rows[2].GetValue<int>(2);

            //Assert
            Assert.AreEqual<int>(10, result1);
            Assert.AreEqual<string>("Prakrishta", result2);
            Assert.AreEqual<int>(30, result3);
        }

        [TestMethod]
        [DataRow("https://testsite.com/types")]
        [DataRow(null)]
        public void QueryParamBuilderTest(string baseUrl)
        {
            //Arrange
            var queryParam = new QueryParameterBuilder(baseUrl)
                                    .AddParameter("page", "1")
                                    .AddParameter("size", "25");

            //Act
            var url = queryParam.ToString();

            //Assert
            Assert.AreEqual($"{baseUrl}?page=1&size=25", url);
        }

        [TestMethod]
        public void GetDataDifference_ShouldReturnDifferences_WhenTablesAreDifferent()
        {
            // Arrange
            DataTable table1 = new();
            table1.Columns.Add("ID", typeof(int));
            table1.Columns.Add("Name", typeof(string));
            table1.Rows.Add(1, "John");
            table1.Rows.Add(2, "Jane");
            table1.Rows.Add(3, "Doe");
            DataTable table2 = new DataTable();
            table2.Columns.Add("ID", typeof(int));
            table2.Columns.Add("Name", typeof(string));
            table2.Rows.Add(2, "Jane"); 
            table2.Rows.Add(3, "Doe");
            table2.Rows.Add(4, "Smith");

            // Act
            DataTable result = table1.GetDataDifference(table2);

            // Assert
            Assert.HasCount(2, result.Rows);
            Assert.AreEqual(1, result.Rows[0]["ID"]);
            Assert.AreEqual("John", result.Rows[0]["Name"]);
            Assert.AreEqual(4, result.Rows[1]["ID"]);
            Assert.AreEqual("Smith", result.Rows[1]["Name"]);
        }
        [TestMethod]
        public void GetDataDifference_ShouldReturnEmpty_WhenTablesAreIdentical()
        {
            // Arrange
            DataTable table1 = new DataTable();
            table1.Columns.Add("ID", typeof(int));
            table1.Columns.Add("Name", typeof(string));
            table1.Rows.Add(1, "John"); table1.Rows.Add(2, "Jane");
            DataTable table2 = table1.Copy();
            // Act
            DataTable result = table1.GetDataDifference(table2);

            // Assert
            Assert.IsEmpty(result.Rows);
        }
        [TestMethod]
        public void GetDataDifference_ShouldThrowException_WhenSchemasDoNotMatch()
        {
            // Arrange
            DataTable table1 = new DataTable();
            table1.Columns.Add("ID", typeof(int));
            table1.Columns.Add("Name", typeof(string));
            DataTable table2 = new DataTable();
            table2.Columns.Add("ID", typeof(int));
            table2.Columns.Add("FullName", typeof(string));

            // Different column name 
            // Act & Assert
            Assert.ThrowsExactly<Exception>(() => table1.GetDataDifference(table2),
                "The schema of two tables is not matching");
        }

        [TestMethod]
        public void CalculateAge_ShouldReturnCorrectAge_WhenGivenValidBirthDate()
        {
            // Arrange
            DateTime birthDate = new DateTime(1984, 07, 20);
            
            // Act
            int age = birthDate.Age();
            // Assert
            Assert.AreEqual(41, age);
        }

    }

    public class User
    {
        public string FirstName { get; set; }
        public string LastName { get; set; }
    }
}
