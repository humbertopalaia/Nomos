using System;
using System.Collections.Generic;
using System.Text;

namespace Nomos.Business.Empresa
{
    public interface IEmpresaBusiness
    {
        IList<Entities.Empresa> Filtrar(Entities.Empresa entidade);
        Entities.Empresa Buscar(string cnpj);
        Entities.Empresa Buscar(int id);
        IList<Entities.Empresa> Listar(bool? somenteCliente = null);        
        void Excluir(int id);
        void Atualizar(Entities.Empresa entidade);
        Entities.Empresa Incluir(Entities.Empresa entidade);
        bool VerificarExisteCnpj(string cnpj);
    }
}
