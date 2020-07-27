namespace RDI_Gerenciador_Usuario.Infra.Dados.Migrations
{
    using Microsoft.AspNet.Identity;
    using RDI_Gerenciador_Usuario.Infra.Dados.Contexto;
    using RDI_Gerenciador_Usuario.Infra.Dados.IdentityInfra;
    using System;
    using System.Configuration;
    using System.Data.Entity.Migrations;
    using System.Linq;

    public class Configuration : DbMigrationsConfiguration<IdentityContexto>
    {
        public Configuration()
        {
            AutomaticMigrationsEnabled = true;
            AutomaticMigrationDataLossAllowed = true;
        }

        protected override void Seed(IdentityContexto context)
        {
            var roleManager = GerenciadorIdentity.RoleManager;
            var manager = GerenciadorIdentity.UserManager;

            var IsCreateDataBase = manager.Users.ToList().Count() == 0;

            if (IsCreateDataBase)
            {


                var user = new UsuarioAplicacao()
                {
                    UserName = "admin",
                    Email = ConfigurationManager.AppSettings["EmailUsuarioAdmin"],
                    EmailConfirmed = true,
                    PrimeiroNome = "Administrador",
                    UltimoNome = "Sistema",
                    IdiomaIngles = true,
                    DataCadastro = DateTime.Now.AddYears(-3),
                    LockoutEnabled = false
                };

                manager.Create(user, "@Mudar123");

                if (roleManager.Roles.Count() == 0)
                {
                    roleManager.Create(new PerfilAplicacao
                    {
                        Name = "Administrador Sistema",
                        Ativo = true
                    });
                    roleManager.Create(new PerfilAplicacao
                    {
                        Name = "Administrador",
                        Ativo = true
                    });
                   
                }

                var adminUser = manager.FindByName("admin");

                manager.AddToRoles(adminUser.Id, new string[] { "Administrador Sistema" });
            }
        }

    }
}

