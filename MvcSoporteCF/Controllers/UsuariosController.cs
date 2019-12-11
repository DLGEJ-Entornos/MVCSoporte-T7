using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using Microsoft.AspNet.Identity;
using Microsoft.AspNet.Identity.EntityFramework;
using MvcSoporteCF.Models;

namespace MvcSoporteCF.Controllers
{
    [Authorize(Roles = "Administrador")]
    public class UsuariosController : Controller
    {
        ApplicationDbContext dbUsu = new ApplicationDbContext();
        // GET: Usuarios
        public ActionResult Index()
        {
            // Muestra usuarios con su Rol. Utilza la clase UsuariosConRol definida en Models
            var resultados = new List<UsuarioConRol>();
            resultados = (
            from au in dbUsu.Users
            from aur in dbUsu.UserRoles.Where(x => x.UserId == au.Id)
            from ar in dbUsu.Roles.Where(x => x.Id == aur.RoleId)
            select new UsuarioConRol
            {
                Email = au.Email,
                NombreUsuario = au.UserName,
                RolDelUsuario = ar.Name
            }
            ).ToList();
            // Ordena los resultados por Rol y después por Email
            resultados = resultados.OrderBy(x => x.RolDelUsuario).ThenBy(x =>
           x.Email).ToList();
            return View(resultados);
        }


        // GET: CrearAdministrador
        public ActionResult CrearAdministrador()
        {

            return View();
        }
        //
        // POST: /Usuarios/CrearAdministrador
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult CrearAdministrador(RegisterViewModel model)
        {
            if (ModelState.IsValid)
            {
                var userManager =
                new UserManager<ApplicationUser>(new UserStore<ApplicationUser>(dbUsu));

                // Se crea el usuario
                var user = new ApplicationUser();
                user.UserName = model.Email; ;
                user.Email = model.Email;
                string userPWD = model.Password;
                var result = userManager.Create(user, userPWD);
                // Agregar el usuario al rol de Administrador
                if (result.Succeeded)
                {
                    var result1 = userManager.AddToRole(user.Id, "Administrador");
                    return RedirectToAction("Index");
                }
                // Gestión de errores:
                // Se agrega el mensaje del error producido al ValidationSummary de la Vista
                foreach (var error in result.Errors)
                {
                    ModelState.AddModelError("", error);
                }
            }
            // Se muestra la vista nuevamente, porque se ha producido un error
            return View(model);
        }
    }
}