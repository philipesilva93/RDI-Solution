using RDI_Gerenciador_Usuario.Dominio.Entidades;
using RDI_Gerenciador_Usuario.Infra.Dados.Utils;
using System;
using System.Collections;
using System.Collections.Generic;
using System.Diagnostics;

namespace RDI_Gerenciador_Usuario.Infra.Dados.Repositorio
{
    [DebuggerStepThrough]
    public class RepositorioExecProc : RepositorioPadrao<UsuarioNextt>
    {
        public List<IEnumerable> RetornaUsuarioNextt()
        {
            try
            {
                return _Db.MultiplosResults("[dbo].[pr_consulta_usuario_nextt]")
                                   .With<UsuarioNextt>()
                                   .Executar();

            }
            catch (Exception ex)
            {
                throw new Exception("RepositorioCompra.RetornaCargaEspeciesFiltros: \n" + ex.Message, ex);
            }
        }
    }
}
