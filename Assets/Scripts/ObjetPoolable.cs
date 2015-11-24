using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;


namespace Assets.Scripts
{
    public interface ObjetPoolable<T>
    {
        bool isAvailable();
        void Copy(T b);
        void putUnavailable();
    }

}

