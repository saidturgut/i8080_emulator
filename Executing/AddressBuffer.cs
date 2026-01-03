namespace i8080_emulator.Executing;
using Signals;

public partial class DataPath
{
    public void AddressBuffer()
    {
        if(signals.AddressDriver == AddressDriver.NONE)
            return;
        
        // PROGRAM COUNTER
        if (signals.AddressDriver == AddressDriver.PC)
        {
            ABUS_L.Set(PC_L);
            ABUS_H.Set(PC_H);
        }
        
        // TEMP ADDRESS REGISTER
        if (signals.AddressDriver == AddressDriver.HL)
        {
            ABUS_L.Set(L);
            ABUS_H.Set(H);
        }
    }
}