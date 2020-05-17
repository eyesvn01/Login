namespace Login.Models
{
    using System;
    using System.Collections.Generic;
    using System.ComponentModel.DataAnnotations;
    using System.ComponentModel.DataAnnotations.Schema;
    using System.Data.Entity.Spatial;
    using System.Data.SqlClient;
    using System.Linq;

    public class User
    {
        [Key]
        [Display(Name = "Tên đăng nhập")]
        [Required(ErrorMessage = "Yêu cầu tên đăng nhập.")]
        [RegularExpression(@"^[\w]{6,50}$",ErrorMessage = "Tài khoản không hợp lệ")]                    //Điều kiện của tên đăng nhập
        public string UserName { get; set; }

        [Display(Name = "Mật khẩu")]
        [Required(ErrorMessage = ("Yêu cầu mật khẩu."))]
        [RegularExpression(@"^(?=.*[a-z])(?=.*[A-Z])(?=.*\d)[A-Za-z0-9]{8,50}$", ErrorMessage = "Mật khẩu không hợp lệ")] //Điều kiện của mật khẩu
        public string Password { get; set; }

        [NotMapped]
        [Display(Name = "Mật khẩu xác nhận")]
        [Required(ErrorMessage = ("Mật khẩu xác nhận không khớp."))]
        [Compare("Password", ErrorMessage = "Mật khẩu xác nhận không khớp.")]
        public string ConfirmPassword { get; set; }

        [Required(ErrorMessage = "Yêu cầu nhập email")]
        [RegularExpression(@"^[A-Za-z0-9._-]+@[A-Za-z0-9\.-]+$",ErrorMessage = "Email không hợp lệ")] //Điều kiện của Email
        public string Email { get; set; }

        [NotMapped]
        [Display(Name = "Xác nhận Email")]
        [Required(ErrorMessage = "Email xác nhận không khớp.")]
        [Compare("Email", ErrorMessage = "Email xác nhận không khớp.")]
        public string ConfirmEmail { get; set; }

        public bool Login()
        {
            UserBDContext context = new UserBDContext();
            var data = context.Users.Where(u => u.UserName.Equals(this.UserName) && u.Password.Equals(this.Password)).ToList();
            if (data.Count() > 0)
            {
                return false;
            }
            else
            {
                return true;
            }
        }

        public bool CheckID()
        {
            UserBDContext context = new UserBDContext();
            var check = context.Users.Where(u => u.UserName.Equals(this.UserName)).ToList();
            if (check.Count() > 0)
            {
                return false;
            }
            else
            {
                return true;
            }
        }

        public bool CheckEmail()
        {
            UserBDContext context = new UserBDContext();
            var check = context.Users.Where(u => u.Email.Equals(this.Email)).ToList();
            if (check.Count > 0)
            {
                return false;
            }
            else
            {
                return true;
            }
        }

        public void Add()
        {
            UserBDContext context = new UserBDContext();
            context.Users.Add(this);
            context.SaveChanges();
        }
    }
}
