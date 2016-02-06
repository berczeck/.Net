using System;

namespace CustomTryCatch
{
    public class TryCatchWeb
    {
        public static void ControlError<T>(Exception ex, T sender) where T : class
        {
            //ScriptManager
            //Mostrar mensaje error
        }

    }


    //public class TryBase
    //{
    //    protected readonly Action ActionExecute;

    //    public TryBase(Action action)
    //    {
    //        ActionExecute = action;
    //    }

    //}

    //public class TryCatchComun : TryBase
    //{
    //    public TryCatchComun(Action action) : base(action)
    //    {
    //    }
    //    public void Catch<T1>(Action<Exception> handler)
    //        where T1 : Exception
    //    {
    //        try
    //        {
    //            ActionExecute();
    //        }
    //        catch (T1 ex)
    //        {
    //            //ErrorManager.RegistrarError(ex);
    //            handler(ex);
    //        }
    //    }

    //}

    //public class TryCatchWebo : TryBase
    //{
    //    public TryCatchWebo(Action action)
    //        : base(action)
    //    {
    //    }
    //    public void Catch<T1>(Action<Exception,object> handler,object sender)
    //        where T1 : Exception
    //    {
    //        try
    //        {
    //            ActionExecute();
    //        }
    //        catch (T1 ex)
    //        {
    //            //ErrorManager.RegistrarError(ex);
    //            handler(ex,sender);
    //        }
    //    }

    //}

    public static class TryFactory
    {
        public static TryCatch Try(Action action)
        {
            return new TryCatch(action);
        }

        public static TryCatch TryUsing<T>(Func<T> resourceCreator, Action<T> action)
            where T : IDisposable
        {
            return new TryCatch(() =>
            {
                using (var resource = resourceCreator())
                {
                    action(resource);
                }
            });
        }

        public static TryCatch TryUsing<T>(T resource, Action<T> action)
            where T : IDisposable
        {
            return new TryCatch(() =>
            {
                using (resource)
                {
                    action(resource);
                }
            });
        }
    }

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
                //ErrorManager.RegistrarError(ex);
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
                //ErrorManager.RegistrarError(ex);
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
                //ErrorManager.RegistrarError(ex);
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
                //ErrorManager.RegistrarError(ex);
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
                //ErrorManager.RegistrarError(ex);
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
                //ErrorManager.RegistrarError(ex);
                handler(ex, sender);
            }
        }
    }
}
