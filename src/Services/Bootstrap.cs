using UnityEngine;

namespace Slantar.Services
{
    public class Bootstrap : MonoBehaviour
    {

        private void Awake()
        {
            if (!Core.isReady)
            {
                //Provide your services here and define in Core.cs file ((T is de service class))
                //Core.Provide<T>(() => new T(somevariables));

                Core.isReady = true;
            }
        }
    }
}
