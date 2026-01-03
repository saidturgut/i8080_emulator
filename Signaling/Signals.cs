namespace i8080_emulator.Signaling;
using Executing.Computing;

public struct SignalSet()
{
    public AddressDriver AddressDriver = AddressDriver.NONE;
    public DataDriver DataDriver = DataDriver.NONE;
    public DataLatcher DataLatcher = DataLatcher.NONE;
    public ALUOperation AluOperation = new ALUOperation();
    public SideEffect SideEffect = SideEffect.NONE;
}

public enum AddressDriver
{
    NONE,
    PC, SP,
    HL,
}

public enum DataDriver
{
    NONE,
    B, C, D, E,
    H, L,
    RAM,
    A,
}

public enum DataLatcher
{
    NONE,
    B, C, D, E,
    H, L,
    RAM,
    A,
    IR,
}

public enum SideEffect
{
    NONE,
    DECODE,
    PC_INC,
    FLAG_LATCH,
}