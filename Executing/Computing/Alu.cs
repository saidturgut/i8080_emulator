namespace i8080_emulator.Executing.Computing;

public partial class Alu
{
    public AluOutput Compute(AluInput input, Operation operation)
    {
        AluOutput output = Operations[(byte)operation](input);
        
        if ((output.Result & 0x80) != 0) output.Flags |= (byte)PswFlag.Sign;
        if (output.Result == 0) output.Flags |= (byte)PswFlag.Zero;
        
        byte ones = 0;
        for (byte i = 0; i < 8; i++) 
            ones += (byte)((output.Result >> i) & 1);
        
        if (ones % 2 == 0) output.Flags |= (byte)PswFlag.Parity;
        
        return output;
    }
    
    private static readonly Func<AluInput, AluOutput>[] Operations =
    [
        NONE, ADD ,SUB, AND, XOR, OR,
    ];
}

public enum Operation
{
    NONE, ADD, SUB, AND, XOR, OR,
}
