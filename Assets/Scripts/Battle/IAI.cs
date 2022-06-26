using System.Collections.Generic;
using UnityEngine;

namespace Battle
{
    public interface IAI
    {
        public void onIdle(int ts);
        public bool onThink();
    };
}
