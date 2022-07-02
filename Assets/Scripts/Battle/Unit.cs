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
        public int Faction { get; private set; }
        public AreaRange visualAreaRange;

        public Unit(int entityID, UnitData data, Vector2 pos, bool isStatic, Battle battle, GameObject gameObj, int faction) : base(entityID, pos, data.size, isStatic, battle, gameObj)
        {
            Data = data;

            props = new Dictionary<int, int>();

            props[Prop.HP] = data.hp;
            props[Prop.DPS] = data.dps;

            Forward = new Vector2(Mathf.Cos(0), Mathf.Sin(0));

            lastThinkTs = Random.Range(0, data.thinkts);

            Faction = faction;

            visualAreaRange = battle.NewAreaRange(pos, data.visualRange);
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

        public void onChgArea(Area area)
        {
            this.area = area;

            visualAreaRange = battle.NewAreaRange(Pos, Data.visualRange);
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

        public Unit FindVisualTarget()
        {
            var enemyTarget = FactionType.GetEnemy(Faction);
            float d = BaseDef.MaxDistance;
            Unit target = null;

            visualAreaRange.ForEach(battle, (area) =>
            {
                area.ForEachUnits((unit) =>
                {
                    if (unit.Faction == enemyTarget)
                    {
                        var cd = Vector2.Distance(Pos, unit.Pos);
                        if (cd < d)
                        {
                            d = cd;
                            target = unit;
                        }
                    }

                    return false;
                });

                return false;
            });

            return target;
        }

        public void LookAt(Vector2 pos)
        {
            Forward = pos - Pos;
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
