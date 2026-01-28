namespace i8080_emulator.Decoding.Multiplexer;
using Executing.Computing;
using Signaling.Cycles;
using Signaling;

// 10b AND 11b INSTRUCTIONS
public partial class DecoderMux
{
    protected static Decoded ALU(bool native)
    {
        Decoded decoded = new()
        {
            AddressDriver = Register.HL_L,
            DataDriver = EncodedRegisters[zz_zzz_xxx()],
            DataLatcher = Register.TMP,

            AluAction = new AluAction(
                EncodedOperations[zz_xxx_zzz()],
                FlagMasks[(byte)FlagMask.SZAPC],
                zz_xxx_zzz() == 0x1 || zz_xxx_zzz() == 3), // ADC, SBB

            MicroCycles = [native ? MicroCycle.MOVE_LOAD : MicroCycle.MOVE_IMM, MicroCycle.ALU_EXECUTE],
        };
        
        Console.WriteLine(EncodedOperations[zz_xxx_zzz()]);
        //Environment.Exit(5);
        
        return decoded;
    }

    private static readonly Operation[] EncodedOperations =
    [
        Operation.ADD, Operation.ADD,
        Operation.SUB, Operation.SUB,
        Operation.AND, Operation.XOR,
        Operation.OR, Operation.SUB,
    ];
    
    public enum FlagMask
    {
        NONE, SZAPC,
    }
    
    private static readonly PswFlag[] FlagMasks =
    [
        PswFlag.None,
        PswFlag.Sign | PswFlag.Zero | PswFlag.Auxiliary | PswFlag.Parity | PswFlag.Carry,
    ];
}