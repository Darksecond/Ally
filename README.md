# Ally
Tools for the ROG Ally

# Ally.HID

This allows for full remapping of the ally's controller. This has the same features as ACSE has.
Currently deadzone configuration is not supported, I will look into this in the future.

# Ally.RyzenMaster

This is a small interface to the Ryzen Master Monitoring SDK.
Right now you can get the current fPPT value for the cpu.
You need admin rights to succesfully run this.
You need the AMD Ryzen Master driver installed, can be found in vendor as well.

The driver can be installed with `DriverUtility.bat -i` in a terminal with admin rights.

# Vendored DLL's
In vendor you can find the following files from vendors:

- Device.dll from AMD
- Platform.dll from AMD
- 

# Command line Tool

```
Description:
  A tool to help set system settings.

Usage:
  AllyRemapper [options]

Options:
  --profile <profile>                The controller profile to apply
  --set-system-tdp <set-system-tdp>  Set the total system TDP
  --set-cpu-tdp <set-cpu-tdp>        Set the cpu TDP
  --set-slow-tdp <set-slow-tdp>      Set the slow TDP
  --set-fast-tdp <set-fast-tdp>      Set the fast TDP
  --set-power-profile <Balanced|Manual|Silent|Turbo>  Set the power profile
  --version                          Show version information
  -?, -h, --help                     Show help and usage information
```