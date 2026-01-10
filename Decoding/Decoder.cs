namespace i8080_emulator.Decoding;
using Multiplexer;

public class Decoder : DecoderMultiplexer
{
    public Decoded Decode(byte[] values)
    {
        byte opcode = values[0];
        byte flags = values[1];
        
        // CHECK FIXED OPCODES
        if (FixedMicroCycles.TryGetValue(opcode, out var value))
            return FamilyFXD(value);
        
        // CHECK INSTRUCTION FAMILY
        switch ((opcode & 0b1100_0000) >> 6)
        {
            case 0b00:
                switch (BB_BBB_XXX(opcode))
                {
                    case 0b111: return ROT(opcode);
                    case 0b101:
                    case 0b100: return FamilyALU(opcode, false, false);
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
                        if(BB_BBX_BBB(opcode) == 1) return DAD(opcode);
                        else return LXI(opcode);
                    }
                }
                return FamilyMSC(opcode);
            case 0b01: return FamilyMOV(opcode);
            case 0b10: return FamilyALU(opcode, true, false);
            case 0b11:
            {
                switch (opcode)
                {
                    case 0xCD: return CALL();// CALL
                    case 0xC9: return RET();// RET
                    case 0xC3: return JMP();// JMP
                    case 0xE3: return XTHL(); // XTHL (M[SP] <-> HL)
                    case 0xEB: return COPY_HL(1);// XCHG (HL <-> DE)
                    case 0xF9: return COPY_HL(3);// SPHL (HL -> SP)
                }

                switch (BB_BBB_XXX(opcode))
                {
                    case 0b111: return RST(); // RST (JMP N*8)
                    case 0b100: //CCC (COND)
                    case 0b010: //JCC (COND)
                    case 0b000: //RCC (COND)
                        return CONDITIONALS(opcode, flags);
                }
                
                switch (BBBB_XXXX(opcode))
                {
                    case 0b0101: return PUSH(opcode); // PUSH (RP -> M[SP])
                    case 0b0001: return POP(opcode); // POP (M[SP] -> RP)
                    case 0b0110:
                    case 0b1110: return FamilyALU(opcode, true, true);
                }
                break;
            }
        }

        return new Decoded();
    }
}