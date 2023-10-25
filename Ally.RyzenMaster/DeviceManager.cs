namespace Ally.RyzenMaster;

public sealed class DeviceManager
{
    private readonly IntPtr handle;

    internal DeviceManager(IntPtr handle)
    {
        this.handle = handle;
    }

    public CpuDevice? GetCpu()
    {
        var instance = NativeMethods.DeviceManager_GetCPU(handle);

        return new CpuDevice(instance);
    }
}
