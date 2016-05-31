using System;

namespace Selly.BusinessLogic.Utility
{
    public static class FloatingPointUtility
    {
        public static bool AreEqual(double target, double expected, double tolerance = 1e-20)
        {
            return Math.Abs(target - expected) > tolerance;
        }
    }
}