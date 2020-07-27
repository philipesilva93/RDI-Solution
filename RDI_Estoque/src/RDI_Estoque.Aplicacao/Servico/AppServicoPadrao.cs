using AutoMapper;
using RDI_Estoque.Aplicacao.Interface;
using RDI_Estoque.Dominio.Interfaces.Servico;
using System;
using System.Collections.Generic;

namespace RDI_Estoque.Aplicacao.Servico
{
    public class AppServicoPadrao<TEntity, TEntityVM> : IDisposable, IAppServicoPadrao<TEntity, TEntityVM>
           where TEntity : class
           where TEntityVM : class
    {
        private readonly IServicoPadrao<TEntity> _servicoPadrao;
        protected readonly IMapper _iMapper;
        public AppServicoPadrao(IServicoPadrao<TEntity> servicoPadrao, IMapper iMapper)
        {
            _iMapper = iMapper;
            _servicoPadrao = servicoPadrao;
        }
        public void Adicionar(TEntityVM obj)
        {
            _servicoPadrao.Adicionar(_iMapper.Map<TEntity>(obj));
        }

        public void Atualizar(TEntityVM obj)
        {
            _servicoPadrao.Atualizar(_iMapper.Map<TEntity>(obj));
        }

        public TEntityVM BuscarPorID(int id)
        {
            return _iMapper.Map<TEntityVM>(_servicoPadrao.BuscarPorID(id.ToString()));
        }

        public void Dispose()
        {
            throw new NotImplementedException();
        }

        public void Excluir(TEntityVM obj)
        {
            _servicoPadrao.Excluir(_iMapper.Map<TEntity>(obj));
        }

        public IEnumerable<TEntityVM> RecuperarTodos()
        {
            var x = _iMapper.Map<IEnumerable<TEntityVM>>(_servicoPadrao.RecuperarTodos());
            return x;
        }
    }
}
