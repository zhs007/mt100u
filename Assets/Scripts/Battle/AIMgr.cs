using System.Collections.Generic;
using System;
using UnityEngine;

namespace Battle
{
    public class AIMgr
    {
        protected static Dictionary<int, Func<Unit, IAI>> mapAI = new Dictionary<int, Func<Unit, IAI>>();

        public static int RegAI(int aiType, Func<Unit, IAI> func)
        {
            mapAI[aiType] = func;

            return aiType;
        }

        public static IAI NewAI(int aiType, Unit unit)
        {
            if (mapAI.ContainsKey(aiType))
            {
                return mapAI[aiType](unit);
            }

            return null;
        }
    };
}
