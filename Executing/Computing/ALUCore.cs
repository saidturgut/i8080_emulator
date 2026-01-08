namespace i8080_emulator.Executing.Computing;

public partial class ALU
{
    public ALUOutput Compute(ALUInput input)
    {
        ALUOutput output = new ALUOutput();
        int result = 0;
        
        switch (input.Operation)
        {
            case Operation.ADD:
            {
                byte carry = (byte)(input is { FlagCY: true, CarryUser: true } ? 1 : 0);
                result = input.A + input.B + carry;
                
                if (((input.A & 0x0F) + (input.B & 0x0F) + carry) > 0x0F) output.Flags |= (byte)ALUFlag.AuxCarry;
                if (result > 0xFF) output.Flags |= (byte)ALUFlag.Carry;
                break;
            }
            case Operation.SUB:
            {
                byte carry;
                if (input.CarryUser)
                    carry = (byte)(input.FlagCY ? 0 : 1);
                else
                    carry = 1;
                
                result = input.A + (~input.B & 0xFF) + carry;
                
                if ((input.A & 0x0F) - (input.B & 0x0F) - (1 - carry) < 0) output.Flags |= (byte)ALUFlag.AuxCarry;
                if (result >= 0x100) output.Flags |= (byte)ALUFlag.Carry;
                break;
            }
            case Operation.AND:
            {
                result = input.A & input.B;
                
                output.Flags |= (byte)ALUFlag.AuxCarry;
                break;
            }
            case Operation.XOR:
            {
                result = input.A ^ input.B;
                break;
            }
            case Operation.OR:
            {
                result = input.A | input.B;
                break;
            }
        }

        result = (byte)result;
        
        if ((result & 0x80) != 0) output.Flags |= (byte)ALUFlag.Sign;
        if (result == 0) output.Flags |= (byte)ALUFlag.Zero;
        if (EvenParity((byte)result)) output.Flags |= (byte)ALUFlag.Parity;
        
        output.Result = (byte)result;
        return output;
    }
    
    private bool EvenParity(byte result)
    {
        int ones = 0;
        for (int i = 0; i < 8; i++)
        {
            ones += (result >> i) & 1;
        }

        return (ones & 1) == 0;
    }
}