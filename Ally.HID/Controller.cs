namespace Ally.HID;

public class Controller
{
    /// <summary>
    /// The layout to start with, this controls if the sticks are in xbox or desktop mode.
    /// </summary>
    public DefaultLayout Layout { get; set; }

    public Assignment A { get; set; }
    public Assignment B { get; set; }
    public Assignment X { get; set; }
    public Assignment Y { get; set; }

    public Assignment DPadUp { get; set; }
    public Assignment DPadDown { get; set; }
    public Assignment DPadLeft { get; set; }
    public Assignment DPadRight { get; set; }

    public Assignment LeftTrigger { get; set; }
    public Assignment LeftButton { get; set; }
    public Assignment RightTrigger { get; set; }
    public Assignment RightButton { get; set; }

    public Assignment L3 { get; set; }
    public Assignment R3 { get; set; }
    public Assignment View { get; set; }
    public Assignment Menu { get; set; }

    public Assignment M1 { get; set; }
    public Assignment M2 { get; set; }

    public static Controller GetDefaultXboxController()
    {
        return new Controller
        {
            Layout = DefaultLayout.Xbox,

            A = new Assignment(new Mapping(ControllerCode.A), new Mapping(ActionCode.Screenshot)),
            B = new Assignment(new Mapping(ControllerCode.B), Mapping.WIN_N),
            X = new Assignment(new Mapping(ControllerCode.X), Mapping.WIN_P),
            Y = new Assignment(new Mapping(ControllerCode.Y), new Mapping(ActionCode.BeginRecording)),

            DPadUp = new Assignment(new Mapping(ControllerCode.UP), new Mapping(ActionCode.ShowKeyboard)),
            DPadDown = new Assignment(new Mapping(ControllerCode.DOWN), Mapping.CTRL_SHIFT_ESCAPE),
            DPadLeft = new Assignment(new Mapping(ControllerCode.LEFT), Mapping.WIN_D),
            DPadRight = new Assignment(new Mapping(ControllerCode.RIGHT), Mapping.WIN_TAB),

            LeftTrigger = new Assignment(new Mapping(ControllerCode.LT), Mapping.DISABLED),
            LeftButton = new Assignment(new Mapping(ControllerCode.LB), Mapping.DISABLED),
            RightTrigger = new Assignment(new Mapping(ControllerCode.RT), Mapping.DISABLED),
            RightButton = new Assignment(new Mapping(ControllerCode.RB), Mapping.DISABLED),

            L3 = new Assignment(new Mapping(ControllerCode.L3), Mapping.DISABLED),
            R3 = new Assignment(new Mapping(ControllerCode.R3), Mapping.DISABLED),

            View = new Assignment(new Mapping(ControllerCode.VIEW), Mapping.DISABLED),
            Menu = new Assignment(new Mapping(ControllerCode.MENU), Mapping.DISABLED),

            M1 = new Assignment(new Mapping(KeyboardCode.M1), new Mapping(KeyboardCode.M1)),
            M2 = new Assignment(new Mapping(KeyboardCode.M2), new Mapping(KeyboardCode.M2)),
        };
    }

    public static Controller GetDefaultDesktopController()
    {
        return new Controller
        {
            Layout = DefaultLayout.Desktop,

            RightButton = new Assignment(Mapping.LEFT_CLICK, Mapping.DISABLED),
            RightTrigger = new Assignment(Mapping.RIGHT_CLICK, Mapping.DISABLED),

            LeftTrigger = new Assignment(Mapping.SHIFT_TAB, Mapping.DISABLED),
            LeftButton = new Assignment(new Mapping(KeyboardCode.Tab), Mapping.DISABLED),

            L3 = new Assignment(new Mapping(KeyboardCode.LeftShift), Mapping.DISABLED),
            R3 = new Assignment(new Mapping(MouseCode.Left), Mapping.DISABLED),

            View = new Assignment(new Mapping(ControllerCode.VIEW), Mapping.DISABLED),
            Menu = new Assignment(new Mapping(ControllerCode.MENU), Mapping.DISABLED),

            M1 = new Assignment(new Mapping(KeyboardCode.M1), new Mapping(KeyboardCode.M1)),
            M2 = new Assignment(new Mapping(KeyboardCode.M2), new Mapping(KeyboardCode.M2)),

            A = new Assignment(new Mapping(KeyboardCode.Enter), new Mapping(ActionCode.Screenshot)),
            B = new Assignment(new Mapping(KeyboardCode.Escape), Mapping.WIN_N),
            X = new Assignment(new Mapping(KeyboardCode.PageDown), Mapping.WIN_P),
            Y = new Assignment(new Mapping(KeyboardCode.PageUp), new Mapping(ActionCode.BeginRecording)),

            DPadUp = new Assignment(new Mapping(KeyboardCode.Up), new Mapping(ActionCode.ShowKeyboard)),
            DPadDown = new Assignment(new Mapping(KeyboardCode.Down), Mapping.CTRL_SHIFT_ESCAPE),
            DPadLeft = new Assignment(new Mapping(KeyboardCode.Left), Mapping.WIN_D),
            DPadRight = new Assignment(new Mapping(KeyboardCode.Right), Mapping.WIN_TAB),
        };
    }

    public void Apply()
    {
        var device = AllyUSB.GetDevice() ?? throw new Exception("No device found");

        AllyUSB.SendDefaultLayout(device, this.Layout);

        AllyUSB.SendNext(device);
        AllyUSB.SendMapping(device, ButtonCode.DpadUp_DpadDown, DPadUp, DPadDown);

        AllyUSB.SendNext(device);
        AllyUSB.SendMapping(device, ButtonCode.DpadLeft_DpadRight, DPadLeft, DPadRight);

        AllyUSB.SendApply(device);
    }
}
