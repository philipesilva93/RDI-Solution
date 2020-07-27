using System;
using System.Collections.Generic;
using System.Text;

namespace RDI_Estoque.Dominio.Interfaces.Repositorio
{
    public interface IRepositorioPadrao<TEntity> : IDisposable
    {
        void Adicionar(TEntity obj);
        TEntity BuscarPorID(int id);
        IEnumerable<TEntity> RecuperarTodos();
        void Atualizar(TEntity obj);
        void Excluir(TEntity obj);

    }
}
