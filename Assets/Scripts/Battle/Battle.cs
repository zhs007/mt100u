using System.Collections.Generic;
using System.Numerics;

namespace Battle
{
    public class Battle
    {
        protected Dictionary<int, MapObj> mapObjs;
        // protected Dictionary<int, StaticObj> mapUnits;
        protected int curEntityID;
        protected Unit mainUnit;
        public Battle()
        {
            curEntityID = 1;
            mapObjs = new Dictionary<int, MapObj>();
        }

        public Unit NewUnit()
        {
            UnitData ud = new UnitData();
            ud.hp = 120;
            ud.dps = 80;
            ud.typeid = 1;

            Unit unit = new Unit(curEntityID, ud, new Vector2(0, 0));

            mapObjs[curEntityID++] = unit;

            return unit;
        }

        public MapObj NewMapObj(Vector2 pos)
        {
            MapObj obj = new MapObj();
            obj.Pos = pos;
            obj.EntityID = curEntityID;

            mapObjs[curEntityID++] = obj;

            return obj;
        }

        public bool CanMove(Unit unit, Vector2 off)
        {
            foreach (KeyValuePair<int, MapObj> entry in mapObjs)
            {
                if (entry.Key != unit.EntityID)
                {
                    if (entry.Value.IsCollect(unit, off))
                    {
                        return false;
                    }
                }
            }

            return true;
        }
    };
}
