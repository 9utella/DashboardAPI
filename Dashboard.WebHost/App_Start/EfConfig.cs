using System;
using System.Collections.Generic;
using System.Data.Entity.Migrations;
using System.Linq;
using System.Web;

namespace Dashboard.WebHost.App_Start
{
    public static class EfConfig
    {
        public static void Initialize()
        {
            RunMigrations();
        }
        private static void RunMigrations()
        {
            var efSetting = new Dashboard.DAL.Migrations.Configuration();
            var efMigrator = new DbMigrator(efSetting);

            efMigrator.Update();
        }
    }
}