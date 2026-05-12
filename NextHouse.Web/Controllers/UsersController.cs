using AspNetCoreHero.ToastNotification.Abstractions;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Controllers;
using Microsoft.AspNetCore.Mvc.Rendering;
using NextHouse.Application.UseCases.Users.Commands.CreateUser;
using NextHouse.Application.UseCases.Users.Queries.GetUsersList;
using NextHouse.Application.Utilites.Mediator;
using NextHouse.Web.DTOs.Users;

namespace PrivateBlog.Web.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class UsersController : ControllerBase
    {
        private readonly IMediator _mediator;

        public UsersController(IMediator mediator)
        {
            _mediator = mediator;
        }



        [HttpPost]
        public async Task<IActionResult> Create(CreateUserDTO dto)
        {


                CreateUserCommand command = new CreateUserCommand
                {
                    FirstName = dto.FirstName,
                    LastName = dto.LastName,
                    Email = dto.Email,
                    Password = dto.Password,
                    PhoneNumber = dto.PhoneNumber,
                    RoleId = dto.RoleId,
                };

                await _mediator.Send(command);
            return Ok();
        }

    }
}