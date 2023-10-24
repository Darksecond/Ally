namespace Ally.ACPI;

public static class Temperature
{
    public static int GetCpuTemperature(AllyACPI acpi) => acpi.DeviceGet(AllyACPI.CPU_TEMP);
    public static int GetGpuTemperature(AllyACPI acpi) => acpi.DeviceGet(AllyACPI.GPU_TEMP);
}
