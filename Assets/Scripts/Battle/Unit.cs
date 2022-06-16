using System.Collections.Generic;
using System.Numerics;

namespace Battle
{
    public class Unit
    {
        protected Dictionary<int, int> props;
        public Vector2 Pos { get; private set; }
        public int UnitID { get; private set; }
        public UnitData Data { get; private set; }

        public Unit(int unitID, UnitData data, Vector2 pos)
        {
            Data = data;

            props = new Dictionary<int, int>();

            props[Prop.HP] = data.hp;
            props[Prop.DPS] = data.dps;

            Pos = pos;
        }
    };
}
