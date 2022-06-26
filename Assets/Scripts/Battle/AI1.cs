using System.Collections.Generic;
using UnityEngine;

namespace Battle
{
    public class AI1 : IAI
    {
        protected Unit mainUnit;
        public bool isForward;

        public AI1(Unit unit)
        {
            mainUnit = unit;
            isForward = false;
        }

        public void onIdle(int ts)
        {
            if (isForward)
            {
                mainUnit.MoveForward(ts / 1000.0f);
            }
            // int r = Random.Range(0, 20);
            // if (r < 1)
            // {
            //     int za = Random.Range(-30, 30);
            //     mainUnit.Rotate((float)za);

            //     // Debug.Log("AI1 angle - " + za);

            //     mainUnit.MoveForward();

            //     return true;
            // }

            // return false;
        }

        public bool onThink()
        {
            int r = Random.Range(0, 2);
            if (r < 1)
            {
                int za = Random.Range(-30, 30);
                mainUnit.Rotate((float)za);

                isForward = true;

                // Debug.Log("AI1 angle - " + za);

                // mainUnit.MoveForward();

                return true;
            }

            isForward = false;

            return false;
        }
    };
}
