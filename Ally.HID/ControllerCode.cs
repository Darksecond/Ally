namespace Ally.HID;

public enum ControllerCode : byte
{
    Disabled = 0x00,

    A = 0x01,
    B = 0x02,
    X = 0x03,
    Y = 0x04,

    LB = 0x05,
    RB = 0x06,

    L3 = 0x07,
    R3 = 0x08,

    UP = 0x09,
    DOWN = 0x0A,
    LEFT = 0x0B,
    RIGHT = 0x0C,

    LT = 0x0D,
    RT = 0x0E,

    VIEW = 0x11,
    MENU = 0x12,
}
