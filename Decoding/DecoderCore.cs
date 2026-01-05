namespace i8080_emulator.Decoding;

public class DecoderCore : DecoderMultiplexer
{
    public Decoded Decode(byte opcode)
    {
        // CHECK FIXED OPCODES
        if (FixedOpcodes.TryGetValue(opcode, out var value))
            return Family11(value);
        
        // CHECK INSTRUCTION FAMILY
        switch ((opcode & 0b1100_0000) >> 6)
        {
            case 0b00:
                if (BB_BBB_XXX(opcode) != 4 && BB_BBB_XXX(opcode) != 5)
                {
                    return Family00(opcode);
                }
                return Family10(opcode, false);
            case 0b01:
                return Family01(opcode);
            case 0b10:
                return Family10(opcode, true);
            case 0b11:
                throw new Exception("INVALID OPCODE");
        }

        return new Decoded();
    }
}

// 00_110_111