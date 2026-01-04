namespace i8080_emulator.Signaling;
public partial class ControlUnit
{
    private static SignalSet STC(byte tState) => new SignalSet { SideEffect = SideEffect.STC };
    private static SignalSet CMC(byte tState) => new SignalSet { SideEffect = SideEffect.CMC };
}