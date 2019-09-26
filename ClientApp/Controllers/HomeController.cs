using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using ClientApp.Models;

namespace ClientApp.Controllers
{
    public class HomeController : Controller
    {
        public ActionResult Index()
        {
            return View();
        }


        [HttpPost]
        public ActionResult Index(LoginModel model)
        {
            TestDBEntities dbContext = new TestDBEntities();

            if (!ModelState.IsValid)

            {
                ModelState.AddModelError("", "Enter login Information please");

                return View();
            }
            if (!IsUserExist(model.UserName,model.Password))
            {
                // ModelState.AddModelError("","Not a Valid user");
                return View("Registration");
            }
            else
            {
                int id = dbContext.LoginInfoes.Where(x => x.UserName == model.UserName).Select(x => x.UserId).FirstOrDefault();
                    
                return RedirectToAction("UserDetails", "Home", new { id = id });
            }

          

            return View("Index");
        }
        public bool IsUserExist(string UserName, string password)

        {
            TestDBEntities db = new TestDBEntities();
            bool isExists = false;
            password = RegistrationModel.Encryptdata(password);
            isExists = db.LoginInfoes.Any(x => (x.UserName == UserName) && x.Password == password);

            return isExists;

        }
       
        public ActionResult UserDetails(int  id)
        {
            TestDBEntities db = new TestDBEntities();
            ClientModel cModel = db.ClientDetails.Where(c => c.UserId == id).Select(
                
                                                                                      cM =>  new ClientModel
                                                                                        {

                                                                                            ClientName = cM.ClientName,
                                                                                            Address = cM.Address,
                                                                                            Image =cM.Image,
                                                                                            PhoneNumber = cM.PhoneNumber

                                                                                        }).FirstOrDefault();

            return View(cModel);
        }
        
        [HttpGet]
        public ActionResult Registration()
        {
            return View("Registration");
        }

        [HttpPost]
        public ActionResult Registration(LoginInfo loginModel)
        {
           loginModel.Password =  RegistrationModel.Encryptdata(loginModel.Password);
            TestDBEntities db = new TestDBEntities();
          //  loginModel.LastLoginDate.Value = System.DateTime.Now();
            db.LoginInfoes.Add(loginModel);
            db.SaveChanges();
            return View("Index");
        }

        public ActionResult GetImg(string path)
        {
            var imagePath = $@"{path}.png";
            var bytes = System.IO.File.ReadAllBytes(imagePath);
            return File(bytes, "image/png");
        }
    }
}