namespace i8080_emulator.Executing;
using Computing;
using Signaling;

public partial class DataPath
{
    public void ResolveALU()
    {
        if(signals.AluOperation is not { })
            return;
        
        var nullable = signals.AluOperation!.Value;
        
        ALUInput input = new ALUInput
        {
            ALUOperation = nullable,
            CR = (byte)(FLAGS & (byte)ALUFlags.Carry) == 1,
            A = nullable.A == DataLatcher.A ? A : TMP,
            B = nullable.B != DataDriver.NONE ? TMP : (byte)1
        };

        ALUOutput output = ALU.Compute(input);
        
        DBUS.Set(output.Result);

        FLAGS = FLAGS_LATCH(output.Flags, 
            ALUModel.FlagMasks[nullable.FlagMask]);
    }

    private byte FLAGS_LATCH(byte newFlags, ALUFlags mask)
    {
        return (byte)((FLAGS & (byte)~mask) | (newFlags & (byte)mask));
    }
}