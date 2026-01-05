namespace i8080_emulator.Executing;
using Computing;
using Signaling;

public partial class DataPath
{
    public void ControlALU()
    {
        if(signals.AluOperation is not { })
            return;
        
        var nullable = signals.AluOperation!.Value;
        
        ALUInput input = new ALUInput
        {
            ALUOperation = nullable,
            CR = (byte)(FLAGS & (byte)ALUFlags.Carry) == 1,
            
            A = DataLatchers[nullable.A].Get(),
            B = nullable.B != DataDriver.NONE ? 
                DataDrivers[nullable.B].Get() : (byte)1
        };

        ALUOutput output = ALU.Compute(input);
        
        DBUS.Set(output.Result);

        ALUFlags mask = ALUModel.FlagMasks[nullable.FlagMask];

        FLAGS = (byte)((FLAGS & (byte)~mask) | (output.Result & (byte)mask));
    }
}