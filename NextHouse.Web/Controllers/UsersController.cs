using AspNetCoreHero.ToastNotification.Abstractions;
using Microsoft.AspNetCore.Components.Forms;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Controllers;
using Microsoft.AspNetCore.Mvc.Rendering;
using NextHouse.Application.Contracts.Security;
using NextHouse.Application.UseCases.Account.Queries.GetRoleOptions;
using NextHouse.Application.UseCases.Account.Queries.UserHasPermission;
using NextHouse.Application.UseCases.Users.Commands.CreateUser;
using NextHouse.Application.UseCases.Users.Queries.GetUsersList;
using NextHouse.Application.Utilites.Mediator;
using NextHouse.Web.DTOs.Users;
using System.Security.Claims;

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
                await LoadRolesSelectListAsync();

                if (!ModelState.IsValid)
                {
                    return View(dto);
                }

                // Usuario NO autenticado -> SOLO puede crear Client
                if (!User.Identity!.IsAuthenticated)
                {
                    string? clientRoleId = ((IEnumerable<SelectListItem>)ViewBag.Roles)
                        .FirstOrDefault(x => x.Text == "Client")
                        ?.Value;

                    if (string.IsNullOrWhiteSpace(clientRoleId))
                    {
                        throw new Exception("No se encontró el rol Client.");
                    }

                    dto.RoleId = Guid.Parse(clientRoleId);
                }
                else
                {
                    // Si está autenticado pero NO es admin -> prohibido
                    if (await _mediator.Send(new UserHasPermissionQuery
                    {
                        UserId = User.FindFirstValue(ClaimTypes.NameIdentifier),
                        PermissionCode = PermissionCodesCatalog.CREATE_USERS
                    }))
                    {
                        // Admin SOLO puede crear Admin y Agent
                        bool isValidRole = ((IEnumerable<SelectListItem>)ViewBag.Roles)
                            .Any(x =>
                                x.Value == dto.RoleId.ToString() &&
                                (x.Text == "Admin" || x.Text == "Agent"));

                        if (!isValidRole)
                        {
                            ModelState.AddModelError(nameof(dto.RoleId),
                                "Rol inválido.");

                            return View(dto);
                        }
                    }

                    
                }

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

                // Si el usuario no está autenticado -> ir al login
                if (!User.Identity!.IsAuthenticated)
                {
                    return RedirectToAction("Login", "Account");
                }

                // Si es admin -> quedarse en gestión de usuarios
                return RedirectToAction("Home", "Index");
            }
            catch (Exception ex)
            {
                _notifyService.Error($"Error al crear el usuario: {ex.Message}");

                await LoadRolesSelectListAsync();

                ModelState.AddModelError(string.Empty, ex.Message);

                return View(dto);
            }
        }
        private async Task LoadRolesSelectListAsync()
        {
            IReadOnlyList<RoleOptionDTO> roles = await _mediator.Send(new GetRoleOptionsQuery());
            ViewBag.Roles = new SelectList(roles, nameof(RoleOptionDTO.Id), nameof(RoleOptionDTO.Name));
        }
    }
}