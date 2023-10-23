using Ally.HID;

Console.WriteLine("Hello, World!");

AllyUSB.SendDefaultLayout(DefaultLayout.Desktop);
AllyUSB.SendNext();
AllyUSB.SendMapping(ButtonCode.LT_RT, new Mapping[] { Mapping.LEFT_CLICK, Mapping.DISABLED, Mapping.RIGHT_CLICK, Mapping.DISABLED, });
AllyUSB.SendNext();
AllyUSB.SendMapping(ButtonCode.LB_RB, new Mapping[] { Mapping.DISABLED, Mapping.DISABLED, Mapping.DISABLED, Mapping.DISABLED, });

Console.ReadKey();

AllyUSB.SendDefaultLayout(DefaultLayout.Xbox);

Console.ReadKey();