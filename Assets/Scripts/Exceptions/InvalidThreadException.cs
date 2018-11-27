using System;

namespace Assets.Scripts.Exceptions
{
    public sealed class InvalidThreadException : Exception
    {
        public InvalidThreadException()
            : base("An object of type 'AsyncWorker<TCallbackType>' must be called from the main thread.")
        { }

        #region Variables



        #endregion

        #region Properties



        #endregion

        #region Methods



        #endregion
    }
}