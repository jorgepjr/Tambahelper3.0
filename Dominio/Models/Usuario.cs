using System;
using System.ComponentModel.DataAnnotations;
using System.Security.Cryptography;
using System.Text;
using TiaIdentity;

namespace Dominio.Models
{
    public class Usuario : IUsuario
    {
        public int Id { get; private set; }
        public Usuario()
        {

        }
        public Usuario(Tecnico tecnico, string email, string perfil)
        {
            GerarNovaHash();
            Nome = tecnico.Nome;
            Email = email;
            Perfil = perfil;
        }

        public string Login { get => Nome; }

        [Required, MaxLength(50)]
        public string Nome { get; private set; }

        [Required, MaxLength(36)]
        public string Senha { get; private set; } = Guid.NewGuid().ToString();

        [Required, MaxLength(50), EmailAddress]
        public string Email { get; private set; }

        public string Hash { get; private set; }

        public bool HashUtilizado { get; private set; }

        public string Perfil { get; private set; }

        public void AlterarSenha(string novaSenha) => this.Senha = CriptografarSenha(novaSenha);

        public void UtilizarHash() => this.HashUtilizado = true;

        public void GerarNovaHash()
        {
            this.Hash = Guid.NewGuid().ToString();
            this.HashUtilizado = false;
        }


        public void Atualizar(string nome, string email, string perfil)
        {
            this.Nome = nome;
            this.Email = email;
            this.Perfil = perfil;
        }

        public bool SenhaCorreta(string senhaDigitada)
        {
            var senhaDigitadaCriptografada = CriptografarSenha(senhaDigitada);
            return (this.Senha == senhaDigitadaCriptografada);
        }

        private string CriptografarSenha(string txt)
        {
            var algoritmo = SHA512.Create();
            var senhaEmBytes = Encoding.UTF8.GetBytes(txt);
            var senhaCifrada = algoritmo.ComputeHash(senhaEmBytes);

            var sb = new StringBuilder();

            foreach (var caractere in senhaCifrada)
                sb.Append(caractere.ToString("X2"));

            return sb.ToString();
        }
    }
}