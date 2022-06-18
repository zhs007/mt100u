using System.Collections.Generic;
using UnityEngine;

namespace Battle
{
    // 这个是battle的区域缓存，静态缓存区域，每个obj只可能存在于一个区域内
    // 所以判断时，需要判断周围至少4个区域
    public class Area
    {
        protected Dictionary<int, MapObj> objs;
        protected Dictionary<int, MapObj> staticObjs;
        public int AreaX { get; private set; }
        public int AreaY { get; private set; }

        public Area(int ax, int ay)
        {
            AreaX = ax;
            AreaY = ay;

            objs = new Dictionary<int, MapObj>();
            staticObjs = new Dictionary<int, MapObj>();
        }

        public void Add(MapObj obj)
        {
            if (obj.IsStatic)
            {
                staticObjs[obj.EntityID] = obj;
            }
            else
            {
                objs[obj.EntityID] = obj;
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
                objs.Remove(obj.EntityID);
            }
        }

        public bool CanMove(Unit unit, Vector2 off)
        {
            foreach (KeyValuePair<int, MapObj> entry in objs)
            {
                if (entry.Key != unit.EntityID)
                {
                    if (entry.Value.CanCollide(unit, off))
                    {
                        return false;
                    }
                }
            }

            foreach (KeyValuePair<int, MapObj> entry in staticObjs)
            {
                if (entry.Key != unit.EntityID)
                {
                    if (entry.Value.CanCollide(unit, off))
                    {
                        return false;
                    }
                }
            }

            return true;
        }
    };
}
