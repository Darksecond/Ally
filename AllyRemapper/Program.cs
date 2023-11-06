using Ally.HID;
using Ally.ACPI;
using Ally.RyzenMaster;
using System.CommandLine;
using System.Text.Json;
using System.Text.Json.Serialization;
using System.Diagnostics;

/*
Controller.GetDefaultDesktopController().Apply();
Console.WriteLine("Applied Desktop Layout");

Console.ReadKey();

Controller.GetDefaultXboxController().Apply();
Console.WriteLine("Applied Xbox Layout");

Console.ReadKey();
*/

/*
using var acpi = new AllyACPI();
acpi.Open();
var cpuTemp = acpi.DeviceGet(AllyACPI.CPU_TEMP);

Console.WriteLine($"CPU Temp: {cpuTemp}");

Console.ReadKey();

acpi.Close();
*/

/*
using var platform = new Platform();
var deviceManager = platform.GetDeviceManager();
var cpu = deviceManager.GetCpu() ?? throw new Exception("No cpu?!");
Console.WriteLine($"count: {cpu.CoreCount} tdp: {cpu.FastPPTvalue}");
*/

public static class Program
{
    static async Task Main(string[] args)
    {
        using var acpi = new AllyACPI();

        var profile = new Option<FileInfo?>(name: "--profile", description: "The controller profile to apply");
        var systemTDP = new Option<uint?>(name: "--set-system-tdp", description: "Set the total system TDP");
        var cpuTDP = new Option<uint?>(name: "--set-cpu-tdp", description: "Set the cpu TDP");
        var slowTDP = new Option<uint?>(name: "--set-slow-tdp", description: "Set the slow TDP");
        var fastTDP = new Option<uint?>(name: "--set-fast-tdp", description: "Set the fast TDP");

        var rootCommand = new RootCommand("A tool to help set system settings.")
        {
            profile,
            systemTDP,
            cpuTDP,
            slowTDP,
            fastTDP,
        };

        rootCommand.SetHandler((profile, systemTDP, cpuTDP, slowTDP, fastTDP) => 
        {
            if (profile != null)
            {
                var options = new JsonSerializerOptions
                {
                    WriteIndented = true,
                    Converters = {
                        new JsonStringEnumConverter()
                    },
                };
                
                if (!profile.Exists)
                {
                    Console.WriteLine("The profile file does not exist");
                    return;
                }

                var controller = JsonSerializer.Deserialize<Controller>(profile.OpenRead(), options);
                if (controller == null)
                {
                    Console.WriteLine("Could not deserialize profile");
                    return;
                }

                Console.WriteLine("Applying controller profile");
                controller.Apply();
            }

            if (systemTDP.HasValue)
            {
                acpi.Open();
                Console.WriteLine($"Setting system TDP to {systemTDP.Value}");
                acpi.DeviceSet(AllyACPI.SYSTEM_PPT, systemTDP.Value);
            }

            if (cpuTDP.HasValue)
            {
                acpi.Open();
                Console.WriteLine($"Setting cpu TDP to {cpuTDP.Value}");
                acpi.DeviceSet(AllyACPI.CPU_PPT, cpuTDP.Value);
            }

            if (slowTDP.HasValue)
            {
                acpi.Open();
                Console.WriteLine($"Setting slow TDP to {slowTDP.Value}");
                acpi.DeviceSet(AllyACPI.CPU_SLOW_PPT, slowTDP.Value);
            }

            if (fastTDP.HasValue)
            {
                acpi.Open();
                Console.WriteLine($"Setting fast TDP to {fastTDP.Value}");
                acpi.DeviceSet(AllyACPI.CPU_FAST_PPT, fastTDP.Value);
            }
        }, profile, systemTDP, cpuTDP, slowTDP, fastTDP);

        await rootCommand.InvokeAsync(args);
    }
}