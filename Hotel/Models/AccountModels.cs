using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Globalization;
using System.Web.Mvc;
using System.Web.Security;

namespace Hotel.Models
{

    public class ChangePasswordModel
    {
        [Required]
        [DataType(DataType.Password)]
        [Display(Name = "Contraseña actual")]
        public string OldPassword { get; set; }

        [Required]
        [StringLength(100, ErrorMessage = "The {0} must be at least {2} characters long.", MinimumLength = 6)]
        [DataType(DataType.Password)]
        [Display(Name = "Nueva Contraseña")]
        public string NewPassword { get; set; }

        [DataType(DataType.Password)]
        [Display(Name = "Confirmar Contraseña")]
        [Compare("NewPassword", ErrorMessage = "The new password and confirmation password do not match.")]
        public string ConfirmPassword { get; set; }
    }

    public class LogOnModel
    {
        [Required]
        [Display(Name = "Usuario")]
        public string UserName { get; set; }

        [Required]
        [DataType(DataType.Password)]
        [Display(Name = "Contraseña")]
        public string Password { get; set; }

        [Display(Name = "Recordarme?")]
        public bool RememberMe { get; set; }
    }

    public class RegisterModel
    {
        [Required(ErrorMessage = "El campo de 'Usuario' es de caracter obligatorio")]
        [Display(Name = "Usuario")]
        [MaxLength(50)]
        public string UserName { get; set; }

        [Required(ErrorMessage = "El campo de 'Rol' es de caracter obligatorio")]
        [Display(Name = "Role")]
        public string Role { get; set; }

        [Required(ErrorMessage = "El campo de 'Direccion Email' es de caracter obligatorio")]
        [DataType(DataType.EmailAddress)]
        [Display(Name = "Direccion Email")]
        public string Email { get; set; }

        [Required(ErrorMessage = "El campo de 'Telefono Personal' es de caracter obligatorio")]
        [Display(Name = "Telefono Persona")]
        public string Tel_personal { get; set; }

        [Required(ErrorMessage="El campo de 'Nombres y Apellidos' es de caracter obligatorio")]
        [Display(Name = "Nombres y Apellidos")]
        public string Nombres { get; set; }

        [Required(ErrorMessage = "El campo de 'Contraseña' es de caracter obligatorio")]
        [StringLength(25, ErrorMessage = "El {0} debe tener como minimo {2} characteres de largo", MinimumLength = 6)]
        [DataType(DataType.Password)]
        [Display(Name = "Contraseña")]
        public string Password { get; set; }

        [DataType(DataType.Password)]
        [Display(Name = "Confirmar Contraseña")]
        [Compare("Password", ErrorMessage = "La contraseña y Confirmacion de contraseña no son Inguales")]
        public string ConfirmPassword { get; set; }
    }
}
