using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

using Demo.Models;

namespace Demo.Controllers
{
    public class HomeController : Controller
    {
        public ActionResult Index()
        {
            return View(new RegisterVm());
        }

        [HttpPost]
        public ActionResult Save(RegisterVm registerVm)
        {
            if (registerVm.data != null)
            {                
                if (registerVm.register.id == 0)
                {
                    registerVm.register.id = (registerVm.data.OrderByDescending(x => x.id).FirstOrDefault().id) + 1;
                    registerVm.data.Add(registerVm.register);
                }
                else
                {
                    var obj = registerVm.data.Where(x => x.id == registerVm.register.id).FirstOrDefault();
                    obj.name = registerVm.register.name;
                    obj.email = registerVm.register.email;
                    obj.phone = registerVm.register.phone;
                }
            }
            else
            {
                registerVm.register.id = 1;
                registerVm.data = new List<register>();
                registerVm.data.Add(registerVm.register);
            }
            
            ModelState.Clear();
            return View("Index", new RegisterVm
            {
                data = registerVm.data,
                register = null
            });
        }

        [HttpPost]
        public ActionResult Edit(RegisterVm registerVm)
        {
            
            var register = registerVm.data.Where(x => x.id == registerVm.selectedIndex).FirstOrDefault();

            var _registerVm = new RegisterVm
            {
                data = registerVm.data,
                register = register
            };
            ModelState.Clear();
            return View("Index", _registerVm);
        }

        [HttpPost]
        public ActionResult Delete(RegisterVm registerVm)
        {
            var obj = registerVm.data.Where(x => x.id == registerVm.selectedIndex).FirstOrDefault();
            registerVm.data.Remove(obj);
            var _registerVm = new RegisterVm
            {
                data = registerVm.data,
                register = null
            };
            ModelState.Clear();
            return View("Index", _registerVm);
        }

        [HttpPost]
        public ActionResult New(RegisterVm registerVm)
        {
            ModelState.Clear();
            registerVm.register = null;
            registerVm.selectedIndex = 0;
            return View("Index", registerVm);
        }
    }
}