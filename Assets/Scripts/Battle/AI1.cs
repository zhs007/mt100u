using System.Collections.Generic;
using UnityEngine;

namespace Battle
{
    public class AI1 : IAI
    {
        protected Unit mainUnit;

        public AI1(Unit unit)
        {
            mainUnit = unit;
        }

        public void onIdle()
        {
            int r = Random.Range(0, 20);
            if (r < 1)
            {
                int za = Random.Range(-30, 30);
                mainUnit.Rotate((float)za);

                // Debug.Log("AI1 angle - " + za);

                mainUnit.MoveForward();
            }
        }
    };
}
