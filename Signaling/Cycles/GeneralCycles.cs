namespace i8080_emulator.Signaling.Cycles;
using Executing.Computing;
using Executing;

public partial class ControlUnitROM
{
    private static SignalSet EMPTY() => new();
    private static SignalSet HALT() => new() { SideEffect = SideEffect.HALT };
    
    private static SignalSet FETCH() => new()
    {
        AddressDriver = Register.PC_L,
        DataDriver = Register.RAM,
        DataLatcher = Register.IR,
        SideEffect = SideEffect.PC_INC,
    };
    
    private static SignalSet INTERNAL_LATCH() => new ()
    {
        DataDriver = decoded.DataDriver,
        DataLatcher = decoded.DataLatcher,
    };
    
    private static SignalSet TMP_LATCH() => new ()
    {
        DataDriver = decoded.DataDriver,
        DataLatcher = Register.TMP,
    };

    private static SignalSet EXECUTE_DECODED() => new()
    {
        AddressDriver = decoded.AddressDriver,
        DataDriver = decoded.DataDriver, 
        DataLatcher = decoded.DataLatcher,
        SideEffect = decoded.SideEffect
    };
    
    private static SignalSet ALU_EXECUTE() => new ()
    {
        AluOperation = (ALUOperation)decoded.AluOperation!,
        DataLatcher = decoded.DataLatcher,
    };

    private static SignalSet MICRO_CYCLE() => new()
        { SideEffect = decoded.SideEffect };
}