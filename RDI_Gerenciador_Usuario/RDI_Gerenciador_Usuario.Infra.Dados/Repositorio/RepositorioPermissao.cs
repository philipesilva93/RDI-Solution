using RDI_Gerenciador_Usuario.Infra.Dados.IdentityInfra;
using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Diagnostics;
using System.Linq;

namespace RDI_Gerenciador_Usuario.Infra.Dados.Repositorio
{
    [DebuggerStepThrough]
    public class RepositorioPermissao : RepositorioPadrao<PermissaoAplicacao>
    {
        public void VincularPermissaoPerfil(PerfilAplicacao parametros, IEnumerable<PermissaoAplicacao> permissaos)
        {
            try
            {
                _Db.Roles.Attach(parametros);

                foreach (var item in permissaos)
                {
                    var permOrig = _Db.Permissoes.Find(item.Id);
                    if (!permOrig.Perfis.Any(x=>x.Id == parametros.Id))
                    {
                        permOrig.Perfis.Add(parametros);
                    }
                }
                _Db.Entry(parametros).State = EntityState.Unchanged;
                _Db.SaveChanges();
                
            }
            catch (Exception ex)
            {
                throw new Exception("RepositorioCompra.AtualizaFiltrosCadastroProduto: \n" + ex.Message, ex);
            }
        }
        public void DesvincularPermissaoPerfil(PerfilAplicacao parametros)
        {
            try
            {
                var permOrig = RecuperarTodos().Where(y=> y.Perfis.Any(x => x.Id == parametros.Id));

                foreach (var item in permOrig)
                {
                        item.Perfis.Remove(parametros);
                }
                _Db.SaveChanges();

            }
            catch (Exception ex)
            {
                throw new Exception("RepositorioCompra.AtualizaFiltrosCadastroProduto: \n" + ex.Message, ex);
            }
        }

    }
}
