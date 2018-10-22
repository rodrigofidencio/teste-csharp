using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace fundagMVC.Classes.DTO
{
    public class LoginDTO
    {

        [Display(Name = "Login")]
        [Required(ErrorMessage = "Informe o nome do usuário", AllowEmptyStrings = false)]
        public string UserName { get; set; }

        [Display(Name = "Senha")]
        [Required(ErrorMessage = "Informe a senha usuário", AllowEmptyStrings = false)]
        public string Senha { get; set; }      
    }
}