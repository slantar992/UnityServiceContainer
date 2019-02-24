using System;

namespace Slantar.Services
{
    public static partial class Core
    {
        public static bool IsReady { get { return isReady; } }
        private static bool isReady = false;
        public static Container Container { get { return container; } }
        private static Container container = new Container();

        public static T Get<T>()
        {
            if (!isReady)
            {
                Bootstrap.Init();
                isReady = true;
            }

            return container.Get<T>();
        }

        public static void Provide<T>(Func<T> constructor)
        {
            container.Provide(constructor);
        }
    }
}
