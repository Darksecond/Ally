namespace Ally.ACPI;

public enum PowerProfile : uint
{
    Balanced = 0,
    Turbo = 1,
    Silent = 2,

    Manual = 4, // Not sure about this one
}
