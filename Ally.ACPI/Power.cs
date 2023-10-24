namespace Ally.ACPI;

public static class Power
{
    public static void SetPowerProfile(AllyACPI acpi, PowerProfile profile) => acpi.DeviceSet(AllyACPI.PERFORMANCE_MODE, (uint)profile);
    public static void SetSystemPPT(AllyACPI acpi, uint value) => acpi.DeviceSet(AllyACPI.SYSTEM_PPT, value);
    public static void SetCpuPPT(AllyACPI acpi, uint value) => acpi.DeviceSet(AllyACPI.CPU_PPT, value);
    public static void SetSlowPPT(AllyACPI acpi, uint value) => acpi.DeviceSet(AllyACPI.CPU_SLOW_PPT, value);
    public static void SetFastPPT(AllyACPI acpi, uint value) => acpi.DeviceSet(AllyACPI.CPU_FAST_PPT, value);
}
