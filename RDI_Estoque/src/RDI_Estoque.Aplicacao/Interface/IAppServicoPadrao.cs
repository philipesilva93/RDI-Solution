using System.Collections.Generic;

namespace RDI_Estoque.Aplicacao.Interface
{
    public interface IAppServicoPadrao<TEntity, TEntityVM>
        where TEntity : class
        where TEntityVM : class
    {
        void Adicionar(TEntityVM obj);
        TEntityVM BuscarPorID(int id);
        IEnumerable<TEntityVM> RecuperarTodos();
        void Atualizar(TEntityVM obj);
        void Excluir(TEntityVM obj);
        void Dispose();
    }
}
