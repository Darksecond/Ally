namespace Ally.RyzenMaster;

public sealed class Platform : IDisposable
{
    private readonly IntPtr handle;

    public Platform()
    {
        handle = NativeMethods.GetPlatformWrapper();
        NativeMethods.Platform_Init(handle);
    }

    public void Dispose()
    {
        NativeMethods.Platform_UnInit(handle);
    }

    public DeviceManager GetDeviceManager()
    {
        var instance = NativeMethods.Platform_GetDeviceManager(handle);
        return new DeviceManager(instance);
    }
}
