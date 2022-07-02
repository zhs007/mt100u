using System.Collections.Generic;
using System;
using UnityEngine;

namespace Battle
{
    // 区域范围，可以是可视区域范围，可攻击区域范围等，每个unit都只应该考虑区域范围以内的事情
    public class AreaRange
    {
        public int sx, sy, ex, ey;

        public AreaRange(int sx, int sy, int ex, int ey)
        {
            this.sx = sx;
            this.sy = sy;
            this.ex = ex;
            this.ey = ey;
        }

        // isbreak onEach(area)
        public void ForEach(Battle battle, Func<Area, bool> onEach)
        {
            for (int ax = sx; ax <= ex; ax++)
            {
                for (int ay = sy; ay < ey; ay++)
                {
                    if (onEach(battle.GetArea(ax, ay)))
                    {
                        break;
                    }
                }
            }
        }
    };
}
