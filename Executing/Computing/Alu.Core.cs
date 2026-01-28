namespace i8080_emulator.Executing.Computing;

public partial class Alu
{
    private static AluOutput NONE(AluInput input) => new();

    private static AluOutput ADD(AluInput input)
    {
        AluOutput output = new();
        
        var result = input.A + input.B + input.C;
        output.Result = (byte)result;
     
        if ((input.A & 0xF) + (input.B & 0xF) + input.C > 0xF) 
            output.Flags |= (byte)PswFlag.Auxiliary;
        if (result > 0xFF) 
            output.Flags |= (byte)PswFlag.Carry;
        
        return output;
    }
    private static AluOutput SUB(AluInput input)
    {
        AluOutput output = new();
        
        var result = input.A + (~input.B & 0xFF) + (1 - input.C);
        output.Result = (byte)result;

        if ((input.A & 0xF) < (input.B & 0xF) + input.C)
            output.Flags |= (byte)PswFlag.Auxiliary;
        if (input.A < input.B + input.C)
            output.Flags |= (byte)PswFlag.Carry;
        
        return output;
    }
    
    private static AluOutput AND(AluInput input)
    {
        AluOutput output = new()
            { Result = (byte)(input.A & input.B), };
        
        output.Flags |= (byte)PswFlag.Auxiliary;
        return output;
    }
    private static AluOutput XOR(AluInput input) => new()
        { Result = (byte)(input.A ^ input.B) };
    private static AluOutput OR(AluInput input) => new()
        { Result = (byte)(input.A | input.B) };
}