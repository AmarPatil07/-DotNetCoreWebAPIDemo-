using EHDEMO.Domain.Entities;
using EHDEMO.Domain.Interfaces;
using EHDEMO.WEBAPI.Controllers;
using Microsoft.Extensions.Logging;
using System;
using System.Collections.Generic;
using System.Text;
using Microsoft.Extensions.Logging.Abstractions;
using Xunit;
using Microsoft.AspNetCore.Mvc;

namespace EHDEMO.WEBAPI.TEST.TestController
{
  public class UserInfoControllerTest
    {

        UserInfoController _controller;
        IUserInfoService _service; 

        public UserInfoControllerTest()
        {
            ILoggerFactory doesntDoMuch = new Microsoft.Extensions.Logging.Abstractions.NullLoggerFactory();
            Logger<UserInfo> _logger = new Logger<UserInfo>(doesntDoMuch);
            _service = new UserInfoServiceMock();
           _controller = new UserInfoController(_service, _logger);
        }

        #region Get Contacts

        [Fact]
        public void Get_WhenCalled_ReturnsOkResult()
        {
            // Act
            var okResult = _controller.Get();

            // Assert
            Assert.IsType<OkObjectResult>(okResult);
        }

        [Fact]
        public void Get_WhenCalled_ReturnsAllItems()
        {
            // Act
            var okResult = _controller.Get() as OkObjectResult;

            // Assert
            var items = Assert.IsType<List<UserInfo>>(okResult.Value);
            Assert.Equal(4, items.Count);
        }

        #endregion

        #region GetContact By Id

        [Fact]
        public void GetContactById_UnknownGuidPassed_ReturnsNotFoundResult()
        {
            // Act
            var notFoundResult = _controller.GetContactById(5);

            // Assert
            Assert.IsType<NotFoundResult>(notFoundResult);
        }

        [Fact]
        public void GetContactById_ExistingGuidPassed_ReturnsOkResult()
        {
            // Arrange
            var testid = 1;

            // Act
            var okResult = _controller.GetContactById(testid);

            // Assert
            Assert.IsType<OkObjectResult>(okResult);
        }

        [Fact]
        public void GetContactById_ExistingGuidPassed_ReturnsRightItem()
        {
            // Arrange
            var testid = 1;

            // Act
            var okResult = _controller.GetContactById(testid) as OkObjectResult; 

            // Assert
            Assert.IsType<UserInfo>(okResult.Value);
            Assert.Equal(1, (okResult.Value as UserInfo).Id);
        }
        #endregion

        #region Add - Contact

        [Fact]
        public void AddContact_InvalidObjectPassed_ReturnsBadRequest()
        {
            // Arrange
            var nameItem = new UserInfo()
            {
                Email = "satish@gmail.com",
                FirstName = string.Empty,
                LastName = "Dhawan",          
                IsActive = true,
                PhoneNumber = "3435454544"
            };
            _controller.ModelState.AddModelError("FirstName", "Required");

            // Act
            var badResponse = _controller.AddContact(nameItem);

            // Assert
            Assert.IsType<CreatedAtActionResult>(badResponse);
        }


        [Fact]
        public void AddContact_ValidObjectPassed_ReturnsCreatedResponse()
        {
            // Arrange
            var nameItem = new UserInfo()
            {
                Email = "satish@gmail.com",
                FirstName = "Satish",
                LastName = "Dhawan",                
                IsActive = true,
                PhoneNumber = "3435454544",
                Id =5
                
            };

            // Act
            var createdResponse = _controller.AddContact(nameItem);

            // Assert
            Assert.IsType<CreatedAtActionResult>(createdResponse);
        }


        [Fact]
        public void AddContact_ValidObjectPassed_ReturnedResponseHasCreatedItem()
        {
            // Arrange
            var nameItem = new UserInfo()
            {
                Email = "satish@gmail.com",
                FirstName = "Satish",
                LastName = "Dhawan",
                Id = 5,
                IsActive = true,
                PhoneNumber = "3435454544"
            };


            // Act
            var createdResponse = _controller.AddContact(nameItem) as CreatedAtActionResult;
            var item = createdResponse.Value as UserInfo;

            // Assert
            Assert.IsType<UserInfo>(item);
            Assert.Equal(5, item.Id);
        }
        #endregion

        
        #region Delete - Contact
        [Fact]
        public void Remove_NotExistingIdPassed_ReturnsNotFoundResponse()
        {
            // Arrange
            var id = 10;

            // Act
            var badResponse = _controller.DeleteContact(id) as BadRequestObjectResult;

            // Assert
            Assert.Equal(400,badResponse.StatusCode);
        }

        [Fact]
        public void Remove_ExistingIdPassed_ReturnsOkResult()
        {
            // Arrange
            var id = 3;

            // Act
            var okResponse = _controller.DeleteContact(id) as OkObjectResult;

            // Assert
            Assert.Equal("Record Deleted", okResponse.Value);
        }
        [Fact]
        public void Remove_ExistingGuidPassed_RemovesOneItem()
        {
            // Arrange
            var id = 3;

            // Act
            var okResponse = _controller.DeleteContact(id);

            // Assert
            Assert.Equal(3, _service.GetAllUsers().Count);
        }
        #endregion

    }
}
