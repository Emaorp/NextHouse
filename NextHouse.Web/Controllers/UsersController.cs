using AspNetCoreHero.ToastNotification.Abstractions;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Controllers;
using Microsoft.AspNetCore.Mvc.Rendering;
using NextHouse.Application.UseCases.Account.Queries.GetRoleOptions;
using NextHouse.Application.UseCases.Users.Commands.CreateUser;
using NextHouse.Application.UseCases.Users.Queries.GetUsersList;
using NextHouse.Application.Utilites.Mediator;
using NextHouse.Web.DTOs.Users;

namespace PrivateBlog.Web.Controllers
{

    [Route("api/[controller]")]
    public class UsersController : Controller
    {
        private readonly IMediator _mediator;
        private readonly INotyfService _notifyService;
        public UsersController(INotyfService notifyService, IMediator mediator)
        {
            _notifyService = notifyService;
            _mediator = mediator;
        }

        [HttpGet]
        public async Task<IActionResult> Create()
        {
            await LoadRolesSelectListAsync();
            return View();
        }

        [HttpPost]
        public async Task<IActionResult> Create(CreateUserDTO dto)
        {

            try
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
                _notifyService.Success("Usuario creado exitosamente.");
            }
            catch (Exception ex)
            {
                _notifyService.Error($"Error al crear el usuario: {ex.Message}");
                await LoadRolesSelectListAsync();
                ModelState.AddModelError(string.Empty, ex.Message);
                
                return View(dto);
            }

            return RedirectToAction("Login", "Account");
        }
        private async Task LoadRolesSelectListAsync()
        {
            IReadOnlyList<RoleOptionDTO> roles = await _mediator.Send(new GetRoleOptionsQuery());
            ViewBag.Roles = new SelectList(roles, nameof(RoleOptionDTO.Id), nameof(RoleOptionDTO.Name));
        }
    }
}