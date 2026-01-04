using i8080_emulator.Signaling;

namespace i8080_emulator.Decoding;

public class DecoderCore : DecoderFamilies
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
                return Family00(opcode);
            case 0b01:
                return Family01(opcode);
            case 0b10:
                return Family10(opcode);
            case 0b11:
                throw new Exception("INVALID OPCODE");
        }

        return new Decoded();
    }
}

// 00_110_111