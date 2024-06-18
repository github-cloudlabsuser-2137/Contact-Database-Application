using CRUD_application_2.Controllers;
using CRUD_application_2.Models;
using System.Collections.Generic;
using System.Web.Mvc;   
using Xunit;

namespace CRUD_application_2.UnitTests
{
    public class UserControllerTests
    {
        public class UserControllerTest
        {
            [Fact]
            public void Index_ReturnsViewWithUsers()
            {
                // Arrange
                var controller = new UserController();
                UserController.userlist.Clear();
                UserController.userlist.Add(new User { Id = 1, Name = "Test User 1", Email = "test1@example.com" });
                UserController.userlist.Add(new User { Id = 2, Name = "Test User 2", Email = "test2@example.com" });

                // Act
                var result = controller.Index() as ViewResult;
                var model = result.Model as List<User>;

                // Assert
                Xunit.Assert.NotNull(result);
                Xunit.Assert.Equal(2, model.Count);
            }

            [Fact]
            public void Details_ReturnsViewWithUser_WhenUserExists()
            {
                // Arrange
                var controller = new UserController();
                UserController.userlist.Clear();
                var testUser = new User { Id = 1, Name = "Test User", Email = "test@example.com" };
                UserController.userlist.Add(testUser);

                // Act
                var result = controller.Details(testUser.Id) as ViewResult;
                var model = result.Model as User;

                // Assert
                Xunit.Assert.NotNull(result);
                Xunit.Assert.Equal(testUser.Id, model.Id);
            }

            [Fact]
            public void Details_ReturnsHttpNotFound_WhenUserDoesNotExist()
            {
                // Arrange
                var controller = new UserController();
                UserController.userlist.Clear();

                // Act
                var result = controller.Details(999); // Assuming 999 is an ID that doesn't exist

                // Assert
                Xunit.Assert.IsType<HttpNotFoundResult>(result);
            }

            [Fact]
            public void Create_Get_ReturnsCreateView()
            {
                // Arrange
                var controller = new UserController();

                // Act
                var result = controller.Create() as ViewResult;

                // Assert
                Xunit.Assert.NotNull(result);
                Xunit.Assert.Equal("Create", result.ViewName);
            }

            [Fact]
            public void Create_Post_AddsUserAndRedirectsToIndex()
            {
                // Arrange
                var controller = new UserController();
                UserController.userlist.Clear(); // Ensure the list is initially empty
                var newUser = new User { Id = 1, Name = "Test User", Email = "test@example.com" };

                // Act
                var result = controller.Create(newUser) as RedirectToRouteResult;

                // Assert
                Xunit.Assert.NotNull(result);
                Xunit.Assert.Equal("Index", result.RouteValues["action"]);
                Xunit.Assert.Contains(UserController.userlist, u => u.Id == newUser.Id && u.Name == "Test User" && u.Email == "test@example.com");
            }

            [Fact]
            public void Delete_PostAction_RemovesUser_WhenUserExists()
            {
                // Arrange
                var controller = new UserController();
                // Ensure the list is in a known state
                UserController.userlist.Clear();
                var testUser = new User { Id = 1, Name = "Test User", Email = "test@example.com" };
                UserController.userlist.Add(testUser);

                // Act
                var result = controller.Delete(testUser.Id, new FormCollection()) as RedirectToRouteResult;

                // Assert
                Xunit.Assert.NotNull(result);
                Xunit.Assert.Equal("Index", result.RouteValues["action"]);
                Xunit.Assert.DoesNotContain(UserController.userlist, u => u.Id == testUser.Id);
            }

            [Fact]
            public void Delete_PostAction_ReturnsHttpNotFound_WhenUserDoesNotExist()
            {
                // Arrange
                var controller = new UserController();
                // Ensure the list is in a known state
                UserController.userlist.Clear();

                // Act
                var result = controller.Delete(999, new FormCollection()); // Assuming 999 is an ID that doesn't exist

                // Assert
                Xunit.Assert.IsType<HttpNotFoundResult>(result);
            }

        }
    }
}