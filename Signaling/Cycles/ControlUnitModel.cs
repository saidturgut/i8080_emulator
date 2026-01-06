namespace i8080_emulator.Signaling.Cycles;
using Decoding;

public partial class ControlUnitModel
{
    protected static Decoded decoded = new ();
    
    protected static readonly Dictionary<MachineCycle, Func<SignalSet>> MachineCyclesMethod = new()
    {
        { MachineCycle.FETCH, FETCH },
        { MachineCycle.RAM_READ, RAM_READ },
        { MachineCycle.RAM_READ_IMM, RAM_READ_IMM },
        { MachineCycle.RAM_WRITE, RAM_WRITE },

        { MachineCycle.INTERNAL_LATCH, INTERNAL_LATCH },
        { MachineCycle.TMP_LATCH, TMP_LATCH },
        { MachineCycle.LXI_LOW, LXI_LOW },
        { MachineCycle.LXI_HIGH, LXI_HIGH },

        { MachineCycle.ALU_EXECUTE, ALU_EXECUTE },

        { MachineCycle.STC, STC },
        { MachineCycle.CMC, CMC },
        { MachineCycle.INX_DCX, INX_DCX },
    };
}

public enum MachineCycle
{
    FETCH,
    RAM_READ, RAM_READ_IMM,
    RAM_WRITE, 
    
    INTERNAL_LATCH,
    TMP_LATCH,
    LXI_LOW, LXI_HIGH, 
    
    ALU_EXECUTE,

    NONE, JMP, CALL, LDA, STA, LHLD, SHLD, //FIXED INSTRUCTIONS
    
    STC, CMC,
    INX_DCX,
}