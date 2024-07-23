﻿using LibraryManager.Core.Interfaces;
using LibraryManager.Core.DTOs.User.InputModels;
using LibraryManager.Core.DTOs.User.ViewModels;
using LibraryManager.Core.Responses;
using Microsoft.AspNetCore.Mvc;
using Data.Repositories;

namespace LibraryManager.Api.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class UserController : ControllerBase
    {
        private readonly IUserRepository _userRepository;

        public UserController(IUserRepository userRepository)
        {
            _userRepository = userRepository;
        }

        [HttpPost("/user")]
        public async Task<ActionResult<APIResponse<CreateUserDTO>>> Register(CreateUserDTO modelDTO)
            => await _userRepository.RegisterUser(modelDTO);


        [HttpGet("/user")]
        public async Task<ActionResult<APIResponse<ViewUserDTO>>> GetAll()
            => await _userRepository.GetAllUsers();
            

        [HttpGet("/user/{id}")]
        public async Task<ActionResult<APIResponse<ViewUserDTO>>> GetById(long id)
            => await _userRepository.GetUserById(id);


        [HttpGet("/validateCredentials")]
        public async Task<ActionResult<APIResponse<ViewValidateCredentialsUserDTO>>> ValidateCredentials([FromQuery]ValidateCredentialsUserDTO DTOresquest)
            => await _userRepository.ValidateUserCredentials(DTOresquest);


        [HttpPut("/user/{id}")]
        public async Task<ActionResult<APIResponse<UpdateInputUserDTO>>> Update(long id, UpdateInputUserDTO DTO)
        {
            return await _userRepository.UpdateUser(id, DTO);
        }

        [HttpDelete("/user/{id}")]
        public async Task<ActionResult<APIResponse<ViewUserDTO>>> Delete(long id)
            => await _userRepository.DeleteUser(id);

    }
}
