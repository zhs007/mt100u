using System.Collections.Generic;
using UnityEngine;

namespace Battle
{
    public class Battle
    {
        protected Dictionary<int, MapObj> mapObjs;
        // protected Dictionary<int, StaticObj> mapUnits;
        protected int curEntityID;
        protected Unit mainUnit;
        protected Area[,] areas;
        public int StartX { get; private set; }
        public int StartY { get; private set; }
        public int EndX { get; private set; }
        public int EndY { get; private set; }
        public int AreaWidth { get; private set; }
        public int AreaHeight { get; private set; }

        public Battle(int startX, int startY, int endX, int endY, int areaWidth, int areaHeight)
        {
            if (areaWidth <= 0)
            {
                areaWidth = 10;
            }

            if (areaHeight <= 0)
            {
                areaHeight = 10;
            }

            StartX = startX;
            StartY = startY;
            EndX = endX;
            EndY = endY;

            AreaWidth = areaWidth;
            AreaHeight = areaHeight;

            areas = new Area[(EndX - StartX) / AreaWidth + 1, (EndY - StartY) / AreaHeight + 1];
            for (int ax = 0; ax <= (EndX - StartX) / AreaWidth; ax++)
            {
                for (int ay = 0; ay <= (EndY - StartY) / AreaHeight; ay++)
                {
                    areas[ax, ay] = new Area(ax, ay);
                }
            }

            curEntityID = 1;
            mapObjs = new Dictionary<int, MapObj>();
        }

        public Unit NewUnit(Vector2 pos, float size)
        {
            UnitData ud = new UnitData();
            ud.hp = 120;
            ud.dps = 80;
            ud.typeid = 1;

            Unit unit = new Unit(curEntityID, ud, pos, size, false, this);

            mapObjs[curEntityID++] = unit;

            onNewObj(unit);

            return unit;
        }

        public MapObj NewMapObj(Vector2 pos, float size, bool isStatic)
        {
            MapObj obj = new MapObj(curEntityID, pos, size, isStatic, this);
            obj.Pos = pos;

            mapObjs[curEntityID++] = obj;

            onNewObj(obj);

            return obj;
        }

        public bool CanMove(Unit unit, Vector2 off)
        {
            int sax = unit.area.AreaX;
            int say = unit.area.AreaY;
            int eax = GetAreaX((int)(unit.Pos.x + off.x));
            int eay = GetAreaY((int)(unit.Pos.y + off.y));

            int sx = sax > eax ? eax : sax;
            int sy = say > eay ? eay : say;
            int ex = sax < eax ? eax : sax;
            int ey = say < eay ? eay : say;

            for (int ax = sx; ax <= ex; ax++)
            {
                for (int ay = sy; ay <= ey; ay++)
                {
                    Area ca = areas[ax, ay];
                    if (!ca.CanMove(unit, off))
                    {
                        return false;
                    }
                }
            }

            // foreach (KeyValuePair<int, MapObj> entry in mapObjs)
            // {
            //     if (entry.Key != unit.EntityID)
            //     {
            //         if (entry.Value.CanCollide(unit, off))
            //         {
            //             return false;
            //         }
            //     }
            // }

            return true;
        }

        public int GetWidth()
        {
            return EndX - StartX;
        }

        public int GetHeight()
        {
            return EndY - StartY;
        }

        public int GetAreaX(int x)
        {
            if (x <= StartX)
            {
                return 0;
            }

            if (x >= EndX)
            {
                return (EndX - StartX) / AreaWidth;
            }

            return (x - StartX) / AreaWidth;
        }

        public int GetAreaY(int y)
        {
            if (y <= StartY)
            {
                return 0;
            }

            if (y >= EndY)
            {
                return (EndY - StartY) / AreaHeight;
            }

            return (y - StartY) / AreaHeight;
        }

        protected void onNewObj(MapObj obj)
        {
            int ax = GetAreaX((int)obj.Pos.x);
            int ay = GetAreaY((int)obj.Pos.y);

            Area curArea = areas[ax, ay];
            curArea.Add(obj);
            obj.onChgArea(curArea);
        }

        public void onChgPos(MapObj obj)
        {
            int ax = GetAreaX((int)obj.Pos.x);
            int ay = GetAreaX((int)obj.Pos.y);

            if (ax != obj.area.AreaX || ay != obj.area.AreaY)
            {
                obj.area.Remove(obj);

                Area curArea = areas[ax, ay];
                curArea.Add(obj);
                obj.onChgArea(curArea);
            }
        }
    };
}
