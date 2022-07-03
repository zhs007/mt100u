using System.Collections.Generic;
using UnityEngine;

namespace Battle
{
    public class AI1 : IAI
    {
        protected Unit mainUnit;
        public bool isForward;
        public Unit target;

        public AI1(Unit unit)
        {
            mainUnit = unit;
            isForward = false;
        }

        public void onIdle(int ts)
        {
            if (isForward)
            {
                if (target != null)
                {
                    mainUnit.LookAt(target.Pos);
                }

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
            if (target != null)
            {
                var cd = Vector2.Distance(mainUnit.Pos, target.Pos);
                if (cd < mainUnit.Data.abandonRange)
                {
                    return true;
                }

                target = null;
            }

            int r = Random.Range(0, 2);
            if (r < 1)
            {
                var t = mainUnit.FindVisualTarget();
                if (t == null)
                {
                    int za = Random.Range(-30, 30);
                    mainUnit.Rotate((float)za);

                    isForward = true;
                }
                else
                {
                    target = t;
                    mainUnit.LookAt(target.Pos);

                    isForward = true;
                }

                return true;
            }

            isForward = false;

            return false;
        }
    };
}
