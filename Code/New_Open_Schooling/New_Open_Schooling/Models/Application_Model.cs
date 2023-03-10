using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.ComponentModel.DataAnnotations;

namespace New_Open_Schooling.Models
{
    public class Application_Model
    {
        [Required(ErrorMessage = " User Id is required")]
        [RegularExpression(@"^(\d{8})$", ErrorMessage = "No white space allowed")]
        [StringLength(8, MinimumLength = 8, ErrorMessage = "Please fill the form no")]
        public string Form_No { get; set; }
        [Required(ErrorMessage = "UID is required")]
        [RegularExpression(@"^(\d{8})$", ErrorMessage = "No white space allowed")]
        [StringLength(8, MinimumLength = 8, ErrorMessage = "Please fill the valid User Id")]
        public string UDISE_No { get; set; }
        [Required(ErrorMessage = "Center is required")]
        public string Contact_center { get; set; }
        [Required(ErrorMessage = "  Last Name  is required")]
        [DataType(DataType.Text)]
        [RegularExpression("^[a-zA-Z_ ]*$", ErrorMessage = "Not a valid  name")]
        public string Last_name { get; set; }
        [Required(ErrorMessage = "  First_Name  is required")]
        [DataType(DataType.Text)]
        [RegularExpression("^[a-zA-Z_ ]*$", ErrorMessage = "Not a valid  name")]
        public string Name { get; set; }
        [Required(ErrorMessage = "  Middle_Name  is required")]
        [DataType(DataType.Text)]
        [RegularExpression("^[a-zA-Z_ ]*$", ErrorMessage = "Not a valid  name")]
        public string Middle_name { get; set; }
        [Required(ErrorMessage = "  Mother_Name  is required")]
        [DataType(DataType.Text)]
        [RegularExpression("^[a-zA-Z_ ]*$", ErrorMessage = "Not a valid  name")]
        public string Mother_name { get; set; }
        [Required(ErrorMessage = " Address  is required")]
        [DataType(DataType.Text)]
        [RegularExpression("^[a-zA-Z_ ]*$", ErrorMessage = "Not a valid  name")]
        public string Address { get; set; }
        [Required(ErrorMessage = "  pin_code  is required")]
        public string Pincode { get; set; }
        [Required(ErrorMessage = " You must provide a mobile number")]
        [DataType(DataType.PhoneNumber)]
        [RegularExpression(@"^(\d{10})$", ErrorMessage = "Not a valid  number")]
        public string Mobile_no { get; set; }
        [Required(ErrorMessage = "  Place of Birth  is required")]
        public string Place_of_birth { get; set; }
        [Required(ErrorMessage = "  Date of Birth  is required")]
        public string Date_of_birth { get; set; }
        [RegularExpression(@"^(\d{12})$", ErrorMessage = "Not a valid Adhar number")]
        [StringLength(12, MinimumLength = 12, ErrorMessage = "Please fill the valid Adhar number")]
        public string Adhar_no { get; set; }
        [Required(ErrorMessage = " Gender  is required")]
        public string Gender { get; set; }
        [Required(ErrorMessage = "  Minority Religion  is required")]
        public string Minority_Religion { get; set; }
        [Required(ErrorMessage = " Category  is required")]
        public string Category { get; set; }
        [Required(ErrorMessage = "  Divang  is required")]
        public string Divyang { get; set; }
        [Required(ErrorMessage = " required...")]
        public string Medium { get; set; }
        [Required(ErrorMessage = " required...")]
        public string Type_Of_User { get; set; }
        [Required(ErrorMessage = " required...")]
        public string Subjects { get; set; }
        [Required(ErrorMessage = " required...")]
        public string Subject1 { get; set; }
        [Required(ErrorMessage = " required...")]
        public string Subject2 { get; set; }
        [Required(ErrorMessage = " required...")]
        public string Subject3 { get; set; }
        [Required(ErrorMessage = " required...")]
        public string Subject4 { get; set; }
        [Required(ErrorMessage = " required...")]
        public string Subject5 { get; set; }
        [Required(ErrorMessage = " required...")]
        public string EC_Year { get; set; }
        [Required(ErrorMessage = " required...")]
        public string EC_Number { get; set; }
        [Required(ErrorMessage = " required...")]
        public string LastExamYear { get; set; }
        [Required(ErrorMessage = " required...")]
        public string LastExamSeatNo { get; set; }
        [Required(ErrorMessage = " required...")]
        public string Year_Id { get; set; }

        public string Ip { get; set; }
        public string DateNow { get; set; }
       
        public string Photo { get; set; }
        public string Signature { get; set; }
        public int Id { get; set; }
        public string Seat_No { get; set; }
        public HttpPostedFileBase Upload_Photo { get; set; }
        public HttpPostedFileBase Upload_Sign { get; set; }
    }
   
}