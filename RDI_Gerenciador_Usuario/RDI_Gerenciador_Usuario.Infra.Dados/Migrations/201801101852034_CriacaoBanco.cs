namespace RDI_Gerenciador_Usuario.Infra.Dados.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class CriacaoBanco : DbMigration
    {
        public override void Up()
        {
            CreateTable(
                "dbo.Permissoes",
                c => new
                    {
                        Id = c.Guid(nullable: false),
                        Descricao = c.String(nullable: false, maxLength: 50, unicode: false),
                    })
                .PrimaryKey(t => t.Id);
            
            CreateTable(
                "dbo.Perfil",
                c => new
                    {
                        ID = c.String(nullable: false, maxLength: 128, unicode: false),
                        Ativo = c.Boolean(nullable: false),
                        Descricao = c.String(nullable: false, maxLength: 256, unicode: false),
                    })
                .PrimaryKey(t => t.ID)
                .Index(t => t.Descricao, unique: true, name: "RoleNameIndex");
            
            CreateTable(
                "dbo.UsuarioPerfil",
                c => new
                    {
                        UsuarioID = c.String(nullable: false, maxLength: 128, unicode: false),
                        PerfilID = c.String(nullable: false, maxLength: 128, unicode: false),
                    })
                .PrimaryKey(t => new { t.UsuarioID, t.PerfilID })
                .ForeignKey("dbo.Perfil", t => t.PerfilID, cascadeDelete: true)
                .ForeignKey("dbo.Usuario", t => t.UsuarioID, cascadeDelete: true)
                .Index(t => t.UsuarioID)
                .Index(t => t.PerfilID);
            
            CreateTable(
                "dbo.Usuario",
                c => new
                    {
                        ID = c.String(nullable: false, maxLength: 128, unicode: false),
                        Nome = c.String(nullable: false, maxLength: 50, unicode: false),
                        Sobrenome = c.String(nullable: false, maxLength: 100, unicode: false),
                        IdiomaIngles = c.Boolean(nullable: false),
                        DataCadastro = c.DateTime(nullable: false),
                        TelefoneContato = c.String(maxLength: 20, unicode: false),
                        Email = c.String(nullable: false, maxLength: 256, unicode: false),
                        EmailConfirmado = c.Boolean(nullable: false),
                        SenhaHash = c.String(unicode: false),
                        SeloSeguranca = c.String(unicode: false),
                        Celular = c.String(maxLength: 20, unicode: false),
                        CelularConfirmado = c.Boolean(nullable: false),
                        FatorDuplo = c.Boolean(nullable: false),
                        BloqueioData = c.DateTime(),
                        BloqueioAtivo = c.Boolean(nullable: false),
                        LimiteTentativasAcessoInvalido = c.Int(nullable: false),
                        NomeLogin = c.String(nullable: false, maxLength: 256, unicode: false),
                    })
                .PrimaryKey(t => t.ID)
                .Index(t => t.NomeLogin, unique: true, name: "UserNameIndex");
            
            CreateTable(
                "dbo.PermissaoUsuario",
                c => new
                    {
                        ID = c.Int(nullable: false, identity: true),
                        UsuarioID = c.String(nullable: false, maxLength: 128, unicode: false),
                        Permissao = c.String(nullable: false, maxLength: 50, unicode: false),
                        Permitir = c.String(nullable: false, maxLength: 1, unicode: false),
                    })
                .PrimaryKey(t => t.ID)
                .ForeignKey("dbo.Usuario", t => t.UsuarioID, cascadeDelete: true)
                .Index(t => t.UsuarioID);
            
            CreateTable(
                "dbo.Login",
                c => new
                    {
                        ProvedorLogin = c.String(nullable: false, maxLength: 128, unicode: false),
                        ProvedorChave = c.String(nullable: false, maxLength: 128, unicode: false),
                        UsuarioID = c.String(nullable: false, maxLength: 128, unicode: false),
                    })
                .PrimaryKey(t => new { t.ProvedorLogin, t.ProvedorChave, t.UsuarioID })
                .ForeignKey("dbo.Usuario", t => t.UsuarioID, cascadeDelete: true)
                .Index(t => t.UsuarioID);
            
            CreateTable(
                "dbo.PermissaoPerfil",
                c => new
                    {
                        PerfilID = c.String(nullable: false, maxLength: 128, unicode: false),
                        PermissaoID = c.Guid(nullable: false),
                    })
                .PrimaryKey(t => new { t.PerfilID, t.PermissaoID })
                .ForeignKey("dbo.Perfil", t => t.PerfilID, cascadeDelete: true)
                .ForeignKey("dbo.Permissoes", t => t.PermissaoID, cascadeDelete: true)
                .Index(t => t.PerfilID)
                .Index(t => t.PermissaoID);
            
        }
        
        public override void Down()
        {
            DropForeignKey("dbo.UsuarioPerfil", "UsuarioID", "dbo.Usuario");
            DropForeignKey("dbo.Login", "UsuarioID", "dbo.Usuario");
            DropForeignKey("dbo.PermissaoUsuario", "UsuarioID", "dbo.Usuario");
            DropForeignKey("dbo.UsuarioPerfil", "PerfilID", "dbo.Perfil");
            DropForeignKey("dbo.PermissaoPerfil", "PermissaoID", "dbo.Permissoes");
            DropForeignKey("dbo.PermissaoPerfil", "PerfilID", "dbo.Perfil");
            DropIndex("dbo.PermissaoPerfil", new[] { "PermissaoID" });
            DropIndex("dbo.PermissaoPerfil", new[] { "PerfilID" });
            DropIndex("dbo.Login", new[] { "UsuarioID" });
            DropIndex("dbo.PermissaoUsuario", new[] { "UsuarioID" });
            DropIndex("dbo.Usuario", "UserNameIndex");
            DropIndex("dbo.UsuarioPerfil", new[] { "PerfilID" });
            DropIndex("dbo.UsuarioPerfil", new[] { "UsuarioID" });
            DropIndex("dbo.Perfil", "RoleNameIndex");
            DropTable("dbo.PermissaoPerfil");
            DropTable("dbo.Login");
            DropTable("dbo.PermissaoUsuario");
            DropTable("dbo.Usuario");
            DropTable("dbo.UsuarioPerfil");
            DropTable("dbo.Perfil");
            DropTable("dbo.Permissoes");
        }
    }
}
