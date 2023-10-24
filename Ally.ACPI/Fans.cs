namespace Ally.ACPI;

public static class Fans
{
    public static int GetCpuFanSpeed(AllyACPI acpi) => acpi.DeviceGet(AllyACPI.CPU_FAN);
    public static int GetGpuFanSpeed(AllyACPI acpi) => acpi.DeviceGet(AllyACPI.GPU_FAN);
}
