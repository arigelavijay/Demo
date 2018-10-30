using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

using Demo.Models;

namespace Demo.Controllers
{
    public class RegisterController : Controller
    {
        // GET: Register
        public ActionResult Index(RegisterVm registerVm = null)
        {
            if (registerVm != null)
            {
                return View(registerVm);
            }
            return View(new RegisterVm());
        }

        public ActionResult Save(register register)
        {
            List <register> data = null;
            if (Session["Ssn:Data"] != null)
            {
                data = (List<register>)Session["Ssn:Data"];
                if (register.id == 0)
                {
                    register.id = (data.OrderByDescending(x => x.id).FirstOrDefault().id) + 1;
                    data.Add(register);
                }
                else
                {
                    var obj = data.Where(x => x.id == register.id).FirstOrDefault();
                    obj.name = register.name;
                    obj.email = register.email;
                    obj.phone = register.phone;
                }
                
            }
            else
            {
                data = new List<register>();
                register.id = 1;
                data.Add(register);
            }

            
            Session["Ssn:Data"] = data;
            ModelState.Clear();
            return View("Index", new RegisterVm {
                data = data,
                register = null
            });
        }

        public ActionResult Edit(int SelectedIndex)
        {
            var data = (List<register>)Session["Ssn:Data"];
            var register = data.Where(x => x.id == SelectedIndex).FirstOrDefault();

            var registerVm = new RegisterVm
            {
                data = data,
                register = register
            };

            return View("Index", registerVm);
        }

        public ActionResult Delete(int SelectedIndex)
        {
            var data = (List<register>)Session["Ssn:Data"];
            var obj = data.Where(x => x.id == SelectedIndex).FirstOrDefault();
            data.Remove(obj);
            Session["Ssn:Data"] = data;            

            var registerVm = new RegisterVm
            {
                data = data,
                register = null
            };

            return View("Index", registerVm);
        }
    }    
}