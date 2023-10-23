using HidLibrary;

namespace Ally.HID;

public class AllyUSB
{
    public const byte REPORT_ID = 0x5A;
    public const int ASUS_ID = 0x0b05;
    public readonly static int[] DEVICE_IDS = { 0x1ABE };

    public static HidDevice? GetDevice()
    {
        var devices = HidDevices.Enumerate(ASUS_ID, DEVICE_IDS);
        HidDevice? selectedDevice = null;
        foreach (var device in devices)
        {
            if (device.ReadFeatureData(out byte[] data, REPORT_ID))
            {
                selectedDevice = device;
                //Console.WriteLine($"HID {device.Capabilities.FeatureReportByteLength} {device.DevicePath}");
            }
        }

        return selectedDevice;
    }

    /// <summary>
    /// This sets the controller to a default layout, either desktop or xbox.
    /// </summary>
    /// <param name="layout">The layout to apply.</param>
    public static bool SendDefaultLayout(HidDevice device, DefaultLayout layout)
    {
        var data = new byte[]
        {
            REPORT_ID,
            0xD1, 0x01, 0x01, (byte)layout, 0x00, 0x00, 0x00, 0x00, 0x00, 0x00, 0x00, 0x00, 0x00, 0x00, 0x00,
            0x00, 0x00, 0x00, 0x00, 0x00, 0x00, 0x00, 0x00, 0x00, 0x00, 0x00, 0x00, 0x00, 0x00, 0x00, 0x00,
            0x00, 0x00, 0x00, 0x00, 0x00, 0x00, 0x00, 0x00, 0x00, 0x00, 0x00, 0x00, 0x00, 0x00, 0x00, 0x00,
            0x00, 0x00, 0x00, 0x00, 0x00, 0x00, 0x00, 0x00, 0x00, 0x00, 0x00, 0x00, 0x00, 0x00, 0x00, 0x00,
        };

        return device.WriteFeatureData(data);
    }

    //TODO Not sure about what this does either.
    public static bool SendNext(HidDevice device)
    {
        var data = new byte[]
        {
            REPORT_ID,
            0xD1, 0x0A, 0x01, 0x00, 0x00, 0x00, 0x00, 0x00, 0x00, 0x00, 0x00, 0x00, 0x00, 0x00, 0x00,
            0x00, 0x00, 0x00, 0x00, 0x00, 0x00, 0x00, 0x00, 0x00, 0x00, 0x00, 0x00, 0x00, 0x00, 0x00, 0x00,
            0x00, 0x00, 0x00, 0x00, 0x00, 0x00, 0x00, 0x00, 0x00, 0x00, 0x00, 0x00, 0x00, 0x00, 0x00, 0x00,
            0x00, 0x00, 0x00, 0x00, 0x00, 0x00, 0x00, 0x00, 0x00, 0x00, 0x00, 0x00, 0x00, 0x00, 0x00, 0x00,
        };

        return device.WriteFeatureData(data);
    }

    //TODO At least, I think it's an apply
    public static bool SendApply(HidDevice device)
    {
        var data = new byte[]
        {
            REPORT_ID,
            0xD1, 0x0F, 0x20, 0x00, 0x00, 0x00, 0x00, 0x00, 0x00, 0x00, 0x00, 0x00, 0x00, 0x00, 0x00,
            0x00, 0x00, 0x00, 0x00, 0x00, 0x00, 0x00, 0x00, 0x00, 0x00, 0x00, 0x00, 0x00, 0x00, 0x00, 0x00,
            0x00, 0x00, 0x00, 0x00, 0x00, 0x00, 0x00, 0x00, 0x00, 0x00, 0x00, 0x00, 0x00, 0x00, 0x00, 0x00,
            0x00, 0x00, 0x00, 0x00, 0x00, 0x00, 0x00, 0x00, 0x00, 0x00, 0x00, 0x00, 0x00, 0x00, 0x00, 0x00,
        };

        return device.WriteFeatureData(data);
    }

    /// <summary>
    /// Apply a mapping to a set of buttons.
    /// You need to provide 4 sets of mappings: first button primary, first button secondary, second button primary, second button secondary.
    /// </summary>
    /// <param name="buttons">The button pair to map.</param>
    /// <param name="mappings">The 4 mappings to apply.</param>
    public static bool SendMapping(HidDevice device, ButtonCode buttons, Assignment first, Assignment second)
    {
        var data = new byte[64];
        Array.Clear(data);

        var index = 0;

        data[index++] = 0x5A;
        data[index++] = 0xD1;
        data[index++] = 0x02;
        data[index++] = (byte)buttons;
        data[index++] = 0x2C;

        AddMapping(first.Primary, ref index, data);
        AddMapping(first.Secondary, ref index, data);
        AddMapping(second.Primary, ref index, data);
        AddMapping(second.Secondary, ref index, data);

        return device.WriteFeatureData(data);
    }

    private static void AddMapping(Mapping mapping, ref int index, byte[] data)
    {
        var macroLength = mapping.Macro?.Length ?? 0;
        if (macroLength > 5) throw new Exception("You can specify up to 5 items in a macro");

        data[index++] = (byte)mapping.Type;
        data[index++] = (byte)mapping.Controller;
        data[index++] = (byte)mapping.Keyboard;
        data[index++] = (byte)mapping.Action;
        data[index++] = (byte)mapping.Mouse;
        data[index++] = (byte)macroLength;
        if (macroLength > 0)
        {
            foreach (var button in mapping.Macro!)
            {
                data[index++] = (byte)button;
            }
        }

        index += 5 - macroLength;
    }
}