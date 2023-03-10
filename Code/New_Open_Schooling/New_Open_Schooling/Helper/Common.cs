using New_Open_Schooling.Models;
using Newtonsoft.Json;
using OfficeOpenXml;
using System;
using System.Collections.Generic;
using System.Data;
using System.IO;
using System.Linq;
using System.Reflection;
using System.Web;

namespace New_Open_Schooling.Helper
{
    public class Common
    {
        Open_Schooling_Final_2023Entities1 db = new Open_Schooling_Final_2023Entities1();
        public Registration_Model Get_Login_Details()
        {
            //string login_string= System.Web.HttpContext.Current.User.Identity.Name;
            string login_string = HttpContext.Current.User.Identity.Name;
            Registration_Model login_model = JsonConvert.DeserializeObject<Registration_Model>(login_string);
            return login_model;
        }
        public static string Get_Year_Id()
        {
            return "22";
        }

        public static string Get_Year()
        {
            return "2022-23";
        }
        public void CreateExcelFile(DataTable dt_list, string filename)
        {
            try
            {
                DataSet ds1 = new DataSet();
                ds1.Tables.Add(dt_list);
                using (DataSet ds = ds1)
                {
                    if (ds != null && ds.Tables.Count > 0)
                    {
                        string rootFolder = HttpContext.Current.Server.MapPath("~/State").ToString();
                        string fileName = @"" + filename + ".xlsx";

                        System.IO.FileInfo file = new System.IO.FileInfo(Path.Combine(rootFolder, fileName));
                        using (ExcelPackage xp = new ExcelPackage(file))
                        {
                            foreach (DataTable dt in ds.Tables)
                            {
                                ExcelWorksheet ws = xp.Workbook.Worksheets.Add(dt.TableName);
                                int rowstart = 1;
                                int colstart = 1;
                                int rowend = rowstart;
                                int colend = colstart + dt.Columns.Count;
                                rowend = rowstart + dt.Rows.Count;
                                ws.Cells[rowstart, colstart].LoadFromDataTable(dt, true);
                                int i = 1;
                                foreach (DataColumn dc in dt.Columns)
                                {
                                    i++;
                                    if (dc.DataType == typeof(decimal))
                                        ws.Column(i).Style.Numberformat.Format = "#0.00";
                                }
                                ws.Cells[ws.Dimension.Address].AutoFitColumns();
                                ws.Cells[rowstart, colstart, rowend, colend].Style.Border.Top.Style =
                                   ws.Cells[rowstart, colstart, rowend, colend].Style.Border.Bottom.Style =
                                   ws.Cells[rowstart, colstart, rowend, colend].Style.Border.Left.Style =
                                   ws.Cells[rowstart, colstart, rowend, colend].Style.Border.Right.Style = OfficeOpenXml.Style.ExcelBorderStyle.Thin;

                            }
                            xp.Save();
                        }
                    }
                }
            }
            catch
            {
                throw;
            }
        }

        public DataTable ToDataTable<T>(List<T> items)
        {
            DataTable dataTable = new DataTable(typeof(T).Name);

            //Get all the properties
            PropertyInfo[] Props = typeof(T).GetProperties(BindingFlags.Public | BindingFlags.Instance);
            foreach (PropertyInfo prop in Props)
            {
                //Defining type of data column gives proper data table 
                var type = (prop.PropertyType.IsGenericType && prop.PropertyType.GetGenericTypeDefinition() == typeof(Nullable<>) ? Nullable.GetUnderlyingType(prop.PropertyType) : prop.PropertyType);
                //Setting column names as Property names
                dataTable.Columns.Add(prop.Name, type);
            }
            foreach (T item in items)
            {
                var values = new object[Props.Length];
                for (int i = 0; i < Props.Length; i++)
                {
                    //inserting property values to datatable rows
                    values[i] = Props[i].GetValue(item, null);
                }
                dataTable.Rows.Add(values);
            }
            //put a breakpoint here and check datatable
            return dataTable;
        }


        public Tbl_Subject[] Get_Nsqf_Sub(Tbl_Registration oS_Form_Data)
        {
            Tbl_Subject[] model = new Tbl_Subject[5];
            


            if (oS_Form_Data.SUBNSQF != null )
            {
                var normal_sub = oS_Form_Data.SUB.ToList();
                var nsqfSub = oS_Form_Data.SUBNSQF.ToList();
                //string[] nsqfsub = oS_Form_Data.Nsqf_Subject.Split(' ');
                if (nsqfSub.Count == 2)
                {
                    string sub1 = "";
                    sub1 = normal_sub[0];
                    string sub2 = "";
                    sub2 = normal_sub[1];
                    string sub3 = "";
                    sub3 = normal_sub[2];
                    model[0] = db.Tbl_Subject.Where(s => s.Subject_Code == sub1).FirstOrDefault();
                    model[1] = db.Tbl_Subject.Where(s => s.Subject_Code == sub2).FirstOrDefault();
                    model[2] = db.Tbl_Subject.Where(s => s.Subject_Code == sub3).FirstOrDefault();
                    string nsqf1 = "";
                    nsqf1 = nsqfSub[0];
                    string nsqf2 = "";
                    nsqf2 = nsqfSub[1];
                    model[3] = db.Tbl_Subject.Where(s => s.Subject_Code == nsqf1).FirstOrDefault();
                    model[4] = db.Tbl_Subject.Where(s => s.Subject_Code == nsqf2).FirstOrDefault();
                }
                else if (nsqfSub.Count == 1)
                {
                    string sub1 = "";
                    sub1 = normal_sub[0];
                    string sub2 = "";
                    sub2 = normal_sub[1];
                    string sub3 = "";
                    sub3 = normal_sub[2];
                    string sub4 = "";
                    sub4 = normal_sub[3];
                    model[0] = db.Tbl_Subject.Where(s => s.Subject_Code == sub1).FirstOrDefault();
                    model[1] = db.Tbl_Subject.Where(s => s.Subject_Code == sub2).FirstOrDefault();
                    model[2] = db.Tbl_Subject.Where(s => s.Subject_Code == sub3).FirstOrDefault();
                    model[3] = db.Tbl_Subject.Where(s => s.Subject_Code == sub4).FirstOrDefault();
                    string nsqf = "";
                    nsqf = nsqfSub[0];
                    model[4] = db.Tbl_Subject.Where(s => s.Subject_Code == nsqf).FirstOrDefault();
                }
                else
                {
                    model[0] = db.Tbl_Subject.Where(s => s.Subject_Code == oS_Form_Data.Subject1).FirstOrDefault();
                    model[1] = db.Tbl_Subject.Where(s => s.Subject_Code == oS_Form_Data.Subject2).FirstOrDefault();
                    model[2] = db.Tbl_Subject.Where(s => s.Subject_Code == oS_Form_Data.Subject3).FirstOrDefault();
                    model[3] = db.Tbl_Subject.Where(s => s.Subject_Code == oS_Form_Data.Subject4).FirstOrDefault();
                    model[4] = db.Tbl_Subject.Where(s => s.Subject_Code == oS_Form_Data.Subject5).FirstOrDefault();
                }
            }
            else
            {
                model[0] = db.Tbl_Subject.Where(s => s.Subject_Code == oS_Form_Data.Subject1).FirstOrDefault();
                model[1] = db.Tbl_Subject.Where(s => s.Subject_Code == oS_Form_Data.Subject2).FirstOrDefault();
                model[2] = db.Tbl_Subject.Where(s => s.Subject_Code == oS_Form_Data.Subject3).FirstOrDefault();
                model[3] = db.Tbl_Subject.Where(s => s.Subject_Code == oS_Form_Data.Subject4).FirstOrDefault();
                model[4] = db.Tbl_Subject.Where(s => s.Subject_Code == oS_Form_Data.Subject5).FirstOrDefault();
            }
          

            return model;
        }



        //public Tbl_Subject[] Get_Nsqf_Sub(Tbl_Registration oS_Form_Data)
        //{
        //    Tbl_Subject[] model = new Tbl_Subject[5];

        //    if (oS_Form_Data.Nsqf_Subject != null && oS_Form_Data.Nsqf_Subject != "No")
        //    {
        //        string[] nsqfsub = oS_Form_Data.Nsqf_Subject.Split(' ');
        //        if (nsqfsub.Length == 2)
        //        {

        //            model[0] = db.Tbl_Subject.Where(s => s.Subject_Code == oS_Form_Data.Subject1).FirstOrDefault();
        //            model[1] = db.Tbl_Subject.Where(s => s.Subject_Code == oS_Form_Data.Subject2).FirstOrDefault();
        //            model[2] = db.Tbl_Subject.Where(s => s.Subject_Code == oS_Form_Data.Subject3).FirstOrDefault();
        //            string nsqf1 = "";
        //            nsqf1 = nsqfsub[0];
        //            string nsqf2 = "";
        //            nsqf2 = nsqfsub[1];
        //            model[3] = db.Tbl_Subject.Where(s => s.Subject_Code == nsqf1).FirstOrDefault();
        //            model[4] = db.Tbl_Subject.Where(s => s.Subject_Code == nsqf2).FirstOrDefault();
        //        }
        //        else if (nsqfsub.Length == 1)
        //        {
        //            model[0] = db.Tbl_Subject.Where(s => s.Subject_Code == oS_Form_Data.Subject1).FirstOrDefault();
        //            model[1] = db.Tbl_Subject.Where(s => s.Subject_Code == oS_Form_Data.Subject2).FirstOrDefault();
        //            model[2] = db.Tbl_Subject.Where(s => s.Subject_Code == oS_Form_Data.Subject3).FirstOrDefault();
        //            model[3] = db.Tbl_Subject.Where(s => s.Subject_Code == oS_Form_Data.Subject4).FirstOrDefault();
        //            string nsqf = "";
        //            nsqf = nsqfsub[0];
        //            model[4] = db.Tbl_Subject.Where(s => s.Subject_Code == nsqf).FirstOrDefault();
        //        }
        //        else
        //        {
        //            model[0] = db.Tbl_Subject.Where(s => s.Subject_Code == oS_Form_Data.Subject1).FirstOrDefault();
        //            model[1] = db.Tbl_Subject.Where(s => s.Subject_Code == oS_Form_Data.Subject2).FirstOrDefault();
        //            model[2] = db.Tbl_Subject.Where(s => s.Subject_Code == oS_Form_Data.Subject3).FirstOrDefault();
        //            model[3] = db.Tbl_Subject.Where(s => s.Subject_Code == oS_Form_Data.Subject4).FirstOrDefault();
        //            model[4] = db.Tbl_Subject.Where(s => s.Subject_Code == oS_Form_Data.Subject5).FirstOrDefault();
        //        }
        //    }
        //    model[0]= db.Tbl_Subject.Where(s => s.Subject_Code == oS_Form_Data.Subject1).FirstOrDefault();
        //    model[1]= db.Tbl_Subject.Where(s => s.Subject_Code == oS_Form_Data.Subject2).FirstOrDefault();
        //    model[2]= db.Tbl_Subject.Where(s => s.Subject_Code == oS_Form_Data.Subject3).FirstOrDefault();
        //    model[3]= db.Tbl_Subject.Where(s => s.Subject_Code == oS_Form_Data.Subject4).FirstOrDefault();
        //    model[4] = db.Tbl_Subject.Where(s => s.Subject_Code == oS_Form_Data.Subject5).FirstOrDefault();

        //    return model;
        //}
    }
}