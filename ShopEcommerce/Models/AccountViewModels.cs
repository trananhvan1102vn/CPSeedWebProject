using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

namespace CPSeed.Models {
    public class ExternalLoginConfirmationViewModel {
        [Required(ErrorMessage = "Bạn chưa nhập email.")]
        [Display(Name = "Email")]
        public string Email { get; set; }

        [Display(Name = "Họ tên")]
        public string FullName { get; set; }
        [Display(Name = "Địa chỉ")]
        public string Address { get; set; }
        [Display(Name = "Giới tính")]
        public string Gender { get; set; }

        [Display(Name = "Ngày sinh")]
        [DataType(DataType.Date)]
        [DisplayFormat(DataFormatString = "{0:dd/MM/yyyy}", ApplyFormatInEditMode = true)]
        public DateTime? BirthDate { get; set; }
    }

    public class ExternalLoginListViewModel {
        public string ReturnUrl { get; set; }
    }

    public class SendCodeViewModel {
        public string SelectedProvider { get; set; }
        public ICollection<System.Web.Mvc.SelectListItem> Providers { get; set; }
        public string ReturnUrl { get; set; }
        public bool RememberMe { get; set; }
    }

    public class VerifyCodeViewModel {
        [Required]
        public string Provider { get; set; }

        [Required]
        [Display(Name = "Vui lòng nhập mã đăng nhập vào bên dưới")]
        public string Code { get; set; }
        public string ReturnUrl { get; set; }

        [Display(Name = "Nhớ đăng nhập?")]
        public bool RememberBrowser { get; set; }

        public bool RememberMe { get; set; }
    }

    public class ForgotViewModel {
        [Required]
        [Display(Name = "Email")]
        public string Email { get; set; }
    }

    public class LoginViewModel {
        [Required(ErrorMessage = "Bạn chưa nhập email.")]
        [Display(Name = "Email")]
        [EmailAddress(ErrorMessage = "Email không hợp lệ, vui lòng kiểm tra lại.")]
        public string Email { get; set; }

        [Required(ErrorMessage = "Bạn chưa nhập mật khẩu.")]
        [DataType(DataType.Password)]
        [Display(Name = "Mật khẩu")]
        public string Password { get; set; }

        [Display(Name = "Ghi nhớ đăng nhập?")]
        public bool RememberMe { get; set; }
    }

    public class RegisterViewModel {
        [Required(ErrorMessage = "Bạn chưa nhập email.")]
        [EmailAddress(ErrorMessage = "Email không hợp lệ, vui lòng kiểm tra lại.")]
        [Display(Name = "Email")]
        public string Email { get; set; }

        [Required(ErrorMessage = "Bạn chưa nhập mật khẩu.")]
        [StringLength(100, ErrorMessage = "Mật khẩu phải tối thiểu {2} ký tự.", MinimumLength = 6)]
        [DataType(DataType.Password)]
        [Display(Name = "Mật khẩu")]
        public string Password { get; set; }

        [DataType(DataType.Password)]
        [Display(Name = "Xác nhận lại mật khẩu")]
        [Compare("Password", ErrorMessage = "Mật khẩu xác nhận không khớp.")]
        public string ConfirmPassword { get; set; }

        [Display(Name = "Họ tên")]
        public string FullName { get; set; }
        [Display(Name = "Địa chỉ")]
        public string Address { get; set; }
        [Display(Name = "Giới tính")]
        public string Gender { get; set; }
        [Display(Name = "Ngày sinh")]
        [DataType(DataType.Date)]
        [DisplayFormat(DataFormatString = "{0:dd/MM/yyyy}", ApplyFormatInEditMode = true)]
        public DateTime? BirthDate { get; set; }

        public string RecaptchaERR { get; set; }
    }

    public class ResetPasswordViewModel {
        [Required(ErrorMessage = "Bạn chưa nhập email.")]
        [EmailAddress(ErrorMessage = "Email không hợp lệ, vui lòng kiểm tra lại.")]
        [Display(Name = "Email")]
        public string Email { get; set; }

        [Required(ErrorMessage = "Bạn chưa nhập mật khẩu.")]
        [StringLength(100, ErrorMessage = "Mật khẩu phải tối thiểu {2} ký tự.", MinimumLength = 6)]
        [DataType(DataType.Password)]
        [Display(Name = "Mật khẩu")]
        public string Password { get; set; }

        [DataType(DataType.Password)]
        [Display(Name = "Xác nhận lại mật khẩu")]
        [Compare("Password", ErrorMessage = "Mật khẩu xác nhận không khớp.")]
        public string ConfirmPassword { get; set; }

        public string Code { get; set; }
    }

    public class ForgotPasswordViewModel {
        [Required(ErrorMessage = "Bạn chưa nhập email.")]
        [EmailAddress(ErrorMessage = "Email không hợp lệ, vui lòng kiểm tra lại.")]
        [Display(Name = "Email")]
        public string Email { get; set; }
    }
}
