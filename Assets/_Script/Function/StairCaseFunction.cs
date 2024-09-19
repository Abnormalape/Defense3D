using System;

namespace BHSSolo.DungeonDefense.Function
{
    static public class StaircaseFunction
    {
        public static double CalculateStair(double input, double stair)
        {
            return stair * Math.Floor((input + stair / 2) / stair);
        }
    }
}
