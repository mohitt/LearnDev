using System.Data.Entity;
using System.Data.Entity.Infrastructure;
using Ads.Model.Entities;

namespace Ads.Model.EFRepositories
{
    public class MyDbInitializer : CreateDatabaseIfNotExists<AdsWebContext>
    {

        protected override void Seed(AdsWebContext context) {
            #region Intialization SQL
            // Here run your custom SQL commands
            string s = System.IO.File.ReadAllText(System.AppDomain.CurrentDomain.RelativeSearchPath+"\\Init.sql");
            context.Database.ExecuteSqlCommand(s);
            #endregion
        }
    }
    
    public static class DbIntializer
    {
        public static void CreateContext() {

            Database.SetInitializer(new MyDbInitializer());

        }
    }
   
    public class AdsWebContext : DbContext
    {

        public AdsWebContext()
            : base("ApplicationServices") {

        }
        // You can add custom code to this file. Changes will not be overwritten.
        // 
        // If you want Entity Framework to drop and regenerate your database
        // automatically whenever you change your model schema, add the following
        // code to the Application_Start method in your Global.asax file.
        // Note: this will destroy and re-create your database with every model change.
        // 
        protected override void OnModelCreating(DbModelBuilder modelBuilder) {
            base.OnModelCreating(modelBuilder);

        }
        private DbSet<User> Users { get; set; }


    }
}