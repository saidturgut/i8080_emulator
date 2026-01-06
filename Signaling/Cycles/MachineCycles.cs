namespace i8080_emulator.Signaling.Cycles;
using Executing.Computing;

public partial class ControlUnitModel
{
    private static SignalSet FETCH() => new()
    {
        AddressDriver = AddressDriver.PC,
        DataDriver = DataDriver.RAM,
        DataLatcher = DataLatcher.IR,
        SideEffect = SideEffect.PC_INC,
    };

    private static SignalSet RAM_READ() => new ()
    {
        AddressDriver = AddressDriver.HL,
        DataDriver = DataDriver.RAM,
        DataLatcher = DataLatcher.TMP,
    };

    private static SignalSet RAM_READ_IMM() => new ()
    {
        AddressDriver = AddressDriver.PC,
        DataDriver = DataDriver.RAM,
        DataLatcher = DataLatcher.TMP,
        SideEffect = SideEffect.PC_INC,
    };

    private static SignalSet RAM_WRITE() => new ()
    {
        AddressDriver = AddressDriver.HL,
        DataDriver = DataDriver.TMP,
        DataLatcher = DataLatcher.RAM,
    };

    private static SignalSet INTERNAL_LATCH() => new ()
    {
        DataDriver = decoded.DataDriver,
        DataLatcher = decoded.DataLatcher,
    };
    
    private static SignalSet TMP_LATCH() => new ()
    {
        DataDriver = decoded.DataDriver,
        DataLatcher = DataLatcher.TMP,
    };
    
    private static SignalSet ALU_EXECUTE() => new ()
    {
        AluOperation = (ALUOperation)decoded.AluOperation!,
        DataLatcher = decoded.DataLatcher,
    };
}