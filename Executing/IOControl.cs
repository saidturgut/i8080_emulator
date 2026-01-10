namespace i8080_emulator.Executing;
using InputOutput.Devices;
using Signaling;

public partial class DataPath
{
    public void HostInput() 
        => IO.Terminal.HostInput();
    
    public void IOControl()
    {
        IO.Terminal.HostInput();
        
        if(signals.SideEffect == SideEffect.NONE)
            return;

        if (signals.SideEffect == SideEffect.IO_READ)
            IO.Read(ABUS_L, DBUS);

        if (signals.SideEffect == SideEffect.IO_WRITE)
            IO.Write(ABUS_L, DBUS);
    }
}