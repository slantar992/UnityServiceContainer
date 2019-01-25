

namespace Slantar.Services
{

    public static partial class Core {

        /* Add your services like this (T is de service class)
         * private static T name;
         * public static T Name {
         *     get
         *     {
         *         return Cache<T>(ref name);
         *     }
         * }
        */

        private static IEventService events;
        public static IEventService Events
        {
            get
            {
                return Cache(ref events);
            }
        }
        

        private static T Cache<T>(ref T cachedVariable)
        {
            if (cachedVariable == null) { cachedVariable = Core.Get<T>(); }
            return cachedVariable;
        }
    }
}
