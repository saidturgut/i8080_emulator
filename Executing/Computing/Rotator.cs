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
}