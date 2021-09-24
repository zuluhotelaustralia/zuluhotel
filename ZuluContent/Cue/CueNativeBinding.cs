using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.InteropServices;

// ReSharper disable UnusedMember.Global
// ReSharper disable UnusedType.Global
// ReSharper disable InconsistentNaming
// ReSharper disable MemberCanBePrivate.Global
// ReSharper disable NotAccessedField.Global

namespace Scripts.Cue
{
    public struct GoSlice
    {
        public IntPtr data;
        public long len, cap;
        public GoSlice(IntPtr data, long len, long cap)
        {
            this.data = data;
            this.len = len;
            this.cap = cap;
        }
    }
    public struct GoString
    {
        public string msg;
        public long len;
        public GoString(string msg, long len)
        {
            this.msg = msg;
            this.len = len;
        }
    }

    public static class GoBindingExtensions
    {
        public static GoString ToGoString(this string str)
        {
            return new GoString(str, str.Length);
        }
    }

    public static class CueNativeBinding
    {
        private const string LibName = "golibcue.so";
        static CueNativeBinding()
        {
            Environment.SetEnvironmentVariable("GODEBUG", "cgocheck=0");
            Environment.SetEnvironmentVariable("GOGC", "off");
        }
        
        [DllImport(LibName, CharSet = CharSet.Auto)]
        private static extern IntPtr Compile([In]string contextDir, [In]string[] entrypoints, int entrypointsLen);
        
        [DllImport(LibName, CharSet = CharSet.Auto)]
        public static extern void Free(IntPtr array, int arrayLen);

        public static (string[] data, Action free) Compile(string contextDir, string[] entrypoints)
        {
            var arrayPtr = Compile(contextDir, entrypoints, entrypoints.Length);
            var data = MarshalStringArray(arrayPtr).ToArray();
            return (data, () => Free(arrayPtr, entrypoints.Length));
        }

        private static IEnumerable<string> MarshalStringArray(IntPtr arrayPtr)
        {
            if (arrayPtr != IntPtr.Zero)
            {
                var ptr = Marshal.ReadIntPtr(arrayPtr);
                while (ptr != IntPtr.Zero)
                {
                    var key = Marshal.PtrToStringAnsi(ptr);
                    yield return key;
                    arrayPtr = new IntPtr(arrayPtr.ToInt64() + IntPtr.Size);
                    ptr = Marshal.ReadIntPtr(arrayPtr);
                }
            }
        }
    }
}