using New_Open_Schooling.Models;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace New_Open_Schooling.Controllers
{
    public class CenterLoginController : Controller
    {
        Open_Schooling_Final_2023Entities1 db = new Open_Schooling_Final_2023Entities1();
        string x;
        public ActionResult CenterLogin()
        {
             return View();        
        }

        [HttpPost]
        public ActionResult CenterLogin(Center_Login_Information center_Login)
        {
            try
            {
                var IsValid = db.Center_Login_Information.Where(c => c.UDISE_No == center_Login.UDISE_No && c.Center_Password == center_Login.Center_Password).FirstOrDefault();
                if (IsValid != null)
                {
                    Session["Center_Name"] = IsValid.Center_Name;
                    Session["Center_Code"] = IsValid.Contact_Center_Code;
                    Session["Division"] = IsValid.Division;
                    Session["Taluka"] = IsValid.Taluka;                 
                    return RedirectToAction("CenterInformation", "CenterLogin");
                }
                else
                {
                    ModelState.AddModelError("", "Invalid Details.");
                    return View();
                }
            }
            catch (Exception ex)
            {
                throw ex;
            }

        }

        public ActionResult CenterInformation()
        {
            try
            {
                List<CenterViewModel> centerViewModel = new List<CenterViewModel>();
                var CenterCode = Session["Center_Code"];
                centerViewModel = (from A in db.Tbl_Registration
                                   join B in db.Tbl_payment on A.ApplicationId equals B.merchant_param1
                                   //join C in db.Tbl_Application_Form on A.ApplicationId equals C.Form_No
                                   where A.Center_Code.Trim() == CenterCode.ToString() && A.Payment_Status == "1"
                                   select new CenterViewModel
                                   {
                                       Payment_Status = A.Payment_Status,
                                       ApplicationId = A.ApplicationId,
                                       Name = A.First_Name,
                                       Mobile_No = A.Mobile_No,
                                       Ec_Status = A.Ec_Status,
                                       Seat_No=db.Tbl_Application_Form.Where(a => a.Form_No == A.ApplicationId).FirstOrDefault().Seat_No,
                                       EC_No = A.Enrollment_No,                                      
                                       Exam_Form_Disable = A.Exam_Form_Disable,

                                   }).ToList();
                //centerViewModel.AddRange(centerViewModel);
                return View(centerViewModel);
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        public ActionResult ForgotPassword()
        {
            try
            {
                return View();
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }
        public JsonResult getPassword(double udiseNo)
        {
            try
            {
                var password = db.Center_Login_Information.Where(x => x.UDISE_No == udiseNo).FirstOrDefault();
                if (password == null)
                {
                    string Error = "Invalid UDISE Number.";
                    return Json(Error, JsonRequestBehavior.AllowGet);
                }
                else
                {
                    return Json(password.Center_Password, JsonRequestBehavior.AllowGet);
                }
            }
            catch (Exception ex)
            {
                throw ex;
            }

        }


        public ActionResult EC_Form()


        {
            try
            {
                return View();
            }
            catch (Exception ex)
            {
                throw ex;

            }
        }
        [HttpPost]
        public ActionResult EC_Form(Tbl_Registration tbl_Registration)
        {
            try
            {
                var IsValid = db.Tbl_Registration.Where(c => c.ApplicationId == tbl_Registration.ApplicationId && c.Mobile_No == tbl_Registration.Mobile_No ).FirstOrDefault();
                if (IsValid != null)
                {
                    Session["FormNo"] = IsValid.ApplicationId;
                    Session["CenterCode"] = IsValid.Center_Code;
                    return RedirectToAction("EnrollmentCertificate");
                }
                else
                {
                    ModelState.AddModelError("", "Invalid Details.");
                    return View();
                }
            }
            catch (Exception ex)
            {
                throw ex;
            }

        }
        public ActionResult EnrollmentCertificate(string ApplicationId)
        {
            try
            {
                Tbl_Registration registration_Model = new Tbl_Registration();
                if (ApplicationId == null)
                {
                    var formNo = Session["FormNo"];
                    var CenterCode = Session["CenterCode"];
                    x = formNo.ToString();
                    ViewData["stand"] = x.Substring(8, 1);
                    registration_Model = db.Tbl_Registration.Where(os => os.ApplicationId == formNo.ToString()).FirstOrDefault();
                    var ecCertificate = new CenterViewModel
                    {
                        tbl_Registration = db.Tbl_Registration.Where(os => os.ApplicationId == formNo.ToString()).FirstOrDefault(),
                        //Tbl_Payment = db.Tbl_payment.Where(p => p.merchant_param1 == formNo.ToString()).FirstOrDefault(),
                        Center_Login = db.Center_Login_Information.Where(c => c.Contact_Center_Code == CenterCode.ToString()).FirstOrDefault()
                    };
                    string extension = Path.GetExtension(registration_Model.Photo.Trim());
                    if (extension == ".jpg" || extension == ".JPG" || extension == ".jpeg")
                    {
                        //ViewData["Photo"] = "/Uploads/Photo/" + registration_Model.ApplicationId.TrimEnd() + "/Profile" + ".jpeg";
                        Session["Photo"] =  registration_Model.Photo;
                    }
                    else
                    {
                        ViewData["Photo"] =  registration_Model.Photo;
                    }
                    return View(ecCertificate);
                }
                else
                {
                    registration_Model = db.Tbl_Registration.Where(x => x.ApplicationId == ApplicationId&& x.Ec_Status!=null).FirstOrDefault();
                    if (registration_Model != null)
                    {
                        var contactCeneterCode = registration_Model.Center_Code;
                        ViewData["stand"] = ApplicationId.Substring(8, 1);
                        var centerEc = new CenterViewModel
                        {
                            tbl_Registration = db.Tbl_Registration.Where(os => os.ApplicationId.Trim() == ApplicationId.Trim()).FirstOrDefault(),
                            //Tbl_Payment = db.Tbl_payment.Where(p => p.merchant_param1.Trim() == UserName.Trim()).FirstOrDefault(),
                            Center_Login = db.Center_Login_Information.Where(c => c.Contact_Center_Code == contactCeneterCode).FirstOrDefault()
                        };

                        string extension = Path.GetExtension(registration_Model.Photo);
                        if (extension == ".jpg" || extension == ".JPG" || extension == ".jpeg")
                        {
                            Session["Photo"] = registration_Model.Photo;
                        }
                        else
                        {
                            ViewData["Photo"] = "/AppFiles/Images/" + registration_Model.ApplicationId.TrimEnd() + "/Profile" + ".jpg";
                        }
                        return View(centerEc);
                    }
                    else
                    {
                        return RedirectToAction("CenterInformation");
                    }
                }
            }
            catch (Exception ex)
            {
               return RedirectToAction("CenterInformation");
            }
        }

        public ActionResult Download_Page()
        {
            try
            {
                List<CenterViewModel> centerViewModel = new List<CenterViewModel>();
                var CenterCode = Session["Center_Code"];
                centerViewModel = (from A in db.Tbl_Registration
                                       //join B in db.Tbl_payment on A.ApplicationId equals B.merchant_param1

                                   where A.Center_Code.Trim() == CenterCode.ToString() && A.Payment_Status == "1"
                                   select new CenterViewModel
                                   {
                                       Payment_Status = A.Payment_Status,
                                       ApplicationId = A.ApplicationId,
                                       Name = A.First_Name,
                                       Mobile_No = A.Mobile_No,
                                       //Ec_Status = A.Ec_Status,
                                       EC_No = A.Enrollment_No,
                                       Exam_Form_Disable = A.Exam_Form_Disable,

                                   }).ToList();
                //centerViewModel.AddRange(centerViewModel);
                return View(centerViewModel);
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }
    }
}