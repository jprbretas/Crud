using padvMVC.Negocio;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using padvMVC.Models;

namespace padvMVC.Controllers
{
    public class Atualizar_UsuariosController : GeralController
    {
        // GET: AtualizarUsuarios
        [RedirectingAction]
        [RefreshAttribute]
        public ActionResult Index()
        {

            Exist_UsersDto usuario = getUser();
            GetDropDownRegioes();
            paises();
            tratamentos();
            ViewBag.usuario = usuario;

            return View();
        }
        public ActionResult LoadDropDownPaises()
        {

            try
            {

                paises();

                return Json(new { status = "success" });
            }
            catch (Exception ex)
            {
                return Json(new { status = "error" });
            }
        }
        public ActionResult LoadGridUsuarios(string texto, string coluna)
        {

            try
            {
                List<Exist_UsersDto> usuarios = new List<Exist_UsersDto>();
                if (texto != null)
                {

                    if (texto == "")
                    {

                        exist_users exist_users = new exist_users();
                        usuarios = exist_users.ConsultarUsuarios();
                        usuarios = usuarios.Where(f => f.user_admin == false).ToList();
                        TempData["collection_users"] = usuarios;
                    }
                    else
                    {

                        usuarios = (List<Exist_UsersDto>)TempData["collection_users"];
                        TempData.Keep("collection_users");
                        usuarios = Buscar(usuarios, texto, coluna);
                        ViewBag.defaultSort = coluna;
                    }
                }
                else
                {

                    usuarios = (List<Exist_UsersDto>)TempData["collection_users"];
                    TempData.Keep("collection_users");
                }

                var SchemeList = (from scheme in usuarios select scheme).ToList();

                return PartialView("GridUsuarios", SchemeList);
            }
            catch (Exception ex)
            {

                return Content("<label> Erro. Contacte o administrador </label>");
            }
        }
        public ActionResult Incluir(Exist_UsersDto model)
        {

            try
            {

                RemoveReferences(ModelState, "country_id");

                //if (!ModelState.IsValid)
                //{
                //    return Json(new { status = "validation", view = "" });
                //}

                exist_users exist_users = new exist_users();
                string retorno = exist_users.Incluir(model);


                return Json(new { status = "success" });
            }
            catch (Exception ex)
            {

                return Json(new { status = "error" });
            }
        }
        public ActionResult Editar(Exist_UsersDto model)
        {

            try
            {

                RemoveReferences(ModelState, "country_id");
                exist_users exist_users = new exist_users();
                string retorno = exist_users.Editar(model);

                return Json(new { status = "success" });
            }
            catch (Exception ex)
            {

                return Json(new { status = "error" });
            }
        }
        public ActionResult Excluir(Exist_UsersDto model)
        {

            try
            {

                exist_users user = new exist_users();
                user.Excluir(model.user_id);

                return Json(new { status = "success" });
            }
            catch (Exception ex)
            {

                return Json(new { status = "error" });
            }
        }
    }
}