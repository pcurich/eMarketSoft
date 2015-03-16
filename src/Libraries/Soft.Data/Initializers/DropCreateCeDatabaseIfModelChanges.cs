using System;
using System.Data.Entity;
using System.Transactions;

namespace Soft.Data.Initializers
{
    /// <summary>
    ///     Una implementacion de IDatabaseInitializer que <b>Borra</b>, <b>recrea</b> y opcionalmente
    ///     poblara la base de datos solo si el modelo ha cambiado desde la ultima vez que la base fue creada.
    ///     Este es archivado escribiendo un hash del modelo almacenado en la base de datos cuando esta fue creada y entonces
    ///     se compraran el hash con el generado por el modelo actual
    ///     Para poblar la base de datos, cree una deribada y sobreescriba el metodo seed
    /// </summary>
    public class DropCreateCeDatabaseIfModelChanges<TContext> : SqlCeInitializer<TContext> where TContext : DbContext
    {
        #region Strategy implementation

        /// <summary>
        ///     Ejecuta la estrategia para inicializar la base de datos para el contexto dado
        /// </summary>
        /// <param name="context">El contexto</param>
        public override void InitializeDatabase(TContext context)
        {
            if (context == null)
                throw new ArgumentNullException("context");

            var replacedContext = ReplaceSqlCeConnection(context);

            bool databaseExists;
            using (new TransactionScope(TransactionScopeOption.Suppress))
                databaseExists = replacedContext.Database.Exists();

            if (databaseExists)
            {
                if (context.Database.CompatibleWithModel(true))
                    return;
                replacedContext.Database.Delete();
            }

            // Database no existe o fue borrada, entonces se recreara otra vez.
            context.Database.Create();
            Seed(context);
            context.SaveChanges();
        }

        #endregion

        #region Seeding 

        /// <summary>
        ///     Esto deberia ser sobreescrito para apregar la data en el contexto para
        ///     ser insertado
        /// </summary>
        /// <param name="context">El contexto a poblar</param>
        protected virtual void Seed(TContext context)
        {
        }

        #endregion
    }
}