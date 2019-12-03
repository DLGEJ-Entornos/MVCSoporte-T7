namespace MvcSoporteCF.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class InitialCreate : DbMigration
    {
        public override void Up()
        {
            CreateTable(
                "dbo.Aviso",
                c => new
                    {
                        Id = c.Int(nullable: false, identity: true),
                        Descripcion = c.String(nullable: false),
                        FechaAviso = c.DateTime(nullable: false),
                        FechaCierre = c.DateTime(),
                        Observaciones = c.String(),
                        EmpleadoId = c.Int(nullable: false),
                        TipoAveriaId = c.Int(nullable: false),
                        EquipoId = c.Int(nullable: false),
                    })
                .PrimaryKey(t => t.Id)
                .ForeignKey("dbo.Empleado", t => t.EmpleadoId)
                .ForeignKey("dbo.Equipo", t => t.EquipoId)
                .ForeignKey("dbo.TipoAveria", t => t.TipoAveriaId)
                .Index(t => t.EmpleadoId)
                .Index(t => t.TipoAveriaId)
                .Index(t => t.EquipoId);
            
            CreateTable(
                "dbo.Empleado",
                c => new
                    {
                        Id = c.Int(nullable: false, identity: true),
                        Nombre = c.String(nullable: false),
                        Email = c.String(),
                    })
                .PrimaryKey(t => t.Id);
            
            CreateTable(
                "dbo.Equipo",
                c => new
                    {
                        Id = c.Int(nullable: false, identity: true),
                        CodigoEquipo = c.String(nullable: false),
                        Marca = c.String(nullable: false),
                        Modelo = c.String(nullable: false),
                        NumeroSerie = c.String(nullable: false),
                        Caracteristicas = c.String(),
                        FechaCompra = c.DateTime(nullable: false),
                        FechaAlta = c.DateTime(),
                        FechaBaja = c.DateTime(),
                        LocalizacionId = c.Int(nullable: false),
                    })
                .PrimaryKey(t => t.Id)
                .ForeignKey("dbo.Localizacion", t => t.LocalizacionId)
                .Index(t => t.LocalizacionId);
            
            CreateTable(
                "dbo.Localizacion",
                c => new
                    {
                        Id = c.Int(nullable: false, identity: true),
                        Descripcion = c.String(nullable: false),
                    })
                .PrimaryKey(t => t.Id);
            
            CreateTable(
                "dbo.TipoAveria",
                c => new
                    {
                        Id = c.Int(nullable: false, identity: true),
                        Descripcion = c.String(nullable: false),
                    })
                .PrimaryKey(t => t.Id);
            
        }
        
        public override void Down()
        {
            DropForeignKey("dbo.Aviso", "TipoAveriaId", "dbo.TipoAveria");
            DropForeignKey("dbo.Equipo", "LocalizacionId", "dbo.Localizacion");
            DropForeignKey("dbo.Aviso", "EquipoId", "dbo.Equipo");
            DropForeignKey("dbo.Aviso", "EmpleadoId", "dbo.Empleado");
            DropIndex("dbo.Equipo", new[] { "LocalizacionId" });
            DropIndex("dbo.Aviso", new[] { "EquipoId" });
            DropIndex("dbo.Aviso", new[] { "TipoAveriaId" });
            DropIndex("dbo.Aviso", new[] { "EmpleadoId" });
            DropTable("dbo.TipoAveria");
            DropTable("dbo.Localizacion");
            DropTable("dbo.Equipo");
            DropTable("dbo.Empleado");
            DropTable("dbo.Aviso");
        }
    }
}
