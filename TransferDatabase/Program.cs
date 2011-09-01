using System;
using System.Collections.Generic;
using System.Configuration;
using System.Data.SqlClient;
using System.IO;
using System.Linq;
using System.Text;
using Microsoft.SqlServer.Management.Smo;
using Microsoft.SqlServer.Management.Common;

namespace TransferDatabase
{
    class Program
    {
        static void Main(string[] args) {

            //Source Database 
            ;

            // Connect to Server
            var fileName = "TrnasferDB_" + DateTime.Now.ToString("yyyyMMddHHmmss") + ".log";
            using (var logFile = File.CreateText(fileName)) {
                try {

                    var sourceDb = (new Server(new ServerConnection(
                                                   new SqlConnection(
                                                       ConfigurationManager.ConnectionStrings["Source"].ToString()
                                                       )
                                                       )
                                                       )).Databases[ConfigurationManager.AppSettings["SourceDB"]];

                   

                    // Setup transfer
                    // I want to copy all objects
                    // both Data and Schema
                    var dropScript = new Transfer(sourceDb) {
                        CopyAllObjects = true,
                        DropDestinationObjectsFirst = true,
                        CopySchema = true,
                        CopyData = false,
                        Options = { IncludeIfNotExists = true,ScriptDrops = true,ScriptSchema = true,ContinueScriptingOnError = true}
                    };
                    //t.DestinationDatabase = destinationDb.Name;
                    //t.DestinationLogin = destinationDb.UserName;
                    //t.DestinationPassword=destinationDb.
                    //t.DestinationPassword = "iA3vGrFYfF7H3ZUu3Tf4fjMciWdQx6UEY5eLvJszANgWMCxFmzpwq2j8WYnSnXzi";
                    StringBuilder dropScriptSb = new StringBuilder();

                    dropScript.EnumScriptTransfer().ToList().ForEach(k => {
                        Console.WriteLine(k);
                        logFile.WriteLine(k);
                        dropScriptSb.Append(k);
                    });
                    //
                    //destinationDb.ExecuteNonQuery(dropScriptSb.ToString());

                    var createScript = new Transfer(sourceDb) {
                        CopyAllObjects = true,
                        DropDestinationObjectsFirst = true,
                        CopySchema = true,
                        CopyData = false,
                        
                        Options = { IncludeIfNotExists = true,TargetServerVersion = SqlServerVersion.Version105}
                    };
                    //t.DestinationDatabase = destinationDb.Name;
                    //t.DestinationLogin = destinationDb.UserName;
                    //t.DestinationPassword=destinationDb.
                    //t.DestinationPassword = "iA3vGrFYfF7H3ZUu3Tf4fjMciWdQx6UEY5eLvJszANgWMCxFmzpwq2j8WYnSnXzi";
                    StringBuilder createScriptSb = new StringBuilder();

                    createScript.EnumScriptTransfer().ToList().ForEach(k => {
                        Console.WriteLine(k);
                        logFile.WriteLine(k);
                        createScriptSb.Append(k);
                    });
                    //
                    


                    var initScript = File.ReadAllText(ConfigurationManager.AppSettings["IntializationFile"]);
                    Console.WriteLine(initScript);
                    logFile.WriteLine(initScript);
                    //destinationDb.ExecuteNonQuery(File.ReadAllText(ConfigurationManager.AppSettings["IntializationFile"]));
                }
                finally {
                    logFile.Close();
                    logFile.Dispose();
                }
                try
                {
                    var readFile = File.ReadAllText(fileName);
                    var destinationDb = (new Server(new ServerConnection(
                                                  new SqlConnection(
                                                      ConfigurationManager.ConnectionStrings["Destination"].ToString()
                                                      )
                                                      )
                                                      )).Databases[ConfigurationManager.AppSettings["DestinationDB"]];

                    destinationDb.ExecuteNonQuery(readFile);
                }
                catch (Exception)
                {
                    
                    throw;
                }
                
            }

            //string sqlConnectionString =
            //    "Data Source=(local);Initial Catalog=AdventureWorks;Integrated Security=True";
            //FileInfo file = new FileInfo("C:\\myscript.sql");
            //string script = file.OpenText().ReadToEnd();
            //SqlConnection conn = new SqlConnection(sqlConnectionString);
            //Server server = new Server(new ServerConnection(conn));
            //server.ConnectionContext.ExecuteNonQuery(script);
            //// Transfer Schema and Data
            //t.TransferData();



            // Kill It
            
        }
    }
}
