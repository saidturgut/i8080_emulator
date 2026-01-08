namespace i8080_emulator.Decoding;
using Executing.Computing;
using Signaling.Cycles;
using Executing;
using Signaling;

public struct Decoded()
{
    public Register AddressDriver = Register.NONE;
    public Register DataDriver = Register.NONE;
    public Register DataLatcher = Register.NONE;
    public SideEffect SideEffect = SideEffect.NONE;

    public Register[] RegisterPairs = [];
    
    public ALUOperation? AluOperation = null;
    
    public readonly List<MachineCycle> Cycles = [MachineCycle.FETCH];
}