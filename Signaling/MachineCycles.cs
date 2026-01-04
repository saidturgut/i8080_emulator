using i8080_emulator.Executing.Computing;

namespace i8080_emulator.Signaling;
using Decoding;

public partial class ControlUnit
{
    private static Decoded decoded = new Decoded();
    
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
        3 => new SignalSet { DataLatcher = DataLatcher.TMP },
        _ => throw ILLEGAL_T_STATE()
    };

    private static SignalSet RAM_READ_IMM(byte tState) => tState switch
    {
        1 => new SignalSet { AddressDriver = AddressDriver.PC },
        2 => new SignalSet { DataDriver = DataDriver.RAM },
        3 => new SignalSet { DataLatcher = DataLatcher.TMP, SideEffect = SideEffect.PC_INC },
        _ => throw ILLEGAL_T_STATE()
    };
    
    private static SignalSet RAM_WRITE(byte tState) => tState switch
    {
        1 => new SignalSet { AddressDriver = AddressDriver.HL },
        2 => new SignalSet { DataDriver = DataDriver.TMP },
        3 => new SignalSet { DataLatcher = DataLatcher.RAM },
        _ => throw ILLEGAL_T_STATE()
    };
    
    private static SignalSet INTERNAL_LATCH(byte tState) => tState switch
    {
        1 => new SignalSet { DataDriver = decoded.DataDriver },
        2 => new SignalSet { DataLatcher = decoded.DataLatcher },
        _ => throw ILLEGAL_T_STATE()
    };
    
    private static SignalSet TMP_LATCH(byte tState) => tState switch
    {
        1 => new SignalSet { DataDriver = decoded.DataDriver },
        2 => new SignalSet { DataLatcher = DataLatcher.TMP },
        _ => throw ILLEGAL_T_STATE()
    };
    
    private static SignalSet ALU_EXECUTE(byte tState) => tState switch
    {
        1 => new SignalSet { AluOperation = decoded.AluOperation },
        _ => throw ILLEGAL_T_STATE()
    };
    
    private static Exception ILLEGAL_T_STATE() 
        => new ("ILLEGAL T STATE");
}