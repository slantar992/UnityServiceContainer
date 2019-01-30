using UnityEngine;

namespace Slantar.Services
{
    public class Bootstrap : MonoBehaviour
    {

        private void Awake()
        {
            if (!Core.isReady)
            {
                //Provide your services here and define it in Core.cs file ((T is de service class))
                //Core.Provide<T>(() => new T(somevariables));
                
                Core.Provide<IEventService>(() => new EventProvider());

                Core.isReady = true;
                DontDestroyOnLoad(gameObject);
            }
            else
            {
                Destroy(gameObject);
            }
        }
    }
}
