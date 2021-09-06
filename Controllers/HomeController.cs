using MicroLabsFinal.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace MicroLabsFinal.Controllers
{
    public class HomeController : Controller
    {
        public ActionResult Index()
        {
            return View();
        }

        public ActionResult About()
        {
            ViewBag.Message = "Your application description page.";

            return View();
        }

        public ActionResult Contact()
        {
            ViewBag.Message = "Your contact page.";

            return View();
        }
        public ActionResult Acceso()
        {

            return View();
        }



        public ActionResult Authorize(MicroLabsFinal.Models.Usuario userModel)
        {
            using (db_a78ddc_mircrolabsEntities1 db = new db_a78ddc_mircrolabsEntities1())
            {
                var userDetails = db.Usuario.Where(x => x.nombreUsuario == userModel.nombreUsuario && x.password == userModel.password).FirstOrDefault();
                if (userDetails == null)
                {
                    ViewBag.Message = "Contraseña o usuario incorrecto";
                    return RedirectToAction("Acceso", "Home");
                }
                else
                {
                    Session["userID"] = userDetails.idUsuario;
                    Session["userName"] = userDetails.nombreUsuario;
                    return RedirectToAction("Index", "Home");
                }
            }


        }

        public ActionResult LogOut()
        {
            int userId = (int)Session["userID"];
            Session.Abandon();
            return RedirectToAction("Acceso", "Home");
        }

    }
}