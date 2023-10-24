namespace Ally.ACPI;

public sealed class AllyACPI : IDisposable
{
    const string FILENAME = @"\\.\\ATKACPI";
    const uint CONTROL_CODE = 0x0022240C;

    const uint DSTS = 0x53545344;
    const uint DEVS = 0x53564544;
    const uint INIT = 0x54494E49;

    public const uint CPU_FAN = 0x00110013;
    public const uint GPU_FAN = 0x00110014;

    public const uint PERFORMANCE_MODE = 0x00120075;

    public const uint CPU_TEMP = 0x00120094;
    public const uint GPU_TEMP = 0x00120097;

    public const uint SYSTEM_PPT = 0x001200A0;

    public const uint CPU_PPT = 0x001200B0;
    public const uint CPU_SLOW_PPT = 0x001200B1;
    public const uint CPU_FAST_PPT = 0x001200C1;

    private IntPtr handle;

    public AllyACPI()
    {
        handle = new IntPtr(-1);
    }

    public void Open()
    {
        if (handle != new IntPtr(-1)) return;

        handle = NativeMethods.CreateFile(
            FILENAME,
            NativeMethods.GENERIC_READ | NativeMethods.GENERIC_WRITE,
            NativeMethods.FILE_SHARE_READ | NativeMethods.FILE_SHARE_WRITE,
            IntPtr.Zero,
            NativeMethods.OPEN_EXISTING,
            NativeMethods.FILE_ATTRIBUTE_NORMAL,
            IntPtr.Zero
        );

        if (handle == new IntPtr(-1))
        {
            throw new Exception("Can't connect to ACPI");
        }
    }

    public void Close()
    {
        if (handle == new IntPtr(-1)) return;

        NativeMethods.CloseHandle(handle);
        handle = new IntPtr(-1);
    }

    public void Dispose() => Close();

    public byte[] DeviceInit()
    {
        var args = new byte[8];
        return CallMethod(INIT, args);
    }

    public int DeviceGet(uint DeviceID)
    {
        var args = new byte[8];
        BitConverter.GetBytes(DeviceID).CopyTo(args, 0);
        var status = CallMethod(DSTS, args);

        return BitConverter.ToInt32(status, 0) - 65536;
    }

    public byte[] DeviceGetBuffer(uint DeviceID, uint Status)
    {
        byte[] args = new byte[8];
        BitConverter.GetBytes(DeviceID).CopyTo(args, 0);
        BitConverter.GetBytes(Status).CopyTo(args, 4);

        return CallMethod(DSTS, args);
    }

    public int DeviceSet(uint DeviceID, uint Status)
    {
        var args = new byte[8];
        BitConverter.GetBytes(DeviceID).CopyTo(args, 0);
        BitConverter.GetBytes(Status).CopyTo(args, 4);
        var status = CallMethod(DEVS, args);

        return BitConverter.ToInt32(status, 0);
    }


    public int DeviceSet(uint DeviceID, byte[] Params)
    {
        var args = new byte[4 + Params.Length];
        BitConverter.GetBytes(DeviceID).CopyTo(args, 0);
        Params.CopyTo(args, 4);
        var status = CallMethod(DEVS, args);

        return BitConverter.ToInt32(status, 0);
    }


    private void Control(uint dwIoControlCode, byte[] lpInBuffer, byte[] lpOutBuffer)
    {

        uint lpBytesReturned = 0;
        NativeMethods.DeviceIoControl(
            handle,
            dwIoControlCode,
            lpInBuffer,
            (uint)lpInBuffer.Length,
            lpOutBuffer,
            (uint)lpOutBuffer.Length,
            ref lpBytesReturned,
            IntPtr.Zero
        );
    }

    private byte[] CallMethod(uint MethodID, byte[] args)
    {
        var acpiBuf = new byte[8 + args.Length];
        var outBuffer = new byte[16];

        BitConverter.GetBytes((uint)MethodID).CopyTo(acpiBuf, 0);
        BitConverter.GetBytes((uint)args.Length).CopyTo(acpiBuf, 4);
        Array.Copy(args, 0, acpiBuf, 8, args.Length);

        Control(CONTROL_CODE, acpiBuf, outBuffer);

        return outBuffer;
    }
}
