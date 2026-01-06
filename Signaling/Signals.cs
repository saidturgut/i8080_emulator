namespace i8080_emulator.Signaling;
using Executing.Computing;
using Executing;

public struct SignalSet()
{
    public AddressDriver AddressDriver = AddressDriver.NONE;
    public DataDriver DataDriver = DataDriver.NONE;
    public DataLatcher DataLatcher = DataLatcher.NONE;
    public ALUOperation? AluOperation = null;
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
    TMP,
}

public enum DataLatcher
{
    NONE,
    B, C, D, E,
    H, L,
    RAM,
    A,
    TMP,
    IR,
    SP_H, SP_L
}

public enum SideEffect
{
    NONE,
    PC_INC,
    
    SP_INC, SP_DCR,
    
    BC_INC, BC_DCR,
    DE_INC, DE_DCR,
    HL_INC, HL_DCR,
    
    STC, CMC,
}