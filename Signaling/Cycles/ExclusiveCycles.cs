namespace i8080_emulator.Signaling.Cycles;

public partial class ControlUnitModel
{
    private static SignalSet STC() => new () { SideEffect = SideEffect.STC };
    private static SignalSet CMC() => new () { SideEffect = SideEffect.CMC };
    private static SignalSet INX_DCX() => new () { SideEffect = decoded.SideEffect };
    
    private static SignalSet LXI_LOW() => new ()
    {
        AddressDriver = AddressDriver.PC,
        DataDriver = DataDriver.RAM,
        DataLatcher = decoded.RegisterPair[0],
        SideEffect = SideEffect.PC_INC,
    };
    
    private static SignalSet LXI_HIGH() => new ()
    {
        AddressDriver = AddressDriver.PC,
        DataDriver = DataDriver.RAM,
        DataLatcher = decoded.RegisterPair[1],
        SideEffect = SideEffect.PC_INC,
    };
}