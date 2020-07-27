using RDI_Estoque.Dominio.Interfaces.Repositorio;
using RDI_Estoque.Dominio.Interfaces.Servico;
using System;
using System.Collections.Generic;
using System.Text;

namespace RDI_Estoque.Dominio.Servicos
{
    public class ServicoPadrao<TEntity> : IDisposable, IServicoPadrao<TEntity> where TEntity : class
    {
        private readonly IRepositorioPadrao<TEntity> _repositorio;
        public ServicoPadrao(IRepositorioPadrao<TEntity> repositorio)
        {
            _repositorio = repositorio;
        }

        public void Adicionar(TEntity obj)
        {
            _repositorio.Adicionar(obj);
        }

        public void Atualizar(TEntity obj)
        {
            _repositorio.Atualizar(obj);
        }

        public TEntity BuscarPorID(string id)
        {
            return _repositorio.BuscarPorID(int.Parse(id));
        }

        public void Excluir(TEntity obj)
        {
            _repositorio.Excluir(obj);
        }

        public IEnumerable<TEntity> RecuperarTodos()
        {
            return _repositorio.RecuperarTodos();
        }

        public void Dispose()
        {
            _repositorio.Dispose();
        }

    }
}
