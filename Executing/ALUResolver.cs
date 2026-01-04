namespace i8080_emulator.Executing;
using Computing;
using Signaling;

public partial class DataPath
{
    public void ResolveALU()
    {
        if(signals.AluOperation.Operation == Operation.NONE)
            return;

        ALUOutput Output = ALU.Compute(new ALUInput
        {
            ALUOperation = signals.AluOperation,
            A = A,
            B = TMP,
            CR = (byte)(FLAGS & (byte)ALUFlags.Carry) == 1 && 
                 signals.AluOperation.CarryIn,
        });

        if (signals.SideEffect != SideEffect.CMP)
            A = Output.Result;

        FLAGS = Output.Flags;
    }
}