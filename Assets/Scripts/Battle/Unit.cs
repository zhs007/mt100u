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
        protected int lastThinkTs;

        public Unit(int entityID, UnitData data, Vector2 pos, bool isStatic, Battle battle, GameObject gameObj) : base(entityID, pos, data.size, isStatic, battle, gameObj)
        {
            Data = data;

            props = new Dictionary<int, int>();

            props[Prop.HP] = data.hp;
            props[Prop.DPS] = data.dps;

            Forward = new Vector2(Mathf.Cos(0), Mathf.Sin(0));

            lastThinkTs = Random.Range(0, data.thinkts);
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

        public void onAIIdle(int ts)
        {
            if (ai != null)
            {
                // Debug.Log("onaiidle " + lastThinkTs + " " + ts);
                lastThinkTs -= ts;

                if (lastThinkTs <= 0)
                {
                    if (ai.onThink())
                    {
                        lastThinkTs = Data.thinkts;
                    }
                }

                ai.onIdle(ts);
            }
        }

        public void MoveForward(float ts)
        {
            if (battle.CanMove(this, Forward))
            {
                var vec2 = Forward * ts * Data.speed;

                Pos += vec2;
                gameObj.transform.position += new Vector3(vec2.x, vec2.y, 0);
            }
        }

        public void Rotate(float angle)
        {
            float curAngle = Mathf.Atan2(Forward.y, Forward.x) * Mathf.Rad2Deg;
            curAngle += angle;
            Forward = new Vector2(Mathf.Cos(curAngle), Mathf.Sin(curAngle));
            Forward.Normalize();
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
