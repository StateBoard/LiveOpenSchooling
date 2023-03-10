using New_Open_Schooling.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace New_Open_Schooling.Controllers
{
    public class Exam_HallTikitController : Controller
    {
        Open_Schooling_Final_2023Entities1 db_Context = new Open_Schooling_Final_2023Entities1();
        int x;
        string y, stand;
        // GET: Exam_HallTikit
        public ActionResult Halltikit()
        {
            return View();
        }

        public string NumberToWords(int number)
        {
            try
            {
                if (number == 0)
                    return "zero";

                if (number < 0)
                    return "minus " + NumberToWords(Math.Abs(number));

                string words = "";

                if ((number / 1000000) > 0)
                {
                    words += NumberToWords(number / 1000000) + " million ";
                    number %= 1000000;
                }

                if ((number / 1000) > 0)
                {
                    words += NumberToWords(number / 1000) + " thousand ";
                    number %= 1000;
                }

                if ((number / 100) > 0)
                {
                    words += NumberToWords(number / 100) + " hundred ";
                    number %= 100;
                }

                if (number > 0)
                {
                    if (words != "")
                        words += "and ";

                    var unitsMap = new[] { "zero", "one", "two", "three", "four", "five", "six", "seven", "eight", "nine", "ten", "eleven", "twelve", "thirteen", "fourteen", "fifteen", "sixteen", "seventeen", "eighteen", "nineteen" };
                    var tensMap = new[] { "zero", "ten", "twenty", "thirty", "forty", "fifty", "sixty", "seventy", "eighty", "ninety" };

                    if (number < 20)
                        words += unitsMap[number];
                    else
                    {
                        words += tensMap[number / 10];
                        if ((number % 10) > 0)

                            words += " " + unitsMap[number % 10];
                    }
                }

                //string seat = lblseatno.Text.Substring(0, 2);
                if (stand == "5")
                {
                    ViewData["Class"] = y + " ZERO FIVE" + "-" + words.ToUpper();
                }
                else if (stand == "8")
                {
                    ViewData["Class"] = y + " ZERO EIGHT" + "- " + words.ToUpper();
                }
                return words;

            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        public ActionResult HalltikitPrint(  Tbl_Registration os_Form_Data)  
        
        {
            try
            {
               
                //ViewBag["Center_Code"] = TempData["Center_Code"].ToString();
                //ViewBag["Division"] = Session["Division"].ToString();
              
                os_Form_Data = db_Context.Tbl_Registration.Where(x => x.ApplicationId == os_Form_Data.ApplicationId || x.Mobile_No == os_Form_Data.Mobile_No).FirstOrDefault();
                    if (os_Form_Data == null)
                    {
                        ViewData["ErrorMsg"] = "Invalid credentials";
                        return View("Halltikit");
                    }

                os_Form_Data.Seat_No = "MH0810014";
                var sub1=db_Context.Tbl_Subject.Where(x=>x.Subject_Code==os_Form_Data.Subject1).FirstOrDefault();
                var sub2 = db_Context.Tbl_Subject.Where(x => x.Subject_Code == os_Form_Data.Subject2).FirstOrDefault();
                var sub3 = db_Context.Tbl_Subject.Where(x => x.Subject_Code == os_Form_Data.Subject3).FirstOrDefault();
                var sub4 = db_Context.Tbl_Subject.Where(x => x.Subject_Code == os_Form_Data.Subject4).FirstOrDefault();
                var sub5 = db_Context.Tbl_Subject.Where(x => x.Subject_Code == os_Form_Data.Subject5).FirstOrDefault();
                TempData["sub1"] = sub1.Subject_Name;
                TempData["sub2"] = sub2.Subject_Name;
                TempData["sub3"] = sub3.Subject_Name;
                TempData["sub4"] = sub4.Subject_Name;
                TempData["sub5"] = sub5.Subject_Name;



                x = Int32.Parse(os_Form_Data.Seat_No.Substring(4, 5));


                //y = hall_Tikit.seatnumber.Substring(0, 2);
                stand = os_Form_Data.Seat_No.Substring(3, 1);
                NumberToWords(x);
                //ViewData["Standard"] = stand;

                //hall_Tikit.Form_No

                //string[] sub = os_Form_Data.Subject_List.Split(';');
                //string Subjects1 = sub[0].TrimStart();
                //string Subjects2 = sub[1].TrimStart();
                //string Subjects3 = sub[2].TrimStart();
                //string Subjects4 = sub[3].TrimStart();
                //string Subjects5 = sub[4].TrimStart();

                //string[] code1 = Subjects1.TrimStart().Split(')');

                //os_Form_Data.Subject1 = code1[1].Replace("(", " ");
                //string[] code2 = Subjects2.TrimStart().Split(')');
                //os_Form_Data.Code2 = code2[0].Replace("(", " ");
                //os_Form_Data.Subject2 = code2[1].Replace("(", " ");
                //string[] code3 = Subjects3.TrimStart().Split(')');
                //os_Form_Data.Code3 = code3[0].Replace("(", " ");
                //os_Form_Data.Subject3 = code3[1].Replace("(", " ");
                //string[] code4 = Subjects4.TrimStart().Split(')');
                //os_Form_Data.Code4 = code4[0].Replace("(", " ");
                //os_Form_Data.Subject4 = code4[1].Replace("(", " ");
                //string[] code5 = Subjects5.TrimStart().Split(')');
                //os_Form_Data.Code5 = code5[0].Replace("(", " ");
                //os_Form_Data.Subject5 = code5[1].Replace("(", " ");

                //ViewData["Photo"] = "/AppFiles/Images/Documents/" + hall_Tikit.Form_No.TrimEnd() + "/Profile" + ".jpg";
                //ViewData["Sign"] = "/AppFiles/Images/Documents/" + hall_Tikit.Form_No.TrimEnd() + "/Sign" + ".jpg";

                return View(os_Form_Data);
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }
    }
}