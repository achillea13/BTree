using System;
using System.Collections.Generic;


namespace Ltm.ActionTree
{
    public static class SimpleLog
    {
        private static bool _debug = false;
        public static void TurnDebugCondition(bool isDebug)
        {
            _debug = isDebug;
        }

        public static void Log(params object[] objs)
        {
            if (_debug)
            {
#if DEBUG
                Console.Write(objs);
#endif
            }
            else
            {
                Console.Write(objs);
            }

        }

    }
}
