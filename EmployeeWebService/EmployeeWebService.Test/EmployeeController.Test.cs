using Microsoft.EntityFrameworkCore;
using System;
using Xunit;
using EmployeeWebService.Model;
using EmployeeWebService.Controllers;
using Microsoft.AspNetCore.Mvc;
using System.Threading.Tasks;

namespace EmployeeServiceTest
{
    public class EmployeeControllerTest
    {
        [Fact]
        public async void GetEmployee_ValidateAllValues()
        {
            // Arrange 
            var options = new DbContextOptionsBuilder<EmployeeContext>()
            .UseInMemoryDatabase(databaseName: "EmployeeDataBase")
            .Options;

            //// Create mocked Context by seeding Data as per Schema///
            using (var context = new EmployeeContext(options))
            {
                context.Employee.Add(new Employee
                {
                    Name = "Alice",
                    ID = "40100001",
                    Address = "san francisco",
                    Role = "Technical Manager",
                    Department = "IoT",
                    SkillSet = "Angular",
                    DateOfBirth = "09-MAY-1984",
                    DateOfJoining = "09-MAY-2006",
                    IsActive = true
                });
                context.SaveChanges();
            }



            using (var context = new EmployeeContext(options))
            {
                // Act 
                EmployeeController controller = new EmployeeController(context);
                var result = await controller.GetEmployee("40100001");
                var actualResult = result.Value;

                // Assert
                Assert.Equal("Alice", ((Employee)actualResult).Name);
                Assert.Equal("san francisco", ((Employee)actualResult).Address);
                Assert.Equal("Technical Manager", ((Employee)actualResult).Role);
                Assert.Equal("IoT", ((Employee)actualResult).Department);
                Assert.Equal("Angular", ((Employee)actualResult).SkillSet);
                Assert.Equal("09-MAY-1984", ((Employee)actualResult).DateOfBirth);
                Assert.Equal("09-MAY-2006", ((Employee)actualResult).DateOfJoining);
            }
        }
        [Fact]
        public async Task GetEmployee_ReturnsBadRequestResult_WhenEmployeeIdNull()
        {
            // Arrange 
            var options = new DbContextOptionsBuilder<EmployeeContext>()
            .UseInMemoryDatabase(databaseName: "EmployeeDataBase")
            .Options;

            //// Create mocked Context by seeding Data as per Schema///
            using (var context = new EmployeeContext(options))
            {
                context.Employee.Add(new Employee
                {
                    Name = "Alice",
                    ID = "40100002",
                    Address = "san francisco",
                    Role = "Technical Manager",
                    Department = "IoT",
                    SkillSet = "Angular",
                    DateOfBirth = "09-MAY-1984",
                    DateOfJoining = "09-MAY-2006",
                    IsActive = true
                });
                context.SaveChanges();
            }



            using (var context = new EmployeeContext(options))
            {
                // Act 
                EmployeeController controller = new EmployeeController(context);
                var result = await controller.GetEmployee(null);
               
                // Assert
                var badRequestResult = Assert.IsType<NotFoundResult>(result.Result);
            }
        }

        [Fact]
        public async void PostEmployee_ValidateAllValues()
        {
            // Arrange 
            var options = new DbContextOptionsBuilder<EmployeeContext>()
            .UseInMemoryDatabase(databaseName: "EmployeeDataBase")
            .Options;

            //// Create mocked Context by seeding Data as per Schema///
            var context = new EmployeeContext(options);
            Employee newEmployee = new Employee
            {
                Name = "Alice",
                ID = "50100001",
                Address = "san francisco",
                Role = "Technical Manager",
                Department = "IoT",
                SkillSet = "Angular",
                DateOfBirth = "09-MAY-1984",
                DateOfJoining = "09-MAY-2006",
                IsActive = true
            };
            // Act 
            EmployeeController controller = new EmployeeController(context);
            var result = await controller.PostEmployee(newEmployee);
            Assert.Equal(201, ((Microsoft.AspNetCore.Mvc.ObjectResult)result.Result).StatusCode);
        }

        [Fact]
        public async void PostEmployee_IdNull()
        {
            // Arrange 
            var options = new DbContextOptionsBuilder<EmployeeContext>()
            .UseInMemoryDatabase(databaseName: "EmployeeDataBase")
            .Options;

            //// Create mocked Context by seeding Data as per Schema///
            var context = new EmployeeContext(options);
            Employee newEmployee = new Employee
            {
                Name = "Alice",                
                Address = "san francisco",
                Role = "Technical Manager",
                Department = "IoT",
                SkillSet = "Angular",
                DateOfBirth = "09-MAY-1984",
                DateOfJoining = "09-MAY-2006",
                IsActive = true
            };
            // Act 
            EmployeeController controller = new EmployeeController(context);
            var result = await controller.PostEmployee(newEmployee);
            Assert.IsType<BadRequestObjectResult>(result.Result);
        }
        [Fact]
        public async void PostEmployee_NameNull()
        {
            // Arrange 
            var options = new DbContextOptionsBuilder<EmployeeContext>()
            .UseInMemoryDatabase(databaseName: "EmployeeDataBase")
            .Options;

            //// Create mocked Context by seeding Data as per Schema///
            var context = new EmployeeContext(options);
            Employee newEmployee = new Employee
            {
                ID = "50100002",
                Address = "san francisco",
                Role = "Technical Manager",
                Department = "IoT",
                SkillSet = "Angular",
                DateOfBirth = "09-MAY-1984",
                DateOfJoining = "09-MAY-2006",
                IsActive = true
            };
            // Act 
            EmployeeController controller = new EmployeeController(context);
            var result = await controller.PostEmployee(newEmployee);
            Assert.IsType<BadRequestObjectResult>(result.Result);
        }
    }
}
