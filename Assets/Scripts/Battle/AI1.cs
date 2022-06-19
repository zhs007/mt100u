using System.Collections.Generic;
using UnityEngine;

namespace Battle
{
    public class AI1 : IAI
    {
        public static int Type = AIMgr.RegAI(AIType.AI1, (unit) =>
        {
            return new AI1(unit);
        });

        protected Unit mainUnit;

        public AI1(Unit unit)
        {
            mainUnit = unit;
        }

        public void onIdle()
        {

        }
    };
}
