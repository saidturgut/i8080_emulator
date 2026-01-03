namespace i8080_emulator.Executing.Computing;

public class ALU
{
    public ALUOutput Compute(ALUInput input)
    {
        ALUOutput output = new ALUOutput();
        
        switch (input.Operation)
        {
            case ALUOperation.ADD:
            {
                byte carry = (byte)(input.CR ? 1 : 0);
                int result = input.A + input.B + carry;
                
                if ((result & 0x80) != 0) output.Flags |= (byte)ALUFlags.Sign;
                if (result == 0) output.Flags |= (byte)ALUFlags.Zero;
                //0000
                if (((input.A & 0X0F) + (input.B & 0X0F) + carry) != 0) output.Flags |= (byte)ALUFlags.AuxCarry;
                //0000
                if (Parity((byte)result)) output.Flags |= (byte)ALUFlags.Parity;
                //1111
                if (result > 0xFF) output.Flags |= (byte)ALUFlags.Carry;

                output.Result = (byte)result;
                
                break;
            }
        }

        return output;
    }

    private bool Parity(byte input)
    {
        int ones = 0;
        for (int i = 0; i < 8; i++)
        {
            ones += (input >> i) & 1;
        }

        return (ones & 1) == 0;
    }
}