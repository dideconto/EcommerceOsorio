using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Text;

namespace Domain
{
    [Table("Usuarios")]
    public class Usuario
    {
        public Usuario() { Endereco = new Endereco(); }
        [Key]
        public int UsuarioId { get; set; }
        [Display(Name = "E-mail:")]
        [Required(ErrorMessage = "Campo obrigatório")]
        [EmailAddress]
        public string Email { get; set; }
        [Display(Name = "Senha:")]
        [Required(ErrorMessage = "Campo obrigatório")]
        public string Senha { get; set; }
        [Display(Name = "Confirmação da senha:")]
        [Required(ErrorMessage = "Campo obrigatório")]
        [Compare("Senha", ErrorMessage = "Os campos não coincidem!")]
        [NotMapped]
        public string ConfirmacaoSenha { get; set; }
        public Endereco Endereco { get; set; }
    }
}
