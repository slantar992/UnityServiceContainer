using System;
using System.Collections.Generic;
using UnityEngine;

namespace Slantar.Services
{

    public partial class Container
    {
        
        private Dictionary<Type, InstanceContainer> instanceContainer = null;

        public Container()
        {
            instanceContainer = new Dictionary<Type, InstanceContainer>();
        }

        public T Get<T>()
        {
            return (T)(Get(typeof(T)));
        }

        public void Provide<T>(Func<T> constructor)
        {
            Provide(typeof(T), () =>
            {
                return constructor();
            });
        }

        private object Get(Type type)
        {

            if(instanceContainer.ContainsKey(type))
            {
                try
                {
                    return instanceContainer[type].Instance;
                }
                catch (Exception e)
                {
                    throw new Exception(string.Format("Error creating an instance of type '{0}' \n {1}", type.ToString(), e.Message));
                }
            }
            else
            {
                if (UnityEngine.SceneManagement.SceneManager.GetActiveScene().buildIndex != 0)
                {
                    Debug.Log(string.Format("Service {0} missing or not initialized, loading first scene.", type));
                }
                else
                {
                    throw new NullReferenceException(string.Format("No class of type '{0}' found", type.ToString()));
                }
            }

            return null;
        }

        private void Provide(Type type, Func<object> constructor)
        {
            if (!instanceContainer.ContainsKey(type))
            {
                instanceContainer.Add(type, new InstanceContainer(constructor));
            }
            else
            {
                instanceContainer[type].Instantiator = constructor;
            }
        }
    }
}
