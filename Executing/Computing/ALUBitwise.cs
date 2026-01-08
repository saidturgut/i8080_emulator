using i8080_emulator.Decoding;

namespace i8080_emulator.Executing.Computing;

public partial class ALU
{
    public ALUOutput Rotate(ALUOpcode opcode, byte A, byte CY)
    {
        byte bit7 = (byte)((A >> 7) & 1);
        byte bit0 = (byte)(A & 1);
        
        switch (opcode)
        {
            case ALUOpcode.RLC:
                return new ALUOutput((byte)((A << 1) | bit7), bit7);
            case ALUOpcode.RAL:
                return new ALUOutput((byte)((A << 1) | CY), bit7);
            case ALUOpcode.RRC:
                return new ALUOutput((byte)((A >> 1) | (bit0 << 7)), bit0);
            case ALUOpcode.RAR:
                return new ALUOutput((byte)((A >> 1) | (CY << 7)), bit0);
        }
        return new ALUOutput();
    }

    public ALUOutput BCDFixer(byte A, bool CY, bool AC)
    { // DAA
        byte fixer = 0;
        ALUFlag mask = ALUFlag.Sign | ALUFlag.Zero | ALUFlag.Parity;

        if ((A & 0x0F) > 9 || AC) // LOW
        {
            fixer |= 0x06;
            mask |= ALUFlag.AuxCarry;
        }

        if (A > 0x99 || CY) // HIGH
        {
            fixer |= 0x60;
            mask |= ALUFlag.Carry;
        }
        
        ALUOutput output = Compute(new ALUInput
            (Operation.ADD, A,  fixer, false, false));
        
        output.FlagMask = mask;
        return output;
    }
}