namespace i8080_emulator.Decoding;
using Multiplexer;

public class Decoder : DecoderMultiplexer
{
    public Decoded Decode(byte opcode)
    {
        // CHECK FIXED OPCODES
        if (FixedOpcodes.TryGetValue(opcode, out var value))
            return FamilyFXD(value);
        
        // CHECK INSTRUCTION FAMILY
        switch ((opcode & 0b1100_0000) >> 6)
        {
            case 0b00:
                if (BB_BBB_XXX(opcode) == 0b100 || BB_BBB_XXX(opcode) == 0b101)
                {
                    return FamilyALU(opcode, false);
                }

                if (BB_BBB_XXX(opcode) == 0b011)
                {
                    return INX_DCX(opcode);
                }

                if (BB_BBB_XXX(opcode) == 0b001)
                {
                    return LXI(opcode);
                }
                
                return Family00(opcode);
            case 0b01:
                return FamilyMOV(opcode);
            case 0b10:
                return FamilyALU(opcode, true);
            case 0b11:
                throw new Exception("INVALID OPCODE");
        }

        return new Decoded();
    }
}