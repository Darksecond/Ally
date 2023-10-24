using Ally.HID;
using Ally.ACPI;

/*
Controller.GetDefaultDesktopController().Apply();
Console.WriteLine("Applied Desktop Layout");

Console.ReadKey();

Controller.GetDefaultXboxController().Apply();
Console.WriteLine("Applied Xbox Layout");

Console.ReadKey();
*/

using var acpi = new AllyACPI();
acpi.Open();
var cpuTemp = acpi.DeviceGet(AllyACPI.CPU_TEMP);

Console.WriteLine($"CPU Temp: {cpuTemp}");

Console.ReadKey();

acpi.Close();