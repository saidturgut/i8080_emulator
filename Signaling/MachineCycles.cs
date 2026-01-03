namespace i8080_emulator.Signaling;
using Decoding;

public partial class ControlUnit
{
    private static Decoded decoded = new Decoded();

    private readonly Dictionary<MachineCycle, Func<byte, SignalSet>> MachineCycleTable =
        new ()
        {
            { MachineCycle.FETCH, FETCH },
            { MachineCycle.RAM_READ, RAM_READ },
            { MachineCycle.RAM_READ_IMM, RAM_READ_IMM },
            { MachineCycle.RAM_WRITE, RAM_WRITE },
            { MachineCycle.EXECUTE_MOV, EXECUTE_MOV },
            { MachineCycle.EXECUTE_ALU, EXECUTE_ALU },
        };
    
    private static SignalSet FETCH(byte tState) => tState switch
    {
        1 => new SignalSet { AddressDriver = AddressDriver.PC },
        2 => new SignalSet { DataDriver = DataDriver.RAM },
        3 => new SignalSet { DataLatcher = DataLatcher.IR, SideEffect = SideEffect.PC_INC },
        4 => new SignalSet { SideEffect = SideEffect.DECODE },
        _ => throw ILLEGAL_T_STATE()
    };

    private static SignalSet RAM_READ(byte tState) => tState switch
    {
        1 => new SignalSet { AddressDriver = AddressDriver.HL },
        2 => new SignalSet { DataDriver = DataDriver.RAM },
        3 => new SignalSet { DataLatcher = decoded.DataLatcher },
        _ => throw ILLEGAL_T_STATE()
    };

    private static SignalSet RAM_READ_IMM(byte tState) => tState switch
    {
        1 => new SignalSet { AddressDriver = AddressDriver.HL },
        2 => new SignalSet { DataDriver = DataDriver.RAM },
        3 => new SignalSet { DataLatcher = decoded.DataLatcher, SideEffect = SideEffect.PC_INC },
        _ => throw ILLEGAL_T_STATE()
    };

    private static SignalSet RAM_WRITE(byte tState) => tState switch
    {
        1 => new SignalSet { AddressDriver = AddressDriver.HL },
        2 => new SignalSet { DataDriver = decoded.DataDriver },
        3 => new SignalSet { DataLatcher = DataLatcher.RAM },
        _ => throw ILLEGAL_T_STATE()
    };
    
    private static SignalSet EXECUTE_MOV(byte tState) => tState switch
    {
        1 => new SignalSet { DataDriver = decoded.DataDriver },
        2 => new SignalSet { DataLatcher = decoded.DataLatcher },
        _ => throw ILLEGAL_T_STATE()
    };
    
    private static SignalSet EXECUTE_ALU(byte tState) => tState switch
    {
        1 => new SignalSet { DataDriver = decoded.DataDriver },
        2 => new SignalSet { DataLatcher = decoded.DataLatcher },
        3 => new SignalSet { SideEffect = SideEffect.FLAG_LATCH },
        _ => throw ILLEGAL_T_STATE()
    };
    
    private static Exception ILLEGAL_T_STATE() 
        => new ("ILLEGAL T STATE");
}