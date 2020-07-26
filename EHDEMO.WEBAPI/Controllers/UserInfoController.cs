using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using EHDEMO.Domain.Dtos;
using EHDEMO.Domain.Entities;
using EHDEMO.Domain.Interfaces;
using EHDEMO.Service.Validators;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;

namespace EHDEMO.WEBAPI.Controllers
{
    [ApiController, Route("api/[controller]")]
    //JWT Token Authentication
    //[Authorize(AuthenticationSchemes = JwtBearerDefaults.AuthenticationScheme)]
    public class UserInfoController : ControllerBase
    {

        private readonly IUserInfoService userInfoService;
        private readonly ILogger<UserInfo> logger;

        public UserInfoController(IUserInfoService userInfoService, ILogger<UserInfo> logger)
        {
            this.userInfoService = userInfoService;
            this.logger = logger;
        }

        [HttpGet]
        public ActionResult Get()
        {
            try
            {
                var userContactInfo = userInfoService.GetAllUsers();
                return Ok(userContactInfo);
            }
            catch (Exception ex)
            {
                logger.LogError("Something went wrong, message description:" + ex.Message);
                return BadRequest(ex);
            }
        }

        /// <summary>
        /// 
        /// </summary>
        /// <returns></returns>
        [HttpGet, Route("GetFullNameOfUser")]
        public ActionResult GetFullNameOfUser()
        {
            try
            {
                List<UserContactInfoDTO> model = new List<UserContactInfoDTO>();
                model = userInfoService.GetUserContactInfo().ToList();
                return Ok(model);
            }
            catch (Exception ex)
            {

                logger.LogError("Something went wrong, message description:" + ex.Message);
                return BadRequest(ex);
            }

        }


        /// <summary>
        /// Get User Contact by ID
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
        [HttpGet, Route("GetContactById")]
        public IActionResult GetContactById(int id)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            var contact = userInfoService.GetUserContact(id);

            if (contact == null)
            {
                return NotFound();
            }

            return Ok(contact);
        }

        /// <summary>To update contact details</summary>        
        /// <param name="id"></param>
        /// <param name="contactInfo"></param>
        /// <returns></returns>
        /// <response code="201">contact modified</response>
        /// <response code="400">Bad Request</response>
        /// <response code="500">Internal server error</response>
        [HttpPut, Route("UpdateUser")]
        public IActionResult UpdateContact(UserInfo userInfo)
        {
            if (userInfo.Id == 0)
            {
                BadRequest();
            }
            try
            {
                userInfoService.Put<UserInfoValidator>(userInfo);
                return Ok(userInfo);
            }
            catch (ArgumentNullException)
            {
                return NotFound("User contact details does not exist");
            }
            catch (Exception ex)
            {
                logger.LogError("Something went wrong, message description:" + ex.Message);
                return BadRequest(ex);
            }
        }

        /// <summary>To Create New Contact</summary>    
        /// <param name="userInfo"></param>
        /// <response code="201">Created new contact</response>
        /// <response code="400">Bad Request</response>
        /// <response code="500">Server Side Error</response>
        [HttpPost, Route("AddContact")]
        public IActionResult AddContact(UserInfo userInfo)
        {
            try
            {
                var objContact = userInfoService.Post<UserInfoValidator>(userInfo);
                logger.LogInformation("Record added " + userInfo.Id.ToString());
                return CreatedAtAction("Get", new { id = objContact.Id }, objContact);
            }
            catch (ArgumentNullException)
            {
                return NotFound("user contact does not exist");
            }
            catch (FluentValidation.ValidationException ex)
            {
                return BadRequest(ex.Errors);
            }

        }

        /// <summary>To delete contact info</summary>      
        /// <param name="id"></param>
        /// <returns></returns>
        [HttpPost, Route("DeleteContact")]
        public IActionResult DeleteContact(int id)
        {
            try
            {
                userInfoService.DeleteUserInfo(id);
                return Ok("Record Deleted");
            }
            catch (ArgumentException)
            {
                return NotFound("contact does not exist");
            }
            catch (Exception ex)
            {
                logger.LogError("Something went wrong,  message description: " + ex.Message);
                return BadRequest(ex);
            }
        }

    
     


    }
}