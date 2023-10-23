namespace Ally.HID;

public struct Assignment
{
    public Mapping Primary { get; set; }
    public Mapping Secondary { get; set; }

    public Assignment(Mapping primary, Mapping secondary)
    {
        this.Primary = primary;
        this.Secondary = secondary;
    }
}
