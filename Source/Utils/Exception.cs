
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
                Log.Error($"    at {frame.GetMethod().DeclaringType.FullName}.{frame.GetMethod().Name}({frame.GetFileName()}:{frame.GetFileLineNumber()})");
            }
        }
    }
}