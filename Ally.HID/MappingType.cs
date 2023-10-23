namespace Ally.HID;

public enum MappingType : byte
{
    Disabled = 0x00,
    Controller = 0x01,
    Keyboard = 0x02,
    Mouse = 0x03,
    Macro = 0x04,
    Action = 0x05,
}
