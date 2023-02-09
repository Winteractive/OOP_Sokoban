/*
 _    _ _       _                      _   _              _________    _____  _____  _____  _____ 
| |  | (_)     | |                    | | (_)            / /  __ \ \  / __  \|  _  |/ __  \/ __  \
| |  | |_ _ __ | |_ ___ _ __ __ _  ___| |_ ___   _____  | || /  \/| | `' / /'| |/' |`' / /'`' / /'
| |/\| | | '_ \| __/ _ \ '__/ _` |/ __| __| \ \ / / _ \ | || |    | |   / /  |  /| |  / /    / /  
\  /\  / | | | | ||  __/ | | (_| | (__| |_| |\ V /  __/ | || \__/\| | ./ /___\ |_/ /./ /___./ /___
 \/  \/|_|_| |_|\__\___|_|  \__,_|\___|\__|_| \_/ \___| | | \____/| | \_____/ \___/ \_____/\_____/
                                                         \_\     /_/                                                                                                                               
*/


using System;

namespace EMMA_ENGINE
{
   public static class Debug
    {
        [Flags]
        public enum DebugStates
        {
            Log = 1,
            Warning = 2,
            Error = 4,
            Standout = 8,
        }

        private static DebugStates _states = DebugStates.Error | DebugStates.Log | DebugStates.Warning;

        public static void SetDebugState(DebugStates state)
        {
            _states = state;
        }


        public static void Log(string m)
        {
            if (!_states.HasFlag(DebugStates.Log)) return;
            Console.WriteLine(m);
        }

        public static void Log(object o)
        {
            if (!_states.HasFlag(DebugStates.Log)) return;
            Log(o.ToString());
        }

        public static void LogWarning(string m)
        {
            if (!_states.HasFlag(DebugStates.Warning)) return;
            Console.BackgroundColor = ConsoleColor.DarkYellow;
            Console.ForegroundColor = ConsoleColor.White;
            Console.WriteLine(m);
            Console.ResetColor();
        }

        public static void LogWarning(object o)
        {
            LogWarning(o.ToString());
        }

        public static void LogError(string m)
        {
            if (!_states.HasFlag(DebugStates.Error)) return;
            Console.BackgroundColor = ConsoleColor.DarkRed;
            Console.ForegroundColor = ConsoleColor.White;
            Console.WriteLine(m);
            Console.ResetColor();
        }

        public static void LogError(object o)
        {
            LogError(o.ToString());
        }

        public static void LogStandout(string m)
        {
            if (!_states.HasFlag(DebugStates.Standout)) return;
            Console.BackgroundColor = ConsoleColor.DarkBlue;
            Console.ForegroundColor = ConsoleColor.White;
            Console.WriteLine(Environment.NewLine);
            Console.WriteLine(m);
            Console.WriteLine(Environment.NewLine);
            Console.ResetColor();
        }

        public static void LogStandout(object o)
        {
            LogStandout(o.ToString());
        }
    }
}
