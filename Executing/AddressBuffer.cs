namespace i8080_emulator.Executing;
using Signaling;

public partial class DataPath
{
    public void AddressBuffer()
    {
        if(signals.AddressDriver == AddressDriver.NONE)
            return;
        
        // PROGRAM COUNTER
        if (signals.AddressDriver == AddressDriver.PC)
        {
            ABUS_L.Set(Registers[R.PC_L].Get());
            ABUS_H.Set(Registers[R.PC_H].Get());
            return;
        }
        
        // TEMP ADDRESS REGISTER
        if (signals.AddressDriver == AddressDriver.HL)
        {
            ABUS_L.Set(Registers[R.L].Get());
            ABUS_H.Set(Registers[R.H].Get());
            return;
        }
    }
}