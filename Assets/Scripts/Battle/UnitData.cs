using System.Collections.Generic;
using System.Numerics;

namespace Battle
{
    public class UnitData
    {
        // 唯一标识
        public int typeid { get; set; }
        // hp
        public int hp { get; set; }
        // dps
        public int dps { get; set; }
        // 移动速度，1秒移动多少
        public float speed { get; set; }
        // 占位，也就是半径
        public float size { get; set; }
        // 思考时间间隔，ms，每间隔这个长时间才思考一次，这个之间按上一次思考操作
        public int thinkts { get; set; }
        // 可视范围
        public float visualRange { get; set; }
        // 放弃范围，如果目标跑出这个范围，在思考点，可以放弃改目标
        public float abandonRange { get; set; }
    }
}
