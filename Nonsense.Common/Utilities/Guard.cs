using System;
using System.Diagnostics;

namespace Nonsense.Common.Utilities {

    public static class Guard {

        [DebuggerStepThrough]
        public static void NotNull(object argValue, string argName) {
            if (argValue == null) {
                throw new ArgumentNullException(argName);
            }
        }

        [DebuggerStepThrough]
        public static void NotNullOrEmpty(string argValue, string argName) {
            if (String.IsNullOrEmpty(argValue)) {
                throw new ArgumentNullException(argName);
            }
        }

        [DebuggerStepThrough]
        public static void NotOutOfRange(int argValue, string argName, int minValue, int maxValue) {
            if (minValue > argValue || argValue > maxValue) {
                throw new ArgumentOutOfRangeException(argName);
            }
        }
    }
}
