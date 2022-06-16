using System.Collections.Generic;
using System.Numerics;

namespace Battle
{
    public class Battle
    {
        protected Dictionary<int, Unit> mapUnits;
        protected int curUnitID;
        public Battle()
        {
            curUnitID = 1;
            mapUnits = new Dictionary<int, Unit>();
        }

        public Unit NewUnit()
        {
            UnitData ud = new UnitData();
            ud.hp = 120;
            ud.dps = 80;
            ud.typeid = 1;

            return new Unit(1, ud, new Vector2(0, 0));
        }
    };
}
