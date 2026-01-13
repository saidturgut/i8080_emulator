namespace i8080_emulator.Executing;
using Signaling;

public partial class DataPath
{
    public void HostInput() => IO.HostInput();

    public void IOControl()
    {
        if (signals.SideEffect == SideEffect.NONE)
        {
            return;
        }

        if (signals.SideEffect == SideEffect.IO_READ)
        {
            IO.Read(ABUS_L, DBUS);
        }

        if (signals.SideEffect == SideEffect.IO_WRITE)
        {
            IO.Write(ABUS_L, DBUS);
        }
    }
}