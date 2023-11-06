namespace Ally.HID;

public readonly struct Mapping
{
    public MappingType Type { get; }
    public ControllerCode Controller { get; }
    public KeyboardCode Keyboard { get; }
    public ActionCode Action { get; }
    public MouseCode Mouse { get; }
    public KeyboardCode[]? Macro { get; }

    public Mapping()
    {
        this.Macro = Array.Empty<KeyboardCode>();
    }

    public Mapping(ControllerCode controller)
    {
        this.Type = MappingType.Controller;
        this.Controller = controller;
        this.Macro = Array.Empty<KeyboardCode>();
    }

    public Mapping(KeyboardCode keyboard)
    {
        this.Type = MappingType.Keyboard;
        this.Keyboard = keyboard;
        this.Macro = Array.Empty<KeyboardCode>();
    }

    public Mapping(MouseCode mouse)
    {
        this.Type = MappingType.Mouse;
        this.Mouse = mouse;
        this.Macro = Array.Empty<KeyboardCode>();
    }

    public Mapping(ActionCode action)
    {
        this.Type = MappingType.Action;
        this.Action = action;
        this.Macro = Array.Empty<KeyboardCode>();
    }

    public Mapping(KeyboardCode[] macro)
    {
        this.Type = MappingType.Macro;
        this.Macro = macro;
    }

    public static readonly Mapping DISABLED = new();

    public static readonly Mapping LEFT_CLICK = new(MouseCode.Left);

    public static readonly Mapping RIGHT_CLICK = new(MouseCode.Right);

    public static readonly Mapping CTRL_SHIFT_ESCAPE = new(new KeyboardCode[] { KeyboardCode.LeftControl, KeyboardCode.LeftShift, KeyboardCode.Escape });

    /// <summary>
    ///  Show desktop
    /// </summary>
    public static readonly Mapping WIN_D = new(new KeyboardCode[] { KeyboardCode.Windows, KeyboardCode.D });

    /// <summary>
    /// Task viewer
    /// </summary>
    public static readonly Mapping WIN_TAB = new(new KeyboardCode[] { KeyboardCode.Windows, KeyboardCode.Tab });

    /// <summary>
    /// Notification Center
    /// </summary>
    public static readonly Mapping WIN_N = new(new KeyboardCode[] { KeyboardCode.Windows, KeyboardCode.N });

    /// <summary>
    /// Projection Mode
    /// </summary>
    public static readonly Mapping WIN_P = new(new KeyboardCode[] { KeyboardCode.Windows, KeyboardCode.P });

    public static readonly Mapping SHIFT_TAB = new(new KeyboardCode[] { KeyboardCode.LeftShift, KeyboardCode.Tab });
}
