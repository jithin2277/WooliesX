using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;
using WooliesX.Data.Entities.Trolley;

namespace WooliesX.Data
{
    public interface ITrolleyTotalProcessor : IDisposable
    {
        Task<int> GetTrolleyTotal(TrolleyEntity trolleyEntity);
    }

    public class TrolleyTotalProcessor : ITrolleyTotalProcessor
    {
        public Task<int> GetTrolleyTotal(TrolleyEntity trolleyEntity)
        {
            throw new NotImplementedException();
        }

        #region IDisposable Support
        private bool disposedValue = false;

        protected virtual void Dispose(bool disposing)
        {
            if (!disposedValue)
            {
                if (disposing)
                {
                }

                disposedValue = true;
            }
        }

        // This code added to correctly implement the disposable pattern.
        public void Dispose()
        {
            Dispose(true);
        }

        #endregion

    }
}
