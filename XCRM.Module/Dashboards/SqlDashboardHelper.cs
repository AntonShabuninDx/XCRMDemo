using System;
using System.Collections.Generic;
using System.Data.Entity.Core.EntityClient;
using System.Data.SqlClient;
using System.Linq;
using System.Text;
using DevExpress.DataAccess.ConnectionParameters;
using DevExpress.ExpressApp;
using DevExpress.ExpressApp.EF;

namespace XCRM.Module.Dashboards {
    public static class SqlDashboardHelper {
        public static void SetupSqlParameters(IObjectSpace objectSpace, MsSqlConnectionParameters connectionParameters) {
            EFObjectSpace efObjectSpace = objectSpace as EFObjectSpace;
            if(efObjectSpace != null) {
                EntityConnection connection = efObjectSpace.ObjectContext.Connection as EntityConnection;
                if(connection != null && connectionParameters != null) {
                    SqlConnectionStringBuilder builder = new SqlConnectionStringBuilder(connection.StoreConnection.ConnectionString);
                    connectionParameters.ServerName = connection.DataSource;
                    connectionParameters.DatabaseName = builder.InitialCatalog;
                    connectionParameters.AuthorizationType = MsSqlAuthorizationType.Windows;
                    if(builder.IntegratedSecurity == false) {
                        connectionParameters.AuthorizationType = MsSqlAuthorizationType.SqlServer;
                        connectionParameters.UserName = builder.UserID;
                        connectionParameters.Password = builder.Password;
                    }
                }
            }
        }
    }
}
