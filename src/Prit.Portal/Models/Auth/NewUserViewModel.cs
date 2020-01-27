using DataAnnotationsExtensions;
using Prit.Portal.Identity;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace Prit.Portal.Models
{
    public class NewUserViewModel
    {
        [Required(ErrorMessage = "O nome é obrigatório")]
        [MaxLength(50, ErrorMessage = "Tamanho máximo de 50 caracteres")]
        [MinLength(5, ErrorMessage = "Tamanho mínimo de 5 caracteres")]
        public string Name { get; set; }
        [Email(ErrorMessage = "Email inválido")]
        [Required(ErrorMessage = "O email é obrigatório")]
        public string Email { get; set; }
        [Required(ErrorMessage = "A senha é obrigatória")]
        [MaxLength(20, ErrorMessage = "Tamanho máximo de 20 caracteres")]
        [MinLength(6, ErrorMessage = "Tamanho mínimo de 6 caracteres")]
        public string Password { get; set; }

        public PritPortalUser AsPritPortalUser()
        {
            return new PritPortalUser
            {
                UserName = Email,
                Email = Email,
                Name = Name
            };
        }
    }
}
