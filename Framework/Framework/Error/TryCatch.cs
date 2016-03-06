using System;

namespace Framework.Error
{
    //https://visualstudio.uservoice.com/forums/121579-visual-studio/suggestions/2140945-i-wish-catch-multiple-exceptions-in-the-same-catch
    //https://github.com/markrendle/FunctionalAlchemy/blob/master/MultiExceptionWpf/TryCatch.cs
    public class TryCatch
    {
        private readonly Action _actionExecute;

        public TryCatch(Action action)
        {
            _actionExecute = action;
        }

        public void Catch(Action<Exception> handler)
        {
            try
            {
                _actionExecute();
            }
            catch (Exception ex)
            {
                ErrorManager.RegistrarError(ex);
                handler(ex);
            }
        }

        public void Catch<T>(Action<Exception, T> handler, T sender)
        {
            try
            {
                _actionExecute();
            }
            catch (Exception ex)
            {
                ErrorManager.RegistrarError(ex);
                handler(ex, sender);
            }
        }

        public void Catch<T1>(Action<Exception> handler)
            where T1 : Exception
        {
            try
            {
                _actionExecute();
            }
            catch (T1 ex)
            {
                ErrorManager.RegistrarError(ex);
                handler(ex);
            }
        }

        public void Catch<T1, T>(Action<Exception, T> handler, T sender)
            where T1 : Exception
            where T : class
        {
            try
            {
                _actionExecute();
            }
            catch (T1 ex)
            {
                ErrorManager.RegistrarError(ex);
                handler(ex, sender);
            }
        }

        public void Catch<T1, T2>(Action<Exception> handler)
            where T1 : Exception
            where T2 : Exception
        {
            try
            {
                Catch<T1>(handler);
            }
            catch (T2 ex)
            {
                ErrorManager.RegistrarError(ex);
                handler(ex);
            }
        }

        public void Catch<T1, T2, T>(Action<Exception, T> handler, T sender)
            where T1 : Exception
            where T2 : Exception
            where T : class
        {
            try
            {
                Catch<T1, T>(handler, sender);
            }
            catch (T2 ex)
            {
                ErrorManager.RegistrarError(ex);
                handler(ex, sender);
            }
        }
    }
}
