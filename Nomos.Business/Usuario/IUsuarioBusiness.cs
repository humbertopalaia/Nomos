using System;
using System.Collections.Generic;
using System.Text;

namespace Nomos.Business.Usuario
{
    public interface IUsuarioBusiness
    {
        Entities.Usuario FiltrarLogin(string login);
    }
}
