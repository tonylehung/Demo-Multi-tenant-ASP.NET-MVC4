using System;
using System.Collections.Concurrent;
using System.Data.Common;
using System.Data.Entity;
using System.Data.Entity.Infrastructure;
using TenantURL.Models.Entity;
namespace source.Models.Proxy
{
    
    public partial class TenantConnection : DbContext
    {
        private TenantConnection(DbConnection connection, DbCompiledModel model) : base(connection, model, contextOwnsConnection: false) { 
        }
        /// <summary>
        /// Object
        /// </summary>
        public virtual DbSet<News> News { get; set; }

        private static ConcurrentDictionary<Tuple<string, string>, DbCompiledModel> modelCache = new ConcurrentDictionary<Tuple<string, string>, DbCompiledModel>();


        public static TenantConnection Create(string tenantSchema, DbConnection connection)
        {
            var compiledModel = modelCache.GetOrAdd( 
                Tuple.Create(connection.ConnectionString, tenantSchema),
                t => {
                    
                    var builder = new DbModelBuilder();
                    
                    builder.Conventions.Remove<IncludeMetadataConvention>();
                    builder.Entity<News>().ToTable("News", tenantSchema);

                    var model = builder.Build(connection);
                    return model.Compile();
                });

            return new TenantConnection(connection, compiledModel);
        }

        /// <summary>
        /// Creates the database and/or tables for a new tenant
        /// </summary>
        public static void ProvisionTenant(string tenantSchema, DbConnection connection) {
            using (var ctx = Create(tenantSchema, connection)) {
                if (!ctx.Database.Exists()) {
                    ctx.Database.Create();
                }
                //else{
                    //var createScript = ((IObjectContextAdapter)ctx).ObjectContext.CreateDatabaseScript();
                    //if (createScript!="")
                        //ctx.Database.ExecuteSqlCommand(createScript);
                //}
            }
        }
    }
 


}
