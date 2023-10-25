namespace Ally.RyzenMaster;

public sealed class CpuDevice
{
    private readonly IntPtr handle;

    internal CpuDevice(IntPtr handle)
    {
        this.handle = handle;
    }

    public uint CoreCount => NativeMethods.CPU_GetCoreCount(handle);
    public float FastPPTvalue => NativeMethods.CPU_fPPTValue(handle);
}
