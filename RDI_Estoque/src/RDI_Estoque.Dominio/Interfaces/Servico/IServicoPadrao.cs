using System;
using System.Collections.Generic;
using System.Text;

namespace RDI_Estoque.Dominio.Interfaces.Servico
{
    public interface IServicoPadrao<TEntity> where TEntity : class
    {
        void Adicionar(TEntity obj);
        TEntity BuscarPorID(string id);
        IEnumerable<TEntity> RecuperarTodos();
        void Atualizar(TEntity obj);
        void Excluir(TEntity obj);
        void Dispose();

    }
}
