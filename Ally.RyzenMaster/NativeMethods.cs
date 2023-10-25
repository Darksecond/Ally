using System.Runtime.InteropServices;

namespace Ally.RyzenMaster;

internal static partial class NativeMethods
{
    [LibraryImport("RyzenMasterWrapper.dll")]
    public static partial IntPtr GetPlatformWrapper();

    [LibraryImport("RyzenMasterWrapper.dll")]
    [return: MarshalAs(UnmanagedType.Bool)]
    public static partial bool Platform_Init(IntPtr platform);

    [LibraryImport("RyzenMasterWrapper.dll")]
    [return: MarshalAs(UnmanagedType.Bool)]
    public static partial bool Platform_UnInit(IntPtr platform);

    [LibraryImport("RyzenMasterWrapper.dll")]
    public static partial IntPtr Platform_GetDeviceManager(IntPtr platform);

    [LibraryImport("RyzenMasterWrapper.dll")]
    public static partial IntPtr DeviceManager_GetCPU(IntPtr deviceManager);

    [LibraryImport("RyzenMasterWrapper.dll")]
    public static partial uint CPU_GetCoreCount(IntPtr cpu);

    [LibraryImport("RyzenMasterWrapper.dll")]
    public static partial float CPU_fPPTValue(IntPtr cpu);
}