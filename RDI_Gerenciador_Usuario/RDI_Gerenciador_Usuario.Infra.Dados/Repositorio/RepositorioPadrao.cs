using RDI_Gerenciador_Usuario.Infra.Dados.Contexto;
using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;

namespace RDI_Gerenciador_Usuario.Infra.Dados.Repositorio
{
    public class RepositorioPadrao<TEntity> : IDisposable where TEntity : class
    {
        protected IdentityContexto _Db = new IdentityContexto();
        public void Adicionar(TEntity obj)
        {
            _Db.Set<TEntity>().Add(obj);
        }

        public void Atualizar(TEntity obj)
        {
            _Db.Entry(obj).State = EntityState.Modified;
        }

        public TEntity BuscarPorID(int id)
        {
            return _Db.Set<TEntity>().Find(id);
        }

        public void Excluir(TEntity obj)
        {
            _Db.Set<TEntity>().Remove(obj);
        }

        public IEnumerable<TEntity> RecuperarTodos()
        { 
            return _Db.Set<TEntity>().ToList();
        }

        public void Dispose()
        {
            _Db.Dispose();
            GC.SuppressFinalize(this);
        }

        public int SalvarAlteracoes()
        {
            return _Db.SaveChanges();
        }
    }
}
