namespace i8080_emulator.Decoding.Multiplexer;
using Signaling.Multiplexer;
using Signaling;
using Executing;

public partial class DecoderMux : DecoderRom
{
    protected static readonly Dictionary<byte, MicroCycle> FixedOpcodes = new()
    {
        { 0x76, MicroCycle.HALT }, // HALT

        { 0xF3, MicroCycle.IDLE }, // DI
        { 0xFB, MicroCycle.IDLE }, // EI
        
        { 0x00, MicroCycle.IDLE }, // NOP
        
        { 0x08, MicroCycle.IDLE }, // NOP
        { 0x10, MicroCycle.IDLE }, // NOP
        { 0x18, MicroCycle.IDLE }, // NOP
        { 0x20, MicroCycle.IDLE }, // NOP
        { 0x28, MicroCycle.IDLE }, // NOP
        { 0x30, MicroCycle.IDLE }, // NOP
        { 0x38, MicroCycle.IDLE }, // NOP
        { 0x40, MicroCycle.IDLE }, // NOP
        { 0x49, MicroCycle.IDLE }, // NOP
        { 0x52, MicroCycle.IDLE }, // NOP
        { 0x5B, MicroCycle.IDLE }, // NOP
        { 0x64, MicroCycle.IDLE }, // NOP
        { 0x6D, MicroCycle.IDLE }, // NOP
        { 0x7F, MicroCycle.IDLE }, // NOP
    };
    
    protected static Decoded FIXED() => new()
        { MicroCycles = [FixedOpcodes[opcode]], };
    protected static Decoded IDLE() => new()
        { MicroCycles = [MicroCycle.IDLE], };

    public static Decoded FETCH() => new()
    {
        AddressDriver = Register.PC_L,
        MicroCycles = [MicroCycle.FETCH, MicroCycle.DECODE],
    };

    protected static Decoded IO(IoAction ioAction) => new()
    {
        AddressDriver = Register.TMP,
        DataDriver = ioAction is IoAction.OUTPUT ? Register.A : Register.NONE,
        DataLatcher = ioAction is IoAction.INPUT ? Register.A : Register.NONE,
        IoAction = ioAction,
        
        MicroCycles = [MicroCycle.IMM_TMP, MicroCycle.IO]
    };
}