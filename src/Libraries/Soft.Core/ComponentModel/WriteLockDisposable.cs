using System;
using System.Threading;

namespace Soft.Core.ComponentModel
{
    /// <summary>
    /// Provee de una metodologia para implementar bloqueo para accesos a los recursos 
    /// </summary>
    public class WriteLockDisposable : IDisposable
    {
        private readonly ReaderWriterLockSlim _rwLock;

        /// <summary>
        /// Inicializa una nueva instancia
        /// </summary>
        /// <param name="rwLock"> el rw lock</param>
        public WriteLockDisposable(ReaderWriterLockSlim rwLock)
        {
            _rwLock = rwLock;
            _rwLock.EnterWriteLock();
        }

        public void Dispose()
        {
            _rwLock.ExitWriteLock();
        }
    }
}