using Microsoft.VisualStudio.TestTools.UnitTesting;
using Prakrishta.Infrastructure.Extensions;
using Prakrishta.Infrastructure.Helper;
using System.Collections.Generic;
using System.Collections.ObjectModel;

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
            Assert.AreEqual(4, userCollection.Count, "No items present");
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
    }

    public class User
    {
        public string FirstName { get; set; }
        public string LastName { get; set; }
    }
}
