﻿using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Data.Entity.Infrastructure;
using System.Linq;
using System.Transactions;

namespace Soft.Data.Initializers
{
    public class CreateTablesIfNotExist<TContext> : IDatabaseInitializer<TContext> where TContext : DbContext
    {
        private readonly string[] _tablesToValidate;
        private readonly string[] _customCommands;

        public CreateTablesIfNotExist(string[] tablesToValidate, string[] customCommands)
        {
            _tablesToValidate = tablesToValidate;
            _customCommands = customCommands;
        }

        public void InitializeDatabase(TContext context)
        {
            bool dbExists;
            using (new TransactionScope(TransactionScopeOption.Suppress))
            {
                dbExists = context.Database.Exists();
            }
            if (dbExists)
            {
                bool createTables;
                if (_tablesToValidate != null && _tablesToValidate.Length > 0)
                {
                    //Validamos algunos nombres de tablas
                    var existingTableNames = new List<string>(
                        context.Database
                            .SqlQuery<string>(
                                "SELECT table_name FROM INFORMATION_SCHEMA.TABLES WHERE table_type = 'BASE TABLE'"));

                    createTables =
                        !existingTableNames.Intersect(_tablesToValidate, StringComparer.InvariantCultureIgnoreCase)
                            .Any();
                }
                else
                {

                    //Verificamos que tablas estan listas para crear
                    var numberOfTables = 0;
                    foreach (var t1 in context.Database.SqlQuery<int>("SELECT COUNT(*) FROM INFORMATION_SCHEMA.TABLES WHERE table_type = 'BASE TABLE' "))
                        numberOfTables = t1;
                    createTables = numberOfTables == 0;
                }

                if (createTables)
                {
                    //Creamos todas las tablas 
                    var dbCreationScript = ((IObjectContextAdapter)context).ObjectContext.CreateDatabaseScript();
                    context.Database.ExecuteSqlCommand(dbCreationScript);

                    //Seed(context);
                    context.SaveChanges();

                    if (_customCommands == null || _customCommands.Length <= 0) 
                        return;

                    foreach (var command in _customCommands)
                        context.Database.ExecuteSqlCommand(command);
                }
            
            }
            else
            {
                throw new ApplicationException("No hay instancia de base de datos");
            }
        }
    }
}