using System;

namespace Framework.Error
{
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
}
