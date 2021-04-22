namespace Slantar.Services
{
    public static class Bootstrap
    {
        public static void Init()
        {
            //Provide your services here and define in Core.cs file ((T is the service class))
            //Core.Provide<T>(() => new T(somevariables));

             Core.Provide<IEventService>(() => new EventProvider());


        }
    }
}
