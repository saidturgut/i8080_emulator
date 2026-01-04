namespace i8080_emulator.Executing.Computing;

public class ALU
{
    public ALUOutput Compute(ALUInput input)
    {
        ALUOutput output = new ALUOutput();
        
        switch (input.ALUOperation.Operation)
        {
            case Operation.ADD:
            {
                byte carry = (byte)(input.CR ? 1 : 0);
                int result = input.A + input.B + carry;
                
                if (((byte)result & 0x80) != 0) output.Flags |= (byte)ALUFlags.Sign;
                if ((byte)result == 0) output.Flags |= (byte)ALUFlags.Zero;
                //0000
                if (((input.A & 0x0F) + (input.B & 0x0F) + carry) > 0x0F) output.Flags |= (byte)ALUFlags.AuxCarry;
                //0000
                if (EvenParity((byte)result)) output.Flags |= (byte)ALUFlags.Parity;
                //1111
                if (result > 0xFF) output.Flags |= (byte)ALUFlags.Carry;

                output.Result = (byte)result;
                
                break;
            }
        }

        return output;
    }

    private bool EvenParity(byte input)
    {
        int ones = 0;
        for (int i = 0; i < 8; i++)
        {
            ones += (input >> i) & 1;
        }

        return (ones & 1) == 0;
    }
}