namespace i8080_emulator.Decoding;
using Executing.Computing;
using Signals;

public struct Decoded()
{    
    public DataLatcher DataLatcher = DataLatcher.NONE;// CMD X, B
    public DataDriver DataDriver = DataDriver.NONE;// CMD B, X
    
    public ALUOperation AluOperation = ALUOperation.NONE;

    public bool InternalLatch = false;
    
    public readonly List<MachineCycle> Table = [MachineCycle.FETCH, MachineCycle.DECODE];
}