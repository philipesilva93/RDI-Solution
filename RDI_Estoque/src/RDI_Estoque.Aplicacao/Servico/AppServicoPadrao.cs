using AutoMapper;
using RDI_Estoque.Aplicacao.Interface;
using RDI_Estoque.Dominio.Interfaces.Servico;
using System;
using System.Collections.Generic;

namespace RDI_Estoque.Aplicacao.Servico
{
    public class AppServicoPadrao<TEntity> : IDisposable, IAppServicoPadrao<TEntity>
           where TEntity : class
    {
        private readonly IServicoPadrao<TEntity> _servicoPadrao;
        protected readonly IMapper _iMapper;
        public AppServicoPadrao(IServicoPadrao<TEntity> servicoPadrao, IMapper iMapper)
        {
            _iMapper = iMapper;
            _servicoPadrao = servicoPadrao;
        }
        public void Adicionar(TEntity obj)
        {
            _servicoPadrao.Adicionar(obj);
        }

        public void Atualizar(TEntity obj)
        {
            _servicoPadrao.Atualizar(obj);
        }

        public TEntity BuscarPorID(int id)
        {
            return _servicoPadrao.BuscarPorID(id.ToString());
        }

        public void Dispose()
        {
            throw new NotImplementedException();
        }

        public void Excluir(TEntity obj)
        {
            _servicoPadrao.Excluir(obj);
        }

        public IEnumerable<TEntity> RecuperarTodos()
        {
            var x = _servicoPadrao.RecuperarTodos();
            return x;
        }
    }
}
