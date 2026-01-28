namespace i8080_emulator.Executing.Computing;

public struct AluInput
{
    public byte A;
    public byte B;
    public byte C;
}

public struct AluOutput
{
    public byte Result;
    public byte Flags;
}

[Flags]
public enum PswFlag
{
    None = 0,
    Sign = 1 << 7,
    Zero = 1 << 6,
    //000000 = 1 <<  5,
    Auxiliary = 1 <<  4,
    //000000 = 1 <<  3,
    Parity = 1 <<  2,
    //111111 = 1 <<  1,
    Carry = 1 <<  0,
}
