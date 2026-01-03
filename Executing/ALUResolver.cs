namespace i8080_emulator.Executing;
using Computing;
using Signaling;

public partial class DataPath
{
    public ALUOutput Output;
    
    public void ResolveALU()
    {
        if(signals.AluOperation.Operation == Operation.NONE)
            return;

        ALUInput input = new ALUInput
        {
            ALUOperation = signals.AluOperation,
            A = A,
            B = B,
            CR = signals.AluOperation.CarryIn,
        };
        
        Output = ALU.Compute(input);
    }
}