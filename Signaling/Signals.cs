namespace i8080_emulator.Signaling;
using Executing.Computing;
using Executing;

public struct SignalSet()
{
    public Register AddressDriver = Register.NONE;
    public Register DataDriver = Register.NONE;
    public Register DataLatcher = Register.NONE;
    public ALUOperation? AluOperation = null;
    public SideEffect SideEffect = SideEffect.NONE;
}

public enum SideEffect
{
    NONE,
    PC_INC,
    
    SP_INC, SP_DCR,
    
    BC_INC, BC_DCR,
    DE_INC, DE_DCR,
    HL_INC, HL_DCR,
    
    WZ_INC,
    
    STC, CMC, CMA,
}