using System;

namespace Engine
{

    public class Lazy<T> where T : class
    {

        private object locker = new object();
        private Func<T> ctor;
        private T instance;

        public Lazy(Func<T> ctor)
        {
            this.ctor = ctor;
        }

        public T Value
        {
            get
            {
                if(instance == null)
                {
                    lock(locker)
                    {
                        if(instance == null)
                        {
                            instance = ctor();
                        }
                    }
                }
                return instance;
            }
        }

    }

}
