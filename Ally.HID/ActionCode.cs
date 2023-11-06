namespace Ally.HID;

public enum ActionCode : byte
{
    Disabled = 0x00,
    MicrophoneOff = 0x01,
    VolumeDown = 0x02,
    VolumeUp = 0x03,
    TaskManager = 0x04,
    Screenshot = 0x16,
    XboxKey = 0x13,
    ShowKeyboard = 0x19,
    ShowDesktop = 0x1C,
    BeginRecording = 0x1E,
}
