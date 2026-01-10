namespace i8080_emulator.Signaling.Cycles;
using Executing.Computing;
using Executing;

public partial class ControlUnitROM
{
    private static SignalSet CMA() => new()
    {
        DataDriver = Register.A,
        SideEffect = SideEffect.CMA,
        DataLatcher = Register.A,
    };
    private static SignalSet INX_DCX() => new()
        { SideEffect = decoded.SideEffect };

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

    // *** XCHG / SPHL *** //
    private static SignalSet COPY_RP_LOW() => new ()
    {
        DataDriver = decoded.DrivePairs[0],
        DataLatcher = decoded.LatchPairs[0],
        SideEffect = decoded.SideEffect,
    };
    private static SignalSet COPY_RP_HIGH() => new ()
    {
        DataDriver = decoded.DrivePairs[1],
        DataLatcher = decoded.LatchPairs[1],
        SideEffect = decoded.SideEffect,
    };
    
    // *** PUSH, CALL *** //
    private static SignalSet PUSH_HIGH() => new()
    {
        AddressDriver = Register.SP_L,
        DataDriver = decoded.DrivePairs[1],
        DataLatcher = Register.RAM,
        SideEffect = SideEffect.SP_NXT,
    };
    private static SignalSet PUSH_LOW() => new()
    {
        AddressDriver = Register.SP_L,
        DataDriver = decoded.DrivePairs[0],
        DataLatcher = Register.RAM,
        SideEffect = SideEffect.SP_NXT,
    };
    
    // *** POP, RET *** //
    private static SignalSet POP_LOW() => new()
    {
        AddressDriver = Register.SP_L,
        DataDriver = Register.RAM,
        DataLatcher = decoded.LatchPairs[0],
        SideEffect = SideEffect.SP_INC,
    };
    private static SignalSet POP_HIGH() => new()
    {
        AddressDriver = Register.SP_L,
        DataDriver = Register.RAM,
        DataLatcher = decoded.LatchPairs[1],
        SideEffect = SideEffect.SP_INC,
    };
    
    // *** XTHL *** //
    private static SignalSet RAM_READ_XTHL() => new ()
    {
        AddressDriver = Register.SP_L,
        DataDriver = Register.RAM,
        DataLatcher = Register.TMP,
        SideEffect = SideEffect.XTHL_SP,
    };
    private static SignalSet XTHL_HIGH() => new ()
    {
        AddressDriver = Register.SP_L,
        DataDriver = Register.HL_H,
        DataLatcher = Register.RAM,
        SideEffect = SideEffect.XTHL_SP,
    };
    private static SignalSet XTHL_LOW() => new ()
    {
        AddressDriver = Register.SP_L,
        DataDriver = Register.HL_L,
        DataLatcher = Register.RAM,
        SideEffect = SideEffect.XTHL,
    };
}