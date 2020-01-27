using Prit.Domain.Aggregates;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace Prit.Portal.Models.Products
{
    public class CreateOrUpdateProductViewModel
    {
        public string Id { get; set; }
        [Required(ErrorMessage = "O nome é obrigatório")]
        [MaxLength(50, ErrorMessage = "Tamanho máximo de 50 caracteres")]
        [MinLength(1, ErrorMessage = "Tamanho mínimo de 1 caracter")]
        public string Name { get; set; }
        [Required(ErrorMessage = "A descrição é obrigatória")]
        [MaxLength(150, ErrorMessage = "Tamanho máximo de 150 caracteres")]
        [MinLength(1, ErrorMessage = "Tamanho mínimo de 1 caracter")]
        public string Description { get; set; }
        [Required(ErrorMessage = "O valor é obrigatório")]
        public string Price { get; set; }

        public Product AsProduct()
        {
            var id = 0;
            var price = 0m;

            int.TryParse(Id, out id);
            decimal.TryParse(Price, out price);

            return new Product
            {
                Description = Description,
                Name = Name,
                Id = id,
                Price = price
            };
        }
    }
}
