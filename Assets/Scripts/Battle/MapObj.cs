using System.Collections.Generic;
using UnityEngine;

namespace Battle
{
    public class MapObj
    {
        public Vector2 Pos;
        public float Size;
        public int EntityID { get; private set; }
        protected Dictionary<int, CollisionData> mapCollisions;

        public MapObj(int entityID, Vector2 pos, float size)
        {
            EntityID = entityID;
            Pos = pos;
            Size = size;
            mapCollisions = new Dictionary<int, CollisionData>();
        }

        public bool CanCollide(MapObj obj, Vector2 off)
        {
            float distance = Vector2.Distance(Pos, obj.Pos + off);
            CollisionData cd;

            if (!mapCollisions.ContainsKey(obj.EntityID))
            {
                if (distance <= (Size + obj.Size) / 2)
                {
                    cd = new CollisionData(this, obj, distance);
                    mapCollisions[obj.EntityID] = cd;

                    return true;
                }

                return false;
            }

            cd = mapCollisions[obj.EntityID];

            if (distance > (Size + obj.Size) / 2)
            {
                mapCollisions.Remove(obj.EntityID);

                return false;
            }

            return cd.CanCollide(distance);
        }
    };
}
