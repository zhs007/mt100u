using System.Collections.Generic;
using System.Numerics;
using UnityEngine;

namespace Battle
{
    public class MapObj
    {
        public System.Numerics.Vector2 Pos;
        public int EntityID;

        public MapObj()
        {
        }

        public bool IsCollect(MapObj obj, System.Numerics.Vector2 off)
        {
            if (System.Numerics.Vector2.Distance(Pos + off, obj.Pos) < 2)
            {
                Debug.Log("player - " + Pos + " obj - " + obj.Pos);

                return true;
            }

            return false;
        }
    };
}
