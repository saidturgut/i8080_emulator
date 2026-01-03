using i8080_emulator.Signaling;

namespace i8080_emulator.Decoding;

public class DecoderCore : DecoderFamilies
{
    public Decoded Decode(byte opcode)
    {
        // CHECK FIXED OPCODES
        if (FixedOpcodes.TryGetValue(opcode, out var value))
            return FamilySYS(value);
        
        // CHECK INSTRUCTION FAMILY
        switch ((opcode & 0b1100_0000) >> 6)
        {
            case 0b00:
                return FamilyIMM(opcode);
            case 0b01:
                return FamilyREG(opcode);
            case 0b10:
                return FamilyALU(opcode, false);
            case 0b11:
                return FamilyALU(opcode, true);
        }

        return new Decoded();
    }
}