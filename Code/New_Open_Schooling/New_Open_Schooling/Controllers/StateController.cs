using New_Open_Schooling.Helper;
using New_Open_Schooling.Models;
using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace New_Open_Schooling.Controllers
{
    public class StateController : Controller
    {
        Open_Schooling_Final_2023Entities1 db = new Open_Schooling_Final_2023Entities1();
        Common common = new Common();
        // GET: State
        public ActionResult StateLogin()
        {
            return View();
        }
        [HttpPost]
        public ActionResult StateLogin(State_Login_Model state_Login_Model)
        {
            try
            {
                if(state_Login_Model.User_Name == "admin" && state_Login_Model.State_Password =="admin")
                {
                    return RedirectToAction("StateDashboard");
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

        public ActionResult StateDashboard()
        {
            string count = "";
            count = "select count( * ) as countData from Tbl_Registration a join tbl_payment b on  a.ApplicationId = b.merchant_param1 and b.order_status = 'success'  and a.Payment_Status = '1'";
            List<Division_List_Model> model = db.Database.SqlQuery<Division_List_Model>(count).ToList();
            foreach (var item in model)
            {
                TempData["count"] ="Total Registered Students are  "+ item.countData;
            }
            
            return View();
        }
        [HttpPost]
        public ActionResult StateDashboard(string Div_Code, string Excel)
        {
            string Query = "", fileName = "";
            try
            {

                
                Query = "select * from Tbl_Registration A join Center_Login_Information B on A.Center_Code=B.Contact_Center_Code join Tbl_payment P on A.ApplicationId=P.merchant_param1 where B.Div_Code='" + Div_Code + "'and A.Payment_Status='1' and P.order_status='Success' ";
                //Query = "select * from Tbl_Registration A join Center_Login_Information B on A.Center_Code=B.Contact_Center_Code where B.Div_Code='" + Div_Code + "'and A.Payment_Status='1' ";
                List<Division_List_Model> model = db.Database.SqlQuery<Division_List_Model>(Query).ToList();
               

                if (Excel == "1")
                {
                    DataTable dt = common.ToDataTable(model);
                    dt.TableName = "District_Batch_List";
                    fileName = DateTime.Now.Day.ToString() + DateTime.Now.Month.ToString() + DateTime.Now.Hour.ToString() + DateTime.Now.Minute.ToString() + DateTime.Now.Second.ToString() + "District_Batch_List";
                    common.CreateExcelFile(dt, fileName);

                }
                return Json(new { Result = true, Response = model, FileName = fileName }, JsonRequestBehavior.AllowGet);
            }
            catch(Exception ex)
            {
                return Json(new { Result = true, Response ="Unable to Fetch Record", }, JsonRequestBehavior.AllowGet);
            }
          
        }
        public ActionResult Download_State_Record()
        {
            
            return View();
        }
        [HttpPost]
        public ActionResult Download_State_Record(string Div_Code,string Excel)
        {
            string Query = "", fileName = "";
            try
                {
                    List<Division_List_Model> centerViewModel = new List<Division_List_Model>();
                   if(Div_Code=="1")
                   {
                    Query = "select * from Tbl_Registration A join Tbl_payment P on A.ApplicationId=P.merchant_param1 where  A.Payment_Status='1' and P.order_status='Success' ";
                    
                    List<Division_List_Model> model = db.Database.SqlQuery<Division_List_Model>(Query).ToList();
                    if (Excel == "1")
                    {
                        DataTable dt = common.ToDataTable(model);
                        dt.TableName = "State_Registered_List";
                        fileName = DateTime.Now.Day.ToString() + DateTime.Now.Month.ToString() + DateTime.Now.Hour.ToString() + DateTime.Now.Minute.ToString() + DateTime.Now.Second.ToString() + "District_Batch_List";
                        common.CreateExcelFile(dt, fileName);

                    }
                    return Json(new { Result = true, Response = model, FileName = fileName }, JsonRequestBehavior.AllowGet);
                  
                }
                else
                   if(Div_Code=="2")
                   {
                    Query = "select * from Tbl_Registration A join Tbl_payment P on A.ApplicationId=P.merchant_param1 where  A.Payment_Status='1' and P.order_status='Success' and A.Ec_Status='Completed'";

                    List<Division_List_Model> model = db.Database.SqlQuery<Division_List_Model>(Query).ToList();
                    if (Excel == "1")
                    {
                        DataTable dt = common.ToDataTable(model);
                        dt.TableName = "State_Registered_List";
                        fileName = DateTime.Now.Day.ToString() + DateTime.Now.Month.ToString() + DateTime.Now.Hour.ToString() + DateTime.Now.Minute.ToString() + DateTime.Now.Second.ToString() + "District_Batch_List";
                        common.CreateExcelFile(dt, fileName);

                    }
                    return Json(new { Result = true, Response = model, FileName = fileName }, JsonRequestBehavior.AllowGet);
                }
                else
                {
                    Query = "select * from Tbl_Registration A join Tbl_payment P on A.ApplicationId=P.merchant_param1 where  A.Payment_Status='1' and P.order_status='Success' and A.Hall_Ticket='1'";

                    List<Division_List_Model> model = db.Database.SqlQuery<Division_List_Model>(Query).ToList();
                    if (Excel == "1")
                    {
                        DataTable dt = common.ToDataTable(model);
                        dt.TableName = "State_Registered_List";
                        fileName = DateTime.Now.Day.ToString() + DateTime.Now.Month.ToString() + DateTime.Now.Hour.ToString() + DateTime.Now.Minute.ToString() + DateTime.Now.Second.ToString() + "District_Batch_List";
                        common.CreateExcelFile(dt, fileName);

                    }
                    return Json(new { Result = true, Response = model, FileName = fileName }, JsonRequestBehavior.AllowGet);
                }

                   

               
                //centerViewModel.AddRange(centerViewModel);
                return Json(new { Result = true, Response = centerViewModel, FileName = fileName }, JsonRequestBehavior.AllowGet);
            }
                catch (Exception ex)
                {
                return Json(new { Result = false, Response = "",  }, JsonRequestBehavior.AllowGet);
            }
           
        }
    }
}