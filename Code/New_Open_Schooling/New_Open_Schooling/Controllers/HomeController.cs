using New_Open_Schooling.Helper;
using New_Open_Schooling.Models;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Configuration;
using System.Data.SqlClient;
using System.IO;
using System.Linq;
using System.Net;
using System.Web;
using System.Web.Mvc;
using System.Web.Security;


namespace New_Open_Schooling.Controllers
{

    public class HomeController : Controller
    {
        Open_Schooling_Final_2023Entities1 db = new Open_Schooling_Final_2023Entities1();
        Common common = new Common();
        SqlConnection _Con;
        SqlCommand _Command;

        public ActionResult HomeDashboard()
        {
            _Con = new SqlConnection(ConfigurationManager.ConnectionStrings["constr3"].ConnectionString);         

            var res = db.Tbl_Site_Status.ToList();
            var TodayDate = DateTime.Now;
            foreach (var item in res)
            {
                if (TodayDate > item.Start_Date && TodayDate < item.Late_Date)
                {
                    _Con.Open();
                    _Command = new SqlCommand("Update Tbl_Site_Status set Active_Status='1' where ID='" + item.ID + "'", _Con);
                    _Command.ExecuteNonQuery();
                    _Con.Close();
                }
                else
                {
                    _Con.Open();
                    _Command = new SqlCommand("Update Tbl_Site_Status set Active_Status='0' where ID='" + item.ID + "'", _Con);
                    _Command.ExecuteNonQuery();
                    _Con.Close();
                }

                if (item.Type == "Registration" && item.Active_Status == 1)
                {
                    Session["Registration"] = 1;
                }
               
                if (item.Type == "State" && item.Active_Status == 1)
                {
                    Session["State"] = 1;

                }
            }
            return View();
        }
       

        public ActionResult Student_Login()
        {

            return View();
        }
        [HttpPost]

        public ActionResult Student_Login(Tbl_Registration tbl_Registration, Tbl_payment tbl_Payment)
        {
            var aa = db.Tbl_Registration.Where(x=>x.Mobile_No == tbl_Registration.Mobile_No && x.Email==tbl_Registration.Email && x.Payment_Status=="1").FirstOrDefault();
            tbl_Payment = db.Tbl_payment.Where(x => x.billing_name == aa.First_Name && x.billing_email == aa.Email).FirstOrDefault();
           
            tbl_Payment.merchant_param1 = aa.ApplicationId;
            db.Tbl_payment.Attach(tbl_Payment);
            db.Entry(tbl_Payment).Property(x => x.merchant_param1).IsModified = true;
            db.SaveChanges();
            if (aa.Id != 0)
            {
                aa.Payment_Status = "1";
            }
            else
            {

                aa.Payment_Status = "0";

            }
            db.Tbl_Registration.Attach(aa);
             db.Entry(aa).Property(x => x.Payment_Status).IsModified = true;
            db.SaveChanges();
            var cc = db.Center_Login_Information.Where(x => x.Center_Name == aa.Center).FirstOrDefault();
            TempData["Amount"] = tbl_Payment.amount_money;
            TempData["order_status"] = tbl_Payment.order_status;
            TempData["Card_name"] = tbl_Payment.card_name;
            TempData["tracking_id"] = tbl_Payment.tracking_id;
            TempData["bank_ref_no"] = tbl_Payment.bank_ref_no;
            TempData["trans_date"] = tbl_Payment.trans_date;
            TempData["UDISE_No"] = cc.UDISE_No;
            TempData["Contact_Center_Code"] = cc.Contact_Center_Code;
            TempData["Subject_List"] = aa.Subject_List;
            if (aa != null)
            {
                return View("PrintForm", aa);
            }
            else
            {
                return View();
            }

        }

        public ActionResult Registration()
        {
            
                    bindDistrict();
                    return View();
               
            //var res = db.Tbl_Site_Status.ToList();
            //var TodayDate = DateTime.Now;
            //foreach (var item in res)
            //{
            //    if (TodayDate > item.Start_Date && TodayDate < item.Late_Date)
            //    {
            //        bindDistrict();
            //        return View();
            //    }
            //    else
            //    {
            //        return RedirectToAction("Error", "Shared");
            //    }
            //}
            //return RedirectToAction("Error", "Shared");
        }
       

        [HttpPost]
        public JsonResult Registration(Tbl_Registration model)
        {
            try
            {

                if (ModelState.IsValid)
                {

                  
                    if (model.First_Name == null) { return Json(new { Result = false, Response = "Please EnterFirst_Name." }, JsonRequestBehavior.AllowGet); }
                    if (model.Mother_Name == null) { return Json(new { Result = false, Response = "Please Enter Mother_Name." }, JsonRequestBehavior.AllowGet); }
                    if (model.Mobile_No == null) { return Json(new { Result = false, Response = "Please Enter Mobile_No." }, JsonRequestBehavior.AllowGet); }
                    if (model.Center == null) { return Json(new { Result = false, Response = "Please Enter Center." }, JsonRequestBehavior.AllowGet); }
                    if (model.Standard == null) { return Json(new { Result = false, Response = "Please Enter Standard." }, JsonRequestBehavior.AllowGet); }
                    if (model.Candidate_Handicaped_YN == null) { return Json(new { Result = false, Response = "Please Enter Candidate_Handicaped_YN." }, JsonRequestBehavior.AllowGet); }
                    if (model.AgeCertificate == null) { return Json(new { Result = false, Response = "Pleaseupload Age Certification Proof." }, JsonRequestBehavior.AllowGet); }
                    if (model.Upload_Photo == null) { return Json(new { Result = false, Response = "Please Upload_Photo." }, JsonRequestBehavior.AllowGet); }
                    if (model.Upload_Sign == null) { return Json(new { Result = false, Response = "Please Upload_sign." }, JsonRequestBehavior.AllowGet); }
                    if (model.Previous_Attend_School_YN == null) { return Json(new { Result = false, Response = "Please select Previous_Attend_School_YN." }, JsonRequestBehavior.AllowGet); }
                    if (model.Email == null) { return Json(new { Result = false, Response = "Please Enter Email." }, JsonRequestBehavior.AllowGet); }
                    if (model.Gender == null) { return Json(new { Result = false, Response = "Please select Gender" }, JsonRequestBehavior.AllowGet); }

                    if (model.SUBNSQF == null)
                    {
                        var comman1 = model.SUB.ToList();
                        int commanCount1 = comman1.Count;
                        Tbl_Subject obj = new Tbl_Subject();
                        if (commanCount1 == 5)
                        {
                            model.Subject1 = comman1[0].ToString();
                            model.Subject2 = comman1[1].ToString();
                            model.Subject3 = comman1[2].ToString();
                            model.Subject4 = comman1[3].ToString();
                            model.Subject5 = comman1[4].ToString();
                            model.Subject_List = model.Subject1 + ", " + model.Subject2 + ", " + model.Subject3 + ", " + model.Subject4 + ", " + model.Subject5;
                            var sub1 = "";
                            var sub2 = "";
                            var sub3 = "";
                            var sub4 = "";
                            var sub5 = "";

                            Tbl_Subject[] tbl_Subjects = common.Get_Nsqf_Sub(model);
                            sub1 = tbl_Subjects[0].Subject_Name + "(" + tbl_Subjects[0].Subject_Code + ")";
                            sub2 = tbl_Subjects[1].Subject_Name + "(" + tbl_Subjects[1].Subject_Code + ")";
                            sub3 = tbl_Subjects[2].Subject_Name + "(" + tbl_Subjects[2].Subject_Code + ")";
                            sub4 = tbl_Subjects[3].Subject_Name + "(" + tbl_Subjects[3].Subject_Code + ")";
                            sub5 = tbl_Subjects[4].Subject_Name + "(" + tbl_Subjects[4].Subject_Code + ")";

                            TempData["Subject_List"] = sub1 + " , " + sub2 + " , " + sub3 + " , " + sub4 + " , " + sub5;

                            model.Subject_List = TempData["Subject_List"].ToString();
                        }
                        else
                        {

                            return Json(new { success = false, message = "You have to select only 5 subjects from available groups" }, JsonRequestBehavior.AllowGet);

                        }
                    }
                    else
                    {
                        var comman = model.SUB.ToList();
                        var nsqfSub = model.SUBNSQF.ToList();
                        int nsqfsubCount = nsqfSub.Count;
                        int commanCount = comman.Count;
                        var totalSubject = commanCount + nsqfsubCount;

                        var sub1 = "";
                        var sub2 = "";
                        var sub3 = "";
                        var sub4 = "";
                        var sub5 = "";

                        Tbl_Subject[] tbl_Subjects = common.Get_Nsqf_Sub(model);
                        sub1 = tbl_Subjects[0].Subject_Name + "(" + tbl_Subjects[0].Subject_Code + ")";
                        sub2 = tbl_Subjects[1].Subject_Name + "(" + tbl_Subjects[1].Subject_Code + ")";
                        sub3 = tbl_Subjects[2].Subject_Name + "(" + tbl_Subjects[2].Subject_Code + ")";
                        sub4 = tbl_Subjects[3].Subject_Name + "(" + tbl_Subjects[3].Subject_Code + ")";
                        sub5 = tbl_Subjects[4].Subject_Name + "(" + tbl_Subjects[4].Subject_Code + ")";
                        TempData["Subject_List"] = sub1 + " , " + sub2 + " , " + sub3 + " , " + sub4 + " , " + sub5;

                        model.Subject_List = TempData["Subject_List"].ToString();
                        if (totalSubject == 5)
                        {
                            if (nsqfsubCount <= 2)
                            {
                                if (nsqfSub.Count == 1)
                                {
                                    model.Nsqf_Subject = nsqfSub[0].ToString();

                                    model.Subject1 = comman[0].ToString();
                                    model.Subject2 = comman[1].ToString();
                                    model.Subject3 = comman[2].ToString();
                                    model.Subject4 = comman[3].ToString();
                                    //model.Subject_List = model.Subject1 + ", " + model.Subject2 + ", " + model.Subject3 + ", " + model.Subject4;
                                    model.Subject_List = TempData["Subject_List"].ToString();
                                }
                                else
                                {
                                    var NSQF_Subject1 = nsqfSub[0].ToString();
                                    var NSQF_Subject2 = nsqfSub[1].ToString();
                                    model.Nsqf_Subject = NSQF_Subject1 + " " + NSQF_Subject2;

                                    model.Subject1 = comman[0].ToString();
                                    model.Subject2 = comman[1].ToString();
                                    model.Subject3 = comman[2].ToString();
                                    //model.Subject_List = model.Subject1 + ", " + model.Subject2 + ", " + model.Subject3;
                                    model.Subject_List = TempData["Subject_List"].ToString();
                                }
                            }
                            else
                            {
                                return Json(new { success = false, message = "Select Only 1 or 2 Subject from NSQF Subject" }, JsonRequestBehavior.AllowGet);
                            }
                        }
                        else
                        {
                            return Json(new { success = false, message = "Select only 5 Subject in Group A,B,C,D." }, JsonRequestBehavior.AllowGet);
                        }
                    }


                    string hostName = Dns.GetHostName();
                    model.ip = Dns.GetHostByName(hostName).AddressList[0].ToString();
                    model.DateNow = DateTime.Now;
                    Random r = new Random();
                    int genRand = r.Next(1000, 9999);
                    if (model.Standard == "5th")
                    {

                        model.ApplicationId = "MSBOSPR05"+ Common.Get_Year_Id() + genRand;
                    }
                    else
                    {
                        model.ApplicationId = "MSBOSPR08" + Common.Get_Year_Id() + genRand;
                    }
                    var ab = model.ApplicationId;
                    TempData["USERNAME"] = ab;
                    model.Year_Id = Common.Get_Year();
                    db.Tbl_Registration.Add(model);
                    db.SaveChanges();

                    var data = db.Tbl_Registration.Where(x => x.ApplicationId == model.ApplicationId).FirstOrDefault();
                    if (model.Upload_Photo != null || model.Upload_Sign != null || model.AgeCertificate != null)
                    {
                        string file = Path.GetExtension(model.Upload_Photo.FileName);

                        string Filename = data.ApplicationId + file;
                        model.Photo = "../Uploads/Photo/" + Filename;
                        model.Upload_Photo.SaveAs(Path.Combine(Server.MapPath("~/Uploads/Photo/"), Filename));


                        string file1 = Path.GetExtension(model.Upload_Sign.FileName);

                        string Filename1 = data.ApplicationId + file1;
                        model.Signature = "../Uploads/Signature/" + Filename1;
                        model.Upload_Sign.SaveAs(Path.Combine(Server.MapPath("~/Uploads/Signature/"), Filename1));

                        string file2 = Path.GetExtension(model.AgeCertificate.FileName);

                        string Filename2 = data.ApplicationId + file2;
                        model.Age_Certified_Proof = "../Uploads/AgeCertificate/" + Filename2;
                        model.AgeCertificate.SaveAs(Path.Combine(Server.MapPath("~/Uploads/AgeCertificate/"), Filename2));

                        if (model.Candidate_Handicaped_YN == "No")
                        {
                            model.Handicap = "N.A.";
                        }
                      
                        FormsAuthentication.SetAuthCookie(model.ApplicationId, false);
                        var user = this.User.Identity.Name;


                        var aa = db.Center_Login_Information.Where(x => x.Center_Name == model.Center).FirstOrDefault().Contact_Center_Code;
                        model.Center_Code = aa;
                        var DivCode = db.Tbl_CenterList.Where(x => x.center_code == aa).FirstOrDefault().div_code;
                        int id = model.Id;
                        string Enrollment_No = Common.Get_Year_Id()+"0" + DivCode.ToString() + (100000 + id).ToString();
                        model.Enrollment_No = Enrollment_No;

                        db.Tbl_Registration.Attach(model);
                        db.Entry(model).Property(x => x.Center_Code).IsModified = true;
                        db.Entry(model).Property(x => x.Enrollment_No).IsModified = true;
                        db.Entry(model).Property(x => x.ApplicationId).IsModified = true;
                        db.Entry(model).Property(x => x.Handicap).IsModified = true;
                        db.Entry(model).Property(x => x.Photo).IsModified = true;
                        db.Entry(model).Property(x => x.Signature).IsModified = true;
                        db.Entry(model).Property(x => x.Age_Certified_Proof).IsModified = true;
                        db.SaveChanges();
                        TempData["Mobile_No"] = model.Mobile_No;
                        var centerCode = db.Center_Login_Information.Where(x => x.Center_Name == model.Center).ToList();
                        foreach (var item in centerCode)
                        {

                            TempData["UDISE_No"] = item.UDISE_No;
                            TempData["Contact_Center_Code"] = item.Contact_Center_Code;
                        }

                        return Json(new { Result = "Submitted", Message = "../dataFrom.htm" }, JsonRequestBehavior.AllowGet);
                    }
                    TempData["Msg"] = "save successfully...!";
                    //return Json(new { Result = true, Response = "Record Sucessfully" }, JsonRequestBehavior.AllowGet);
                }
                return Json(new { Result = true, Response = "Record Sucessfully" }, JsonRequestBehavior.AllowGet);

            }
            catch (Exception ex)
            {

                return Json(new { Result = false, Response = "Failed" + ex }, JsonRequestBehavior.AllowGet);
            }
        }


        public ActionResult PrintForm(Tbl_Registration tbl_Registration, CenterViewModel model, Tbl_payment tbl_Payment)
        {

            //TempData["Subject_List"] = Session["Subject_List"].ToString();
            //string mobileno = TempData["Mobile_No"].ToString();

           

            tbl_Registration = db.Tbl_Registration.Where(x => x.ApplicationId == tbl_Registration.ApplicationId).SingleOrDefault();

            tbl_Payment = db.Tbl_payment.Where(x => x.billing_name == tbl_Registration.First_Name && x.billing_email==tbl_Registration.Email).SingleOrDefault();
           
            tbl_Payment.merchant_param1 = tbl_Registration.ApplicationId;
            db.Tbl_payment.Attach(tbl_Payment);
            db.Entry(tbl_Payment).Property(x => x.merchant_param1).IsModified = true;
            if (tbl_Registration.Id != 0)
            {
                tbl_Registration.Payment_Status = "1";
            }
            else
            {

                tbl_Registration.Payment_Status = "0";

            }
            var cc = db.Center_Login_Information.Where(x => x.Center_Name == tbl_Registration.Center).FirstOrDefault();
            TempData["Amount"] = tbl_Payment.amount_money;
            TempData["order_status"] = tbl_Payment.order_status;
            TempData["Card_name"] = tbl_Payment.card_name;
            TempData["tracking_id"] = tbl_Payment.tracking_id;
            TempData["bank_ref_no"] = tbl_Payment.bank_ref_no;
            TempData["trans_date"] = tbl_Payment.trans_date;
            TempData["UDISE_No"] = cc.UDISE_No;
            TempData["Contact_Center_Code"] = tbl_Registration.Center_Code;
            TempData["Subject_List"] = tbl_Registration.Subject_List;
            db.Tbl_Registration.Attach(tbl_Registration);
            db.Entry(tbl_Registration).Property(x => x.Payment_Status).IsModified = true;
            db.SaveChanges();
            return View("PrintForm", tbl_Registration);

        }
        //public ActionResult PrintForm(Tbl_Registration tbl_Registration, CenterViewModel model)
        //{

        //    //TempData["Subject_List"] = Session["Subject_List"].ToString();
        //    string mobileno = TempData["Mobile_No"].ToString();



        //    tbl_Registration = db.Tbl_Registration.Where(x => x.Mobile_No == mobileno).SingleOrDefault();

        //    if (tbl_Registration.Id != 0)
        //    {
        //        tbl_Registration.Payment_Status = "1";
        //    }
        //    else
        //    {

        //        tbl_Registration.Payment_Status = "0";

        //    }
        //    db.Tbl_Registration.Attach(tbl_Registration);
        //    db.Entry(tbl_Registration).Property(x => x.Payment_Status).IsModified = true;
        //    db.SaveChanges();
        //    return View("PrintForm", tbl_Registration);

        //}
        [HttpGet]
        public ActionResult Application_Form(Tbl_Registration ExamFormModel)
        {
            
                Tbl_Registration tbl_Registration = new Tbl_Registration();
                var UserName = ExamFormModel.ApplicationId;
                tbl_Registration = db.Tbl_Registration.Where(os => os.ApplicationId == UserName).FirstOrDefault();
                Tbl_Subject tbl_Subject1 = new Tbl_Subject();
                Tbl_Subject tbl_Subject2 = new Tbl_Subject();
                Tbl_Subject tbl_Subject3 = new Tbl_Subject();
                Tbl_Subject tbl_Subject4 = new Tbl_Subject();
                Tbl_Subject tbl_Subject5 = new Tbl_Subject();

            Tbl_Subject[] tbl_Subjects =  common.Get_Nsqf_Sub(tbl_Registration);
          

            var examForm = new CenterViewModel
            {
                tbl_Registration = db.Tbl_Registration.Where(os => os.ApplicationId == UserName).FirstOrDefault(),
                Center_Login = db.Center_Login_Information.Where(c => c.Contact_Center_Code == tbl_Registration.Center_Code).FirstOrDefault(),
                Subject1 = tbl_Subjects[0].Subject_Name + "(" + tbl_Subjects[0].Subject_Code + ")",
                Subject2 = tbl_Subjects[1].Subject_Name + "(" + tbl_Subjects[1].Subject_Code + ")",
                Subject3 = tbl_Subjects[2].Subject_Name + "(" + tbl_Subjects[2].Subject_Code + ")",
                Subject4 = tbl_Subjects[3].Subject_Name + "(" + tbl_Subjects[3].Subject_Code + ")",
                Subject5 = tbl_Subjects[4].Subject_Name + "(" + tbl_Subjects[4].Subject_Code + ")",




            };



            return View(examForm);
           
        }

        [HttpPost]
       
        public JsonResult Save_Application_Form(CenterViewModel model)
        {
          

            try
            {
                if (ModelState.IsValid)
                {
                    Tbl_Application_Form obj = new Tbl_Application_Form();
                    obj.Form_No=model.tbl_Registration.ApplicationId ;
                    obj.UDISE_No = model.Center_Login.UDISE_No.ToString();
                    obj.Contact_center = model.tbl_Registration.Center_Code;
                    obj.Last_name = model.tbl_Registration.Last_Name;
                    obj.Name = model.tbl_Registration.First_Name;
                    obj.Middle_name = model.tbl_Registration.Middle_Name;
                    obj.Mother_name = model.tbl_Registration.Mother_Name;
                    obj.Address = model.tbl_Registration.Address;
                    obj.Pincode = model.tbl_Registration.Pin_Code;
                    obj.Mobile_no = model.tbl_Registration.Mobile_No;
                    obj.Place_of_birth = model.tbl_Registration.DOB_Village;
                    obj.Date_of_birth = model.tbl_Registration.Date_of_Birth;
                    obj.Adhar_no = model.tbl_Registration.Adhar_no;
                    obj.Gender = model.tbl_Registration.Gender;
                    obj.Minority_Religion = model.tbl_Registration.Minority_Religion;
                    obj.Divyang = model.tbl_Registration.Handicap;
                    obj.Medium = model.tbl_Registration.Medium;
                    obj.Type_Of_User = model.Type_Of_User;
                    obj.Subject1 = model.Subject1;
                    obj.Subject2 = model.Subject2;
                    obj.Subject3 = model.Subject3;
                    obj.Subject4 = model.Subject4;
                    obj.Subject5 = model.Subject5;
                    obj.Subjects = model.Subject1 + "," + model.Subject2 + "," + model.Subject3+","+model.Subject4+","+model.Subject5 ;

                    obj.EC_Number = model.tbl_Registration.Enrollment_No;
                    obj.EC_Year = model.EC_Year;
                    obj.LastExamSeatNo = model.LastExamSeatNo;
                    obj.LastExamYear = model.LastExamYear;
                    obj.DateNow = DateTime.Now.ToString();

                  


                    db.Tbl_Application_Form.Add(obj);
                    db.SaveChanges();

                    _Con = new SqlConnection(ConfigurationManager.ConnectionStrings["ConnectionString1"].ConnectionString);
                    _Con.Open();
                    _Command = new SqlCommand("Update Tbl_Registration set Exam_Form_Disable='Downloaded' where ApplicationId='" + obj.Form_No + "'", _Con);
                    _Command.ExecuteNonQuery();
                    _Con.Close();

                }
                return Json(new { Result = true, Response = "Record Saved Successfully" }, JsonRequestBehavior.AllowGet);
            }
            catch (Exception ex)
            {

                return Json(new { Result = false, Response = "Failed" + ex }, JsonRequestBehavior.AllowGet);
            }
        }


        [HttpGet]
        public ActionResult PrintExamApplicationForm(CenterViewModel centerViewModel)
        {
            Tbl_Registration oS_Form_Data = new Tbl_Registration();
            var UserName = centerViewModel.ApplicationId;
            oS_Form_Data = db.Tbl_Registration.Where(os => os.ApplicationId == UserName).FirstOrDefault();
            Tbl_Subject tbl_Subject1 = new Tbl_Subject();
            Tbl_Subject tbl_Subject2 = new Tbl_Subject();
            Tbl_Subject tbl_Subject3 = new Tbl_Subject();
            Tbl_Subject tbl_Subject4 = new Tbl_Subject();
            Tbl_Subject tbl_Subject5 = new Tbl_Subject();

            Tbl_Subject[] tbl_Subjects = common.Get_Nsqf_Sub(oS_Form_Data);


            var examForm = new CenterViewModel
            {
                tbl_Registration = db.Tbl_Registration.Where(os => os.ApplicationId == UserName).FirstOrDefault(),
                Center_Login = db.Center_Login_Information.Where(c => c.Contact_Center_Code == oS_Form_Data.Center_Code).FirstOrDefault(),
                Subject1 = tbl_Subjects[0].Subject_Name + "(" + tbl_Subjects[0].Subject_Code + ")",
                Subject2 = tbl_Subjects[1].Subject_Name + "(" + tbl_Subjects[1].Subject_Code + ")",
                Subject3 = tbl_Subjects[2].Subject_Name + "(" + tbl_Subjects[2].Subject_Code + ")",
                Subject4 = tbl_Subjects[3].Subject_Name + "(" + tbl_Subjects[3].Subject_Code + ")",
                Subject5 = tbl_Subjects[4].Subject_Name + "(" + tbl_Subjects[4].Subject_Code + ")",




            };

            return View(examForm);
        }
        public JsonResult hallTickit(String myJSON)
        {
            try
            {

                Tbl_Registration jobject = JsonConvert.DeserializeObject<Tbl_Registration>(myJSON);
                Tbl_Registration isexist = db.Tbl_Registration.Where(x => x.ApplicationId == jobject.ApplicationId).FirstOrDefault();
               
                if (isexist != null)
                {
                    return Json(new { Result = "Success", Message = "PrintExamApplicationForm?User_Name=" + jobject.ApplicationId }, JsonRequestBehavior.AllowGet);
                }
                else
                {
                    Tbl_Subject tbl_Subject1 = new Tbl_Subject();
                    Tbl_Subject tbl_Subject2 = new Tbl_Subject();
                    Tbl_Subject tbl_Subject3 = new Tbl_Subject();
                    Tbl_Subject tbl_Subject4 = new Tbl_Subject();
                    Tbl_Subject tbl_Subject5 = new Tbl_Subject();
                    Tbl_Registration oS_Form_Data = new Tbl_Registration();
                    CenterViewModel centerVM = new CenterViewModel();
                    oS_Form_Data = db.Tbl_Registration.Where(os => os.ApplicationId == jobject.ApplicationId).FirstOrDefault();
                    string hostName = Dns.GetHostName();
                    Console.WriteLine(hostName);
                    jobject.ip = Dns.GetHostEntry(hostName).AddressList[0].ToString();
                    var UserName = oS_Form_Data.ApplicationId;




                    Tbl_Subject[] tbl_Subjects = common.Get_Nsqf_Sub(oS_Form_Data);


                    var examForm = new CenterViewModel
                    {
                        tbl_Registration = db.Tbl_Registration.Where(os => os.ApplicationId == UserName).FirstOrDefault(),
                        Center_Login = db.Center_Login_Information.Where(c => c.Contact_Center_Code == oS_Form_Data.Center_Code).FirstOrDefault(),
                        Subject1 = tbl_Subjects[0].Subject_Name + "(" + tbl_Subjects[0].Subject_Code + ")",
                        Subject2 = tbl_Subjects[1].Subject_Name + "(" + tbl_Subjects[1].Subject_Code + ")",
                        Subject3 = tbl_Subjects[2].Subject_Name + "(" + tbl_Subjects[2].Subject_Code + ")",
                        Subject4 = tbl_Subjects[3].Subject_Name + "(" + tbl_Subjects[3].Subject_Code + ")",
                        Subject5 = tbl_Subjects[4].Subject_Name + "(" + tbl_Subjects[4].Subject_Code + ")",
                    };


                    db.Tbl_Registration.Add(jobject);

                    db.SaveChanges();
                    _Con = new SqlConnection(ConfigurationManager.ConnectionStrings["ConnectionString1"].ConnectionString);
                    _Con.Open();
                    _Command = new SqlCommand("Update Tbl_Registration set Hidden='Downloaded' where Id='" + oS_Form_Data.Id + "'", _Con);
                    _Command.ExecuteNonQuery();
                    _Con.Close();
                    return Json(new { Result = "Success", Message = "PrintExamApplicationForm?User_Name=" + jobject.ApplicationId }, JsonRequestBehavior.AllowGet);
                }

            }
            catch (Exception ex)
            {
                _Con.Close();
                return Json(new { Result = "Failed", Message = "Exam form submission failed" }, JsonRequestBehavior.AllowGet);
                throw ex;
            }

        }
        public void bindDistrict()
        {
            try
            {
                var districtList = db.Tbl_District.ToList();
                List<SelectListItem> li = new List<SelectListItem>();
                li.Add(new SelectListItem { Text = "-Select District-", Value = "0" });

                foreach (var m in districtList)
                {
                    li.Add(new SelectListItem { Text = m.District, Value = m.Id.ToString() });
                    ViewBag.districtList = li;
                }
            }
            catch (Exception ex)
            {
                throw ex;
            }

        }

        public JsonResult getTaluka(string id)
        {
            try
            {
                var talukaList = db.All_Taluka.Where(s => s.District == id).GroupBy(x => x.Taluka).Select(grp => grp.FirstOrDefault()).ToList();
                List<SelectListItem> licent = new List<SelectListItem>();
                licent.Add(new SelectListItem { Text = "-Select Taluka-", Value = "0" });
                if (talukaList != null)
                {
                    foreach (var x in talukaList)
                    {
                        licent.Add(new SelectListItem { Text = x.Taluka, Value = x.Taluka.ToString() });
                    }
                }
                return Json(new SelectList(licent, "Value", "Text", JsonRequestBehavior.AllowGet));
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        public JsonResult getCenter(string id)
        {
            try
            {
                var centerList = db.Tbl_CenterList.Where(x => x.taluka == id).ToList();
                List<SelectListItem> licent = new List<SelectListItem>();
                licent.Add(new SelectListItem { Text = "-Select Center-", Value = "0" });
                if (centerList != null)
                {
                    foreach (var x in centerList)
                    {
                        licent.Add(new SelectListItem { Text = x.center_name, Value = x.center_name.ToString() });
                    }
                }
                return Json(new SelectList(licent, "Value", "Text", JsonRequestBehavior.AllowGet));
            }
            catch (Exception ex)
            {
                throw ex;
            }

        }

        public JsonResult getCenterCode(string center)
        {
            try
            {
                var centerCode = db.Tbl_CenterList.Where(x => x.center_name == center).FirstOrDefault();
                return Json(centerCode.center_code, JsonRequestBehavior.AllowGet);
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }
        public JsonResult getSubject(string id, string handicaped, Tbl_Registration subject)
        {
            try
            {
                subject.SubjectListA = db.Tbl_Subject.Where(x => x.Class == id && x.Handicaped == handicaped && x.Subject_Group == "A").ToList<Tbl_Subject>();
                subject.SubjectListB = db.Tbl_Subject.Where(x => x.Class == id && x.Handicaped == handicaped && x.Subject_Group == "B").ToList<Tbl_Subject>();
                subject.SubjectListC = db.Tbl_Subject.Where(x => x.Class == id && x.Handicaped == handicaped && x.Subject_Group == "C").ToList<Tbl_Subject>();
                if (handicaped == "No" && id == "8th")
                {
                    subject.SubjectListD = db.Tbl_Subject.Where(x => x.Class == id && x.Handicaped == handicaped && x.Subject_Group == "D").ToList<Tbl_Subject>();
                    var allList = new { SubjectListA = subject.SubjectListA, SubjectListB = subject.SubjectListB, SubjectListC = subject.SubjectListC, SubjectListD = subject.SubjectListD };
                    return Json(allList, JsonRequestBehavior.AllowGet);
                }
                else
                {
                    var allList = new { SubjectListA = subject.SubjectListA, SubjectListB = subject.SubjectListB, SubjectListC = subject.SubjectListC };
                    return Json(allList, JsonRequestBehavior.AllowGet);
                }

               
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        public JsonResult getNSQFSubject(string standard, Tbl_Registration subject)
        {
            try
            {
                subject.SubjectListD_NSQF = db.Tbl_Subject.Where(x => x.Class == standard && x.Subject_Group == "D-NSQF").ToList<Tbl_Subject>();
                return Json(subject.SubjectListD_NSQF, JsonRequestBehavior.AllowGet);
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

    }
}
   