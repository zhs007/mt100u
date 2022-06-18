using System.Collections.Generic;
using UnityEngine;

namespace Battle
{
    public class Unit : MapObj
    {
        protected Dictionary<int, int> props;
        public Vector2 Forward;
        public UnitData Data { get; protected set; }

        public Unit(int entityID, UnitData data, Vector2 pos, float size) : base(entityID, pos, size)
        {
            Data = data;

            props = new Dictionary<int, int>();

            props[Prop.HP] = data.hp;
            props[Prop.DPS] = data.dps;
        }

        // public void SetPos(float x, float y)
        // {
        //     Pos = new Vector2(x, y);
        // }

        // public void SetForawar(float x, float y)
        // {
        //     Pos = new Vector2(x, y);
        // }
    };
}
