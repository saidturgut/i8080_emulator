namespace i8080_emulator.Signaling.Cycles;
using Decoding;

public partial class MicroUnitRom
{
    private static SignalSet EMPTY() => new();
    private static SignalSet FETCH() => new()
    {
        AddressDriver = Register.PC_L,
        IncAction = IncAction.INC,
        DataDriver = Register.RAM,
        DataLatcher = Register.IR,
        State = State.FETCH,
    };
    private static SignalSet DECODE() => new() { State = State.DECODE };
    private static SignalSet HALT() => new() { State = State.HALT };

    private static SignalSet ALU_EXECUTE() => new()
        { AluAction = decoded.AluAction!.Value, };
    
    private static SignalSet MOVE_IMM() => new()
    {
        AddressDriver = Register.PC_L,
        DataDriver = Register.RAM,
        DataLatcher = Register.TMP,
        IncAction = IncAction.INC,
    };
    private static SignalSet MOVE_LOAD() => new()
    {
        AddressDriver = decoded.AddressDriver,
        DataDriver = decoded.DataDriver,
        DataLatcher = decoded.DataLatcher,
    };
    private static SignalSet MOVE_STORE() => new()
    {
        AddressDriver = decoded.AddressDriver,
        DataDriver = decoded.DataLatcher,
        DataLatcher = decoded.DataDriver,
    };

    private static SignalSet MOVE_PAIR_IMM() => new()
    {
        AddressDriver = Register.PC_L,
        IncAction = IncAction.INC,
        DataDriver = Register.RAM,
        DataLatcher = decoded.Pair[pairIndex],
        Index = true,
    };
    private static SignalSet MOVE_PAIR_LOAD() => new()
    {
        AddressDriver = decoded.AddressDriver,
        IncAction = IncAction.INC,
        DataDriver = Register.RAM,
        DataLatcher = decoded.Pair[pairIndex],
        Index = true,
    };
    private static SignalSet MOVE_PAIR_STORE() => new()
    {
        AddressDriver = decoded.AddressDriver,
        IncAction = IncAction.INC,
        DataDriver = decoded.Pair[pairIndex],
        DataLatcher = Register.RAM,
        Index = true,
    };
    
    private static SignalSet MOVE_PAIR_TO_TMP() => new()
    {
        DataDriver = decoded.Pair[pairIndex],
        DataLatcher = Register.TMP,
        Index = true,
    };
    private static SignalSet MOVE_TMP_TO_PAIR() => new()
    {
        DataDriver = Register.TMP,
        DataLatcher = decoded.Pair[pairIndex],
        Index = true,
    };
}