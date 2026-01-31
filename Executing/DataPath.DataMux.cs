namespace i8080_emulator.Executing;
using Components;
using Signaling;

public partial class DataPath
{
    private readonly ClockedRegister[] Registers = new ClockedRegister[17];
    
    public void DataDrive()
    {
        switch (signals.DataDriver)
        {
            case Register.NONE:
                if (signals.IoAction is IoAction.NONE)
                    Dbus.Set(signals.EncodeLatch);
                return;
            case Register.RAM:
                Ram.Read(AbusL, AbusH, Dbus);
                return;
            default:
                Dbus.Set(Reg(signals.DataDriver).Get());
                return;
        }
    }
    
    public void DataLatch()
    {
        switch (signals.DataLatcher)
        {
            case Register.NONE:
                return;
            case Register.RAM:
                Ram.Write(AbusL, AbusH, Dbus);
                break;
            default:
                Reg(signals.DataLatcher).Set(Dbus.Get());
                break;
        }
    }
}