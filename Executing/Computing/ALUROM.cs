namespace i8080_emulator.Executing.Computing;
using Signaling;

public static class ALUROM
{
    public static readonly Dictionary<FlagMask, ALUFlag> FlagMasks = new()
    {
        { FlagMask.ALL, ALUFlag.Sign | ALUFlag.Zero | ALUFlag.AuxCarry | ALUFlag.Parity | ALUFlag.Carry },
        { FlagMask.SZAP, ALUFlag.Sign | ALUFlag.Zero | ALUFlag.AuxCarry | ALUFlag.Parity },
        { FlagMask.C, ALUFlag.Carry },
    };
}

public enum FlagMask
{
    ALL, SZAP, C,
}

public readonly struct ALUInput(Operation operation, byte a, byte b, bool flagCy, bool carryUser)
{
    public readonly Operation Operation = operation;
    public readonly byte A = a;
    public readonly byte B = b;
    public readonly bool FlagCY = flagCy;
    public readonly bool CarryUser = carryUser;
}

public struct ALUOutput()
{
    public byte Result = 0;
    public byte Flags = 0x2;

    public ALUOutput(byte result, byte flags) : this()
    {
        Result = result;
        Flags = flags;
    }
}

[Flags]
public enum ALUFlag
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

public struct ALUOperation()
{
    public Operation Operation = Operation.NONE;
    public ALUOpcode Opcode = ALUOpcode.NONE;
    public Register A = 0;
    public Register B = 0;
    public bool UseCarry = false;
    public FlagMask FlagMask = FlagMask.ALL;
}

public enum Operation
{
    NONE,
    ADD, SUB, AND, XOR, OR,
    ROT,
}

public enum ALUOpcode
{
    NONE,
    ADD, ADC, SUB, SBB, ANA, XRA, ORA, CMP,
    INR, DCR, INX, DCX,
    DAD,
    RLC, RAL, RRC, RAR,
}