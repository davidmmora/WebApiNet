
namespace Exam.Entities
{
    using System;
    using System.Collections.Generic;

    public partial class User
    {
        public int IdUsuario { get; set; }
        public string Nombre { get; set; }
        public string Apellido { get; set; }
        public string Password { get; set; }
        public Nullable<int> UsuarioBloqueado { get; set; }
        public Nullable<int> UserLoginTries { get; set; }
    }
}
