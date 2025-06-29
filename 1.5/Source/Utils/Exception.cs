using System;
using System.Diagnostics;
using Verse;

namespace NzRimImmortalBizarre
{
    public static partial class Utils
    {
        public static void PrintExceptionWithStackTrace(this Exception ex)
        {
            Log.Error($"{ex.GetType().Name}: {ex.Message}");
            StackTrace stackTrace = new StackTrace(ex, true);
            foreach (StackFrame frame in stackTrace.GetFrames())
            {
#if DEBUG
                // 在调试模式下输出行号
                Log.Error($"    at {frame.GetMethod().DeclaringType.FullName}.{frame.GetMethod().Name}({frame.GetFileName()}:{frame.GetFileLineNumber()})");
#else
                // 在发布模式下不输出行号
                Log.Error($"    at {frame.GetMethod().DeclaringType.FullName}.{frame.GetMethod().Name}({frame.GetFileName()})");
#endif
            }
        }
    }
}