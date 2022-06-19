using System.Collections.Generic;
using UnityEngine;

namespace Battle
{
    public class Unit : MapObj
    {
        protected Dictionary<int, int> props;
        public Vector2 Forward;
        public UnitData Data { get; protected set; }
        protected IAI ai;

        public Unit(int entityID, UnitData data, Vector2 pos, float size, bool isStatic, Battle battle) : base(entityID, pos, size, isStatic, battle)
        {
            Data = data;

            props = new Dictionary<int, int>();

            props[Prop.HP] = data.hp;
            props[Prop.DPS] = data.dps;
        }

        public void AddAI(int aiType)
        {
            ai = AIMgr.NewAI(aiType, this);
            if (ai != null)
            {
                battle.addAIUnit(this);
            }
            // this.ai = AIMgr

            // battle.addAIUnit(this);
        }

        public void onAIIdle()
        {
            if (ai != null)
            {
                ai.onIdle();
            }
        }

        public void MoveForward()
        {
            
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
