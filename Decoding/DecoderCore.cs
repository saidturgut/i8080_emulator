using i8080_emulator.Signaling;

namespace i8080_emulator.Decoding;

public class DecoderCore : DecoderFamilies
{
    public Decoded Decode(byte opcode)
    {
        // CHECK FIXED OPCODES
        switch (opcode) 
        {
            case 0x00:// NOP
                return GroupSYS(MachineCycle.NONE);
            case 0x76:// HLT
                return GroupSYS(MachineCycle.NONE);
            case 0xC3:// JMP
                return GroupSYS(MachineCycle.JMP);
            case 0xCD:// CALL
                return GroupSYS(MachineCycle.CALL);
            case 0x3A:// LDA
                return GroupSYS(MachineCycle.LDA);
            case 0x32:// STA
                return GroupSYS(MachineCycle.STA);
            case 0x2A:// LHLD
                return GroupSYS(MachineCycle.LHLD);
            case 0x22:// SHLD
                return GroupSYS(MachineCycle.SHLD);
        }
        
        // CHECK INSTRUCTION FAMILY
        switch ((opcode & 0b1100_0000) >> 6)
        {
            case 0b00:
                return GroupIMM(opcode);
            case 0b01:
                return GroupREG(opcode);
            case 0b10:
                return GroupALU(opcode, false);
            case 0b11:
                return GroupALU(opcode, false);
        }

        return new Decoded();
    }
}