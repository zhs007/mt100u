using System.Collections.Generic;
using System;
using UnityEngine;

namespace Battle
{
    public class Battle
    {
        protected Dictionary<int, MapObj> mapObjs;
        protected Dictionary<int, Unit> mapUnits;
        protected int curEntityID;
        protected Unit mainUnit;
        protected Area[,] areas;
        public int StartX { get; private set; }
        public int StartY { get; private set; }
        public int EndX { get; private set; }
        public int EndY { get; private set; }
        public int AreaWidth { get; private set; }
        public int AreaHeight { get; private set; }
        protected Dictionary<int, MapObjArea> mapObjAreas;
        protected Dictionary<int, Unit> mapAIUnit;
        protected FactionUnits factionUnits;

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
            mapUnits = new Dictionary<int, Unit>();

            mapObjAreas = new Dictionary<int, MapObjArea>();

            mapAIUnit = new Dictionary<int, Unit>();

            factionUnits = new FactionUnits();
        }

        public Unit NewUnit(int unitTypeID, Vector2 pos, GameObject gameObj, int faction)
        {
            UnitData ud = UnitMgr.GetUnitData(unitTypeID);

            Unit unit = new Unit(curEntityID, ud, pos, false, this, gameObj, faction);

            mapObjs[curEntityID] = unit;
            mapUnits[curEntityID] = unit;

            curEntityID++;

            onNewObj(unit);

            factionUnits.AddUnit(unit);

            return unit;
        }

        public MapObj NewMapObj(Vector2 pos, float size, bool isStatic, GameObject gameObj, Func<MapObj, int> onNew)
        {
            MapObj obj = new MapObj(curEntityID, pos, size, isStatic, this, gameObj);
            obj.Pos = pos;

            mapObjs[curEntityID] = obj;

            curEntityID++;

            if (onNew != null)
            {
                onNew(obj);
            }

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
            // int ax = GetAreaX((int)obj.Pos.x);
            // int ay = GetAreaY((int)obj.Pos.y);

            // Area curArea = areas[ax, ay];
            // curArea.Add(obj);
            // obj.onChgArea(curArea);

            foreach (KeyValuePair<int, MapObjArea> entry in mapObjAreas)
            {
                entry.Value.IsIn(obj);
            }
        }

        public void onChgPos(Unit unit)
        {
            int ax = GetAreaX((int)unit.Pos.x);
            int ay = GetAreaX((int)unit.Pos.y);

            if (ax != unit.area.AreaX || ay != unit.area.AreaY)
            {
                unit.area.Remove(unit);

                Area curArea = areas[ax, ay];
                curArea.Add(unit);
                unit.onChgArea(curArea);
            }

            foreach (KeyValuePair<int, MapObjArea> entry in mapObjAreas)
            {
                procObjArea(entry.Value);
            }
        }

        public void AddObjArea(int objAreaID, MapObj obj, float size)
        {
            MapObjArea moa = new MapObjArea(objAreaID, obj, size);

            mapObjAreas[objAreaID] = moa;

            obj.onAddObjArea(moa);
        }

        protected void procObjArea(MapObjArea moa)
        {
            int sax = moa.obj.area.AreaX;
            int say = moa.obj.area.AreaY;
            int eax = GetAreaX((int)(moa.obj.Pos.x + moa.Size));
            int eay = GetAreaY((int)(moa.obj.Pos.y + moa.Size));

            int sx = sax > eax ? eax : sax;
            int sy = say > eay ? eay : say;
            int ex = sax < eax ? eax : sax;
            int ey = say < eay ? eay : say;

            for (int ax = sx; ax <= ex; ax++)
            {
                for (int ay = sy; ay <= ey; ay++)
                {
                    Area ca = areas[ax, ay];
                    ca.procObjArea(moa);
                }
            }
        }

        protected (int, int, int, int) calcAreaWithPosSize(Vector2 pos, float size)
        {
            int sax = GetAreaX((int)(pos.x - size));
            int say = GetAreaY((int)(pos.y - size));
            int eax = GetAreaX((int)(pos.x + size));
            int eay = GetAreaY((int)(pos.y + size));

            return (sax, say, eax, eay);
        }

        // ???????????????????????????????????????
        public bool IsValidPos(Vector2 pos, float size)
        {
            (int sax, int say, int eax, int eay) = calcAreaWithPosSize(pos, size);

            for (int ax = sax; ax <= eax; ax++)
            {
                for (int ay = say; ay <= eay; ay++)
                {
                    Area ca = areas[ax, ay];
                    if (!ca.IsValidPos(pos, size))
                    {
                        return false;
                    }
                }
            }

            return true;
        }

        public void addAIUnit(Unit unit)
        {
            mapAIUnit[unit.EntityID] = unit;
        }

        public void onIdle(int ts)
        {
            foreach (KeyValuePair<int, Unit> entry in mapAIUnit)
            {
                entry.Value.onAIIdle(ts);
                // procObjArea(entry.Value);
            }
        }

        public Area GetArea(int x, int y)
        {
            return areas[x, y];
        }

        public AreaRange NewAreaRange(Vector2 pos, float size)
        {
            (int sax, int say, int eax, int eay) = calcAreaWithPosSize(pos, size);

            return new AreaRange(sax, say, eax, eay);
        }
    };
}
