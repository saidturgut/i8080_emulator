namespace i8080_emulator.Signaling.Cycles;
using Executing.Computing;
using Executing;

public partial class ControlUnitROM
{
    private static SignalSet PCHL() => new() { SideEffect = SideEffect.PCHL };
    private static SignalSet STC() => new () { SideEffect = SideEffect.STC };
    private static SignalSet CMC() => new () { SideEffect = SideEffect.CMC };
    private static SignalSet CMA() => new()
    {
        DataDriver = Register.A,
        SideEffect = SideEffect.CMA,
        DataLatcher = Register.A,
    };
    private static SignalSet INX_DCX() => new () { SideEffect = decoded.SideEffect };

    // *** LHLD / SHLD READ / WRITE AND EXECUTE *** //
    private static SignalSet RAM_READ_H() => new ()
    {
        AddressDriver = decoded.AddressDriver,
        DataDriver = Register.RAM,
        DataLatcher = Register.HL_H,
    };
    private static SignalSet RAM_WRITE_H() => new ()
    {
        AddressDriver = decoded.AddressDriver,
        DataDriver = Register.HL_H,
        DataLatcher = Register.RAM,
    };

    // *** SET PCHL / SPHL WITH HL *** //
    private static SignalSet COPY_HL_LOW() => new ()
    {
        DataDriver = Register.HL_L,
        DataLatcher = decoded.RegisterPairs[0],
        SideEffect = decoded.SideEffect,
    };
    private static SignalSet COPY_HL_HIGH() => new ()
    {
        DataDriver = Register.HL_H,
        DataLatcher = decoded.RegisterPairs[1],
        SideEffect = decoded.SideEffect,
    };
}