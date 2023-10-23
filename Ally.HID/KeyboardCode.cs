namespace Ally.HID;

public enum KeyboardCode : byte
{
    Disabled = 0x00,

    Tab = 0x0D,

    D = 0x23,

    N = 0x31,

    P = 0x4D,

    Enter = 0x5A,
    Backspace = 0x66,
    Escape = 0x76,

    Windows = 0x82,
    
    LeftShift = 0x88,
    RightShift = 0x89,
    LeftAlt = 0x8A,
    RightAlt = 0x8B,
    LeftControl = 0x8C,
    RightControl = 0x8D,

    // Special keycodes used by the M1/M2 keys
    M2 = 0x8E,
    M1 = 0x8F,

    PageUp = 0x96,
    PageDown = 0x97,
    Up = 0x98,
    Down = 0x99,
    Left = 0x9A,
    Right = 0x9B,

    //TODO All the other keycodes.
}
