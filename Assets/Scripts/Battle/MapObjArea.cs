using System.Collections.Generic;
using UnityEngine;
using System;

namespace Battle
{
    public class MapObjArea
    {
        public class Data
        {
            public MapObj obj;
            public Func<bool, int> onInOut;

            public Data(MapObj obj, Func<bool, int> func)
            {
                this.obj = obj;
                onInOut = func;
            }
        };

        public MapObj obj { get; private set; }
        public float Size { get; private set; }
        public Dictionary<int, Data> objs;
        public int ObjAreaID { get; private set; }
        protected List<int> curRemovedObjID;

        public MapObjArea(int objAreaID, MapObj obj, float size)
        {
            this.obj = obj;
            Size = size;

            objs = new Dictionary<int, Data>();
            ObjAreaID = objAreaID;
            curRemovedObjID = new List<int>();
        }

        public bool IsIn(MapObj other)
        {
            Func<bool, int> func = other.GetObjAreaFunc(ObjAreaID);
            if (func == null)
            {
                return false;
            }

            if (objs.ContainsKey(other.EntityID))
            {
                return true;
            }

            float distance = Vector2.Distance(obj.Pos, other.Pos);
            if (distance <= (other.Size + Size) / 2)
            {
                func(true);

                Data cd = new Data(other, func);

                objs[other.EntityID] = cd;

                return true;
            }

            return false;
        }

        public void onMoved()
        {
            foreach (KeyValuePair<int, Data> entry in objs)
            {
                float distance = Vector2.Distance(obj.Pos, entry.Value.obj.Pos);
                if (distance > (entry.Value.obj.Size + Size) / 2)
                {
                    entry.Value.onInOut(false);

                    curRemovedObjID.Add(entry.Key);
                    // objs.Remove(entry.Key);
                }
            }

            foreach (int v in curRemovedObjID)
            {
                objs.Remove(v);
            }

            curRemovedObjID.Clear();
        }
    };
}
