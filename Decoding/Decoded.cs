namespace i8080_emulator.Decoding;
using Executing.Computing;
using Signaling.Cycles;
using Executing;
using Signaling;

public struct Decoded()
{    
    public DataLatcher DataLatcher = DataLatcher.NONE;// CMD X, B
    public DataDriver DataDriver = DataDriver.NONE;// CMD B, X
    public SideEffect SideEffect = SideEffect.NONE;

    public DataLatcher[] RegisterPair = [];
    
    public ALUOperation? AluOperation = null;
    
    public readonly List<MachineCycle> Table = [MachineCycle.FETCH];
}