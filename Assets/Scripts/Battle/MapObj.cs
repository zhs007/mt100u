using System.Collections.Generic;
using System;
using UnityEngine;

namespace Battle
{
    public class MapObj
    {
        public Vector2 Pos;
        public float Size;
        public int EntityID { get; private set; }
        protected Dictionary<int, CollisionData> mapCollisions;
        public bool IsStatic { get; private set; }
        public Area area { get; private set; }
        protected Battle battle;
        protected Dictionary<int, MapObjArea> mapAreas; // 对象区域缓存，理解为领域吧
        protected Dictionary<int, Func<bool, int>> mapObjAreaFunc;  // 进入别人领域时触发的事件
        public int Camp { get; private set; } // 广义的阵营，阵营内部和阵营外部很多处理会不一样，实际上，譬如场景内静态物体，也会是一种中立阵营

        public MapObj(int entityID, Vector2 pos, float size, bool isStatic, Battle battle)
        {
            EntityID = entityID;
            Pos = pos;
            Size = size;
            mapCollisions = new Dictionary<int, CollisionData>();
            IsStatic = isStatic;
            this.battle = battle;
            mapAreas = new Dictionary<int, MapObjArea>();
            mapObjAreaFunc = new Dictionary<int, Func<bool, int>>();
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

        public bool CanCollideEx(Vector2 pos, float size)
        {
            return Vector2.Distance(Pos, pos) < (size + Size) / 2;
        }

        public void onChgArea(Area area)
        {
            this.area = area;
        }

        public void Move(Vector2 off)
        {
            int sx = (int)Pos.x;
            int sy = (int)Pos.y;

            Pos += off;

            int ex = (int)Pos.x;
            int ey = (int)Pos.y;

            if (sx != ex || sy != ey)
            {
                onMoved();

                battle.onChgPos(this);
            }
        }

        public void onMoved()
        {
            foreach (KeyValuePair<int, MapObjArea> entry in mapAreas)
            {
                entry.Value.onMoved();
            }
        }

        public void onAddObjArea(MapObjArea moa)
        {
            mapAreas[moa.ObjAreaID] = moa;
        }

        public void AddObjAreaFunc(int objAreaID, Func<bool, int> func)
        {
            mapObjAreaFunc[objAreaID] = func;
        }

        public Func<bool, int> GetObjAreaFunc(int objAreaID)
        {
            if (mapObjAreaFunc.ContainsKey(objAreaID))
            {
                return mapObjAreaFunc[objAreaID];
            }

            return null;
        }
    };
}
