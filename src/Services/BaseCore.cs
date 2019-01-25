using System;

namespace Slantar.Services
{
    public static partial class Core
    {
        public static bool isReady = false;
        public static Container Container { get { return container; } }
        private static Container container = new Container();

        public static T Get<T>()
        {
            return container.Get<T>();
        }

        public static void Provide<T>(Func<T> constructor)
        {
            container.Provide(constructor);
        }
    }
}
