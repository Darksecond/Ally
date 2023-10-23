using Ally.HID;

Controller.GetDefaultDesktopController().Apply();
Console.WriteLine("Applied Desktop Layout");

Console.ReadKey();

Controller.GetDefaultXboxController().Apply();
Console.WriteLine("Applied Xbox Layout");

Console.ReadKey();