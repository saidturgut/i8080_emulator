namespace i8080_emulator.Executing;

public partial class DataPath
{
    private readonly Tty Tty = new ();

    public void HostInput() => Tty.HostInput();
    
    public void IoControl()
    {
        switch (signals.IoAction)
        {
            case IoAction.NONE:
            {
                return;
            }
            case IoAction.INPUT:
            {
                switch (AbusL.Get())
                {
                    case 0: Dbus.Set(Tty.ReadStatus()); break;
                    case 1: Dbus.Set(Tty.ReadData()); break;
                    default: throw new Exception($"INVALID INPUT PORT \"{AbusL.Get()}\"");
                }
                return;
            }
            case IoAction.OUTPUT:
            {
                switch (AbusL.Get())
                {
                    case 1: Tty.WriteData(Dbus.Get()); break;
                    default: throw new Exception($"INVALID OUTPUT PORT \"{AbusL.Get()}\"");
                }
                return;
            }
        }
    }
}

public enum IoAction
{
    NONE, INPUT, OUTPUT,
}
