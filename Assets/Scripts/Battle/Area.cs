using System.Collections.Generic;
using System;
using UnityEngine;

namespace Battle
{
    // 这个是battle的区域缓存，静态缓存区域，每个obj只可能存在于一个区域内
    // 所以判断时，需要判断周围至少4个区域
    public class Area
    {
        protected Dictionary<int, Unit> units;
        protected Dictionary<int, MapObj> staticObjs;
        public int AreaX { get; private set; }
        public int AreaY { get; private set; }
        protected FactionUnits factionUnits;

        public Area(int ax, int ay)
        {
            AreaX = ax;
            AreaY = ay;

            units = new Dictionary<int, Unit>();
            staticObjs = new Dictionary<int, MapObj>();
            factionUnits = new FactionUnits();
        }

        public void Add(MapObj obj)
        {
            if (obj.IsStatic)
            {
                staticObjs[obj.EntityID] = obj;
            }

            if (obj is Unit)
            {
                units[obj.EntityID] = (Unit)obj;

                factionUnits.AddUnit((Unit)obj);
            }
        }

        public void Remove(MapObj obj)
        {
            if (obj.IsStatic)
            {
                staticObjs.Remove(obj.EntityID);
            }
            else
            {
                units.Remove(obj.EntityID);
            }
        }

        public bool CanMove(Unit unit, Vector2 off)
        {
            foreach (KeyValuePair<int, Unit> entry in units)
            {
                if (entry.Key != unit.EntityID)
                {
                    if (unit.CanCollide(entry.Value, off))
                    {
                        return false;
                    }
                }
            }

            foreach (KeyValuePair<int, MapObj> entry in staticObjs)
            {
                if (entry.Key != unit.EntityID)
                {
                    if (unit.CanCollide(entry.Value, off))
                    {
                        return false;
                    }
                }
            }

            return true;
        }

        public void procObjArea(MapObjArea moa)
        {
            foreach (KeyValuePair<int, Unit> entry in units)
            {
                if (entry.Key != moa.obj.EntityID)
                {
                    moa.IsIn(entry.Value);
                }
            }

            foreach (KeyValuePair<int, MapObj> entry in staticObjs)
            {
                if (entry.Key != moa.obj.EntityID)
                {
                    moa.IsIn(entry.Value);
                }
            }
        }

        // 这个位置是否可以生成新对象
        public bool IsValidPos(Vector2 pos, float size)
        {
            foreach (KeyValuePair<int, Unit> entry in units)
            {
                if (entry.Value.CanCollideEx(pos, size))
                {
                    return false;
                }
            }

            foreach (KeyValuePair<int, MapObj> entry in staticObjs)
            {
                if (entry.Value.CanCollideEx(pos, size))
                {
                    return false;
                }
            }

            return true;
        }

        public void ForEachUnits(Func<Unit, bool> onEach)
        {
            foreach (KeyValuePair<int, Unit> entry in units)
            {
                if (onEach(entry.Value))
                {
                    break;
                }
            }
        }
    };
}
