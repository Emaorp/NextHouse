using AspNetCoreHero.ToastNotification.Abstractions;
using Microsoft.AspNetCore.Mvc;
using NextHouse.Application.Contracts.Security;
using NextHouse.Application.UseCases.Roles.Commands.UpdateRolePermissions;
using NextHouse.Application.UseCases.Roles.Queries;
using NextHouse.Application.UseCases.Security.Roles.Commands.UpdateRolePermissions;
using NextHouse.Application.Utilites.Mediator;
using NextHouse.Web.Security;

namespace NextHouse.Web.Controllers
{
    public class RolesController : Controller
    {
        private readonly IMediator _mediator;
        private readonly INotyfService _notifyService;

        public RolesController(
            IMediator mediator,
            INotyfService notifyService)
        {
            _mediator = mediator;
            _notifyService = notifyService;
        }

        [HttpGet]
        [RequirePermission(PermissionCodesCatalog.ADMIN_ROLES)]
        public async Task<IActionResult> Index()
        {
            List<GetRolesResponseDTO> roles =
                await _mediator.Send(
                    new GetRolesQuery());

            return View(roles);
        }

        [HttpPost]
        [RequirePermission(PermissionCodesCatalog.ADMIN_ROLES)]
        public async Task<IActionResult> UpdatePermissions(
            [FromBody] UpdateRolePermissionsDTO dto)
        {
            try
            {
                await _mediator.Send(
                    new UpdateRolePermissionsCommand
                    {
                        RoleId = dto.RoleId,
                        PermissionIds = dto.PermissionIds
                    });

                return Ok(new
                {
                    success = true,
                    message = "Permisos actualizados correctamente."
                });
            }
            catch (Exception ex)
            {
                return BadRequest(new
                {
                    success = false,
                    message = ex.Message
                });
            }
        }
    }
}