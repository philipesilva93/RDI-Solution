using Microsoft.EntityFrameworkCore;
using RDI_Estoque.Dados.Contexto;
using RDI_Estoque.Dominio.Interfaces.Repositorio;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace RDI_Estoque.Dados.Repositorio
{
    public class RepositorioPadrao<TEntity> : IDisposable, IRepositorioPadrao<TEntity> where TEntity : class
    {
        protected readonly AppContexto _Db;
        public RepositorioPadrao(AppContexto Db)
        {
            _Db = Db;
        }
        public void Adicionar(TEntity obj)
        {
            _Db.Set<TEntity>().Add(obj);
            _Db.SaveChanges();
        }

        public void Atualizar(TEntity obj)
        {
            _Db.Entry(obj).State = EntityState.Modified;
            _Db.SaveChanges();
        }

        public TEntity BuscarPorID(int id)
        {
            return _Db.Set<TEntity>().Find(id);
        }

        public void Excluir(TEntity obj)
        {
            _Db.Set<TEntity>().Remove(obj);
            _Db.SaveChanges();
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
    }
}
