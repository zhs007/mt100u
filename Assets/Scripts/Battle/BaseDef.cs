
namespace Battle
{
    public class Prop
    {
        // 基础属性配置，初始值可以直接用这里的
        public const int HP = 0;
        public const int DPS = 2;

        // 战斗用属性配置，当前瞬时值
        public const int CurHP = 1000;
        public const int MaxHP = 1001;
        public const int CurDPS = 1002;
    };

    public class AIType
    {
        public const int AI1 = 1;
    };

    public class FactionType
    {
        public const int Player = 0;
        public const int Enemey = 1;

        public static int GetEnemy(int faction)
        {
            if (faction == Player)
            {
                return Enemey;
            }

            return Player;
        }
    };

    public class BaseDef
    {
        public const float MaxDistance = 99999999;
    }
}
