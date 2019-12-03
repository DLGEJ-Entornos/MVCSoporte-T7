namespace MvcSoporteCF.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class TelefonoFechaEmpleado : DbMigration
    {
        public override void Up()
        {
            AddColumn("dbo.Empleado", "Telefono", c => c.String());
            AddColumn("dbo.Empleado", "FechaNacimiento", c => c.DateTime());
        }
        
        public override void Down()
        {
            DropColumn("dbo.Empleado", "FechaNacimiento");
            DropColumn("dbo.Empleado", "Telefono");
        }
    }
}
