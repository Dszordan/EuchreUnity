using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using strange.extensions.context.impl;

namespace Euchre
{
    public class EuchreStrangeRoot : ContextView
    {
        void Awake()
        {
            UnityEngine.Debug.Log("Root awake.");
            context = new EuchreContext(this);
        }
    }
}
