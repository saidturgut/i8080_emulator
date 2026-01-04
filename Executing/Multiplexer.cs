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
            RAM.Read(ABUS_H, ABUS_L, DBUS);
        
        // TEMP DATA REGISTER
        if (signals.DataDriver == DataDriver.TMP)
            DBUS.Set(TMP);
        
        // TEMP ADDRESS REGISTER
        if(signals.DataDriver == DataDriver.H)
            DBUS.Set(H);
        if(signals.DataDriver == DataDriver.L)
            DBUS.Set(L);

        // GENERAL REGISTERS
        if (signals.DataDriver == DataDriver.A)
            DBUS.Set(A);
        if (signals.DataDriver == DataDriver.B)
            DBUS.Set(B);
        if (signals.DataDriver == DataDriver.C)
            DBUS.Set(C);
        if (signals.DataDriver == DataDriver.D)
            DBUS.Set(D);
        if (signals.DataDriver == DataDriver.E)
            DBUS.Set(E);
    }
    
    public void MultiplexerLatch()
    {        
        if(signals.DataLatcher == DataLatcher.NONE)
            return;
        
        // RANDOM ACCESS MEMORY
        if (signals.DataLatcher == DataLatcher.RAM)
            RAM.Write(ABUS_H, ABUS_L, DBUS);
        if (signals.DataLatcher == DataLatcher.IR)
            IR = DBUS.Get();
        
        // TEMP DATA REGISTER
        if (signals.DataLatcher == DataLatcher.TMP)
            TMP = DBUS.Get();
        
        // TEMP ADDRESS REGISTER
        if(signals.DataLatcher == DataLatcher.H)
            H = DBUS.Get();
        if(signals.DataLatcher == DataLatcher.L)
            L = DBUS.Get();

        // GENERAL REGISTERS
        if (signals.DataLatcher == DataLatcher.A)
            A = DBUS.Get();
        if (signals.DataLatcher == DataLatcher.B)
            B = DBUS.Get();
        if (signals.DataLatcher == DataLatcher.C)
            C = DBUS.Get();
        if (signals.DataLatcher == DataLatcher.D)
            D = DBUS.Get();
        if (signals.DataLatcher == DataLatcher.E)
            E = DBUS.Get();
    }
}