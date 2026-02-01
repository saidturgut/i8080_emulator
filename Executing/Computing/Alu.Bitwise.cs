namespace intel8080.Executing.Computing;
using Components;

public partial class Alu
{
    private static AluOutput RLC(AluInput input) => new()
    {
        Result = (byte)((input.A << 1) | Bit7(input.A)),
        Flags = RotateCarry(Bit7(input.A))
    };
    private static AluOutput RAL(AluInput input) => new()
    {
        Result = (byte)((input.A << 1) | input.C),
        Flags = RotateCarry(Bit7(input.A))
    };
    private static AluOutput RRC(AluInput input) => new()
    {
        Result = (byte)((input.A >> 1) | (Bit0(input.A) << 7)),
        Flags = RotateCarry(Bit0(input.A))
    };
    private static AluOutput RAR(AluInput input) => new()
    {
        Result = (byte)((input.A >> 1) | (input.C << 7)),
        Flags = RotateCarry(Bit0(input.A))
    };
    
    private static AluOutput DAA(AluInput input)
    {
        byte fixer = 0;

        if ((input.A & 0x0F) > 9 || Psw.Auxiliary)
            fixer |= 0x06;

        if (input.A + fixer > 0x99 || Psw.Carry)
            fixer |= 0x60;

        var result = input.A + fixer;

        AluOutput output = new()
            { Result = (byte)result };

        if ((fixer & 0x06) != 0)
            output.Flags |= (byte)Flag.Auxiliary;
        if (result > 0xFF)
            output.Flags |= (byte)Flag.Carry;

        return output;
    }
    
    private static AluOutput CMA(AluInput input) => new()
    {
        Result = (byte)~input.A 
    };
    private static AluOutput STC(AluInput input) => new()
    {
        Result = input.A, Flags = 0xFF
    };
    private static AluOutput CMC(AluInput input) => new()
    {
        Result = input.A, Flags = (byte)(Psw.Carry ? 0x00 : 0xFF), 
    };

    private static AluOutput RST(AluInput input) => new()
    {
        Result = (byte)(((input.A >> 3) & 0x07) << 3),
    };
    
    private static byte RotateCarry(byte bit) => (byte)((byte)Flag.Carry & bit);
    private static byte Bit7(byte A) => (byte)((A >> 7) & 1);
    private static byte Bit0(byte A) => (byte)(A & 1);
}