﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using AI_NETCORE_API.Models.Objects;
using AI_NETCORE_API.Models.Request;
using Data.Infrastructure.AppsettingsConfiguration.Abstract;
using Data.Infrastructure.EmailAddressValidation.Abstract;
using Data.Infrastructure.Logging.Abstract;
using Data.Infrastructure.PasswordValidation.Abstract;
using Data.Providers.Common.Enum;
using Data.Providers.User.Abstract;
using Data.Providers.User.Request.Abstract;
using Data.Providers.User.Request.Concrete;
using Data.Providers.User.Response.Abstract;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace AI_NETCORE_API.Controllers
{
    [Route("api/")]
    [ApiController]
    public class UsersController : ControllerBase
    {
        private readonly ILogger _logger;
        private readonly IPasswordValidator _passwordValidator;
        private readonly IEmailValidator _emailValidator;
        private readonly IUserProvider _userProvider;

        public UsersController(ILogger logger, IPasswordValidator passwordValidator, IEmailValidator emailValidator, IUserProvider userProvider)
        {
            _logger = logger;
            _passwordValidator = passwordValidator;
            _emailValidator = emailValidator;
            _userProvider = userProvider;
        }

        [HttpGet("user/{id:int}")]
        public async Task<ActionResult<string>> Login(int id)
        {
            try
            {
                IGetUserByIdRequest getUserByIdRequest = new GetUserByIdRequest(id);
                IGetUserByIdResponse getUserByIdResponse = _userProvider.GetUserById(getUserByIdRequest);
                return PrepareResponseAfterGetUserById(getUserByIdResponse);
            }
            catch (Exception ex)
            {
                _logger.Log(ex);
                return StatusCode(500);
            }
        }

        
        [HttpPost("login")]
        public async Task<ActionResult<string>> Login([FromBody] LoginRequest loginRequest)
        {
            try
            {
                return StatusCode(200);
            }
            catch(Exception ex)
            {
                _logger.Log(ex);
                return StatusCode(500);
            }
        }

        [HttpGet("logout")]
        public async Task<ActionResult>Logout()
        {
            try
            {
                return StatusCode(200);
            }
            catch (Exception ex)
            {
                _logger.Log(ex);
                return StatusCode(500);
            }
        }

        [HttpPost("register")]
        public async Task<ActionResult<UserModel>> Register([FromBody] RegisterRequest registerRequest)
        {
            try
            {
                if (!registerRequest.IsValid(_passwordValidator,_emailValidator))
                {
                    return StatusCode(400);
                }

                //TODO Add new user in database and return their details

                return StatusCode(200, new UserModel
                {
                    Email = registerRequest.Email,
                    Name = registerRequest.Name
                });
            }
            catch(Exception ex)
            {
                _logger.Log(ex);
                return StatusCode(500);
            }
        }
        private ActionResult<string> PrepareResponseAfterGetUserById(IGetUserByIdResponse getUserByIdResponse)
        {
            switch (getUserByIdResponse.ProvideResult)
            {
                case ProvideEnumResult.Exception:
                    return StatusCode(500);
                case ProvideEnumResult.Success:
                    return Ok(new UserModel
                    {
                        Email = getUserByIdResponse.User.Email,
                        Name = getUserByIdResponse.User.Name
                    });
                case ProvideEnumResult.NotFound:
                    return StatusCode(404);
                default:
                    throw new ArgumentOutOfRangeException();
            }
        }
    }
}