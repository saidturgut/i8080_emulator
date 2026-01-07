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
                switch (BB_BBB_XXX(opcode))
                {
                    case 0b111: return ROTATE(opcode);
                    case 0b101:
                    case 0b100: return FamilyALU(opcode, false);
                    case 0b011: return INX_DCX(opcode);
                    case 0b010:
                    {
                        switch (BB_XXB_BBB(opcode))
                        {
                            case 0b11: return LDA_STA(opcode, false);
                            case 0b10: return LHLD_SHLD(opcode);
                            default: return LDA_STA(opcode, true);
                        }
                    }
                    case 0b001:
                    {
                        if(BB_BBX_BBB(opcode) == 1)
                            return DAD(opcode);
                        else
                            return LXI(opcode);
                    }
                }
                
                return FamilyMSC(opcode);
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