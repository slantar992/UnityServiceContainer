using System;

namespace Slantar.Services
{
    public class InstanceContainer
    {
        private object instance;
        private Func<object> instantiator;

        public object Instance
        {
            get
            {
                if (instance == null)
                {
                    instance = Instantiator();
                }

                return instance;
            }

            set
            {
                instance = value;
            }
        }

        public Func<object> Instantiator
        {
            get
            {
                return instantiator;
            }
            set
            {
                instance = null;
                instantiator = value;
            }
        }

        public InstanceContainer(Func<object> instantiator)
        {
            this.instantiator = instantiator;
        }
    }
}