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

        ALUInput input = new ALUInput(
            nullable, // ALU OPERATION
            DataLatchers[nullable.A].Get(), // A
            nullable.B != DataDriver.NONE ? DataDrivers[nullable.B].Get() : (byte)1, // B
            (byte)(FLAGS & (byte)ALUFlags.Carry) == 1); // CARRY
        
        ALUOutput output = ALU.Compute(input);
        
        DBUS.Set(output.Result);

        ALUFlags mask = ALUModel.FlagMasks[nullable.FlagMask];

        FLAGS = (byte)((FLAGS & (byte)~mask) | (output.Result & (byte)mask));
    }
}