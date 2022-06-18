using System.Collections.Generic;
using UnityEngine;

namespace Battle
{
    public class CollisionData
    {
        public MapObj[] Objs { get; private set; }
        public float LastDictance { get; private set; }

        public CollisionData(MapObj obj0, MapObj obj1, float distance)
        {
            Objs = new MapObj[] { obj0, obj1 };
            LastDictance = distance;
        }

        // 如果已经碰撞在一起了，接下来是远离，就不算碰撞
        public bool CanCollide(float distance)
        {
            if (distance > LastDictance)
            {
                LastDictance = distance;

                return false;
            }

            return true;
        }
    };
}
