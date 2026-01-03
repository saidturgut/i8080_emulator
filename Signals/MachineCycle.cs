namespace i8080_emulator.Signals;
using Executing.Computing;

public enum MachineCycle
{
    FETCH,
    DECODE,
    RAM_READ, RAM_WRITE, 
    RAM_READ_IMM,
    
    NONE, JMP, CALL, LDA, STA, LHLD, SHLD, //FIXED INSTRUCTIONS
}

public struct SignalSet()
{
    public AddressDriver AddressDriver = AddressDriver.NONE;
    public DataDriver DataDriver  = DataDriver.NONE;
    public DataLatcher DataLatcher =  DataLatcher.NONE;
    public ALUOperation AluOperation = ALUOperation.NONE;
    public SideEffect SideEffect  = SideEffect.NONE;
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
    RAM,
    A, B, C, D, E,
    H, L,
}

public enum DataLatcher
{
    NONE,
    RAM, IR,
    A, B, C, D, E,
    H, L,
}

public enum SideEffect
{
    NONE,
    PC_INC,
}