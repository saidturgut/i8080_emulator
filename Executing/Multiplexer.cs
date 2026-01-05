namespace i8080_emulator.Executing;
using Signaling;

public partial class DataPath
{
    public void MultiplexerDrive()
    {        
        if(signals.DataDriver == DataDriver.NONE)
            return;
        
        // RANDOM ACCESS MEMORY
        if (signals.DataDriver == DataDriver.RAM)
        {
            RAM.Read(ABUS_H, ABUS_L, DBUS);
            return;
        }

        if (DataDrivers.ContainsKey(signals.DataDriver))
        {
            DBUS.Set(DataDrivers[signals.DataDriver].Get());
            return;
        }
    }
    
    public void MultiplexerLatch()
    {        
        if(signals.DataLatcher == DataLatcher.NONE)
            return;
        
        // RANDOM ACCESS MEMORY
        if (signals.DataLatcher == DataLatcher.RAM)
        {
            RAM.Write(ABUS_H, ABUS_L, DBUS);
            return;
        }

        if (DataLatchers.ContainsKey(signals.DataLatcher))
        {
            DataLatchers[signals.DataLatcher].Set(DBUS.Get());
            return;
        }
    }
}