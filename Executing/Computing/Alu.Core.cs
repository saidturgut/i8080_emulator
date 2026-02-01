namespace intel8080.Executing.Computing;

public partial class Alu
{
    private static AluOutput NONE(AluInput input) => new();

    private static AluOutput ADD(AluInput input)
    {
        var result = input.A + input.B + input.C;

        AluOutput output = new()
            { Result = (byte)result };

        if ((input.A & 0xF) + (input.B & 0xF) + input.C > 0xF) 
            output.Flags |= (byte)Flag.Auxiliary;
        if (result > 0xFF) 
            output.Flags |= (byte)Flag.Carry;
        
        return output;
    }
    private static AluOutput SUB(AluInput input)
    {
        var result = input.A + (~input.B & 0xFF) + (1 - input.C);

        AluOutput output = new()
            { Result = (byte)result };

        if ((input.A & 0xF) < (input.B & 0xF) + input.C)
            output.Flags |= (byte)Flag.Auxiliary;
        if (input.A < input.B + input.C)
            output.Flags |= (byte)Flag.Carry;
        
        return output;
    }
    
    private static AluOutput AND(AluInput input)
    {
        AluOutput output = new()
            { Result = (byte)(input.A & input.B), };
        
        output.Flags |= (byte)Flag.Auxiliary;
        return output;
    }
    private static AluOutput XOR(AluInput input) => new()
    {
        Result = (byte)(input.A ^ input.B) 
    };
    private static AluOutput OR(AluInput input) => new()
    {
        Result = (byte)(input.A | input.B)
    };

    private static AluOutput INC(AluInput input) 
    { 
        input.B = 1; return ADD(input); 
    }
    private static AluOutput DEC(AluInput input)
    { 
        input.B = 1; return SUB(input); 
    }
}