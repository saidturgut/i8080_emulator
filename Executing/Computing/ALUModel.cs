namespace i8080_emulator.Executing.Computing;

public struct ALUInput
{
    public ALUOperation Operation;
    public byte A;
    public byte B;
    public bool CR;
}

public struct ALUOutput()
{
    public byte Result = 0;
    public byte Flags = 0x2;
}

[Flags]
public enum ALUFlags
{
    Sign = 1 << 7,
    Zero = 1 << 6,
    //000000 = 1 <<  5,
    AuxCarry = 1 <<  4,
    //000000 = 1 <<  3,
    Parity = 1 <<  2,
    //111111 = 1 <<  1,
    Carry = 1 <<  0,
}

public enum ALUOperation
{
    NONE,
    ADD, SUB, SBB, AND, XOR, OR,  CMP, INR, DCR
}

public enum ALUOpcode
{
    ADD, ADC, SUB, SBB, ANA, XRA, ORA, CMP,
}
