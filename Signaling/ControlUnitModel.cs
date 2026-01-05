namespace i8080_emulator.Signaling;

public partial class ControlUnit
{
    private static readonly Dictionary<MachineCycle, Func<byte, SignalSet>> MachineCyclesMethod =
        new ()
        {
            { MachineCycle.FETCH, FETCH },
            { MachineCycle.RAM_READ, RAM_READ },
            { MachineCycle.RAM_READ_IMM, RAM_READ_IMM },
            { MachineCycle.RAM_WRITE, RAM_WRITE },
            { MachineCycle.BUS_LATCH, INTERNAL_LATCH },
            { MachineCycle.TMP_LATCH, TMP_LATCH },
            { MachineCycle.ALU_EXECUTE, ALU_EXECUTE },
            
            { MachineCycle.STC, STC },
            { MachineCycle.CMC, CMC },
        };
    private static readonly Dictionary<MachineCycle, byte> MachineCyclesLength
        = new()
        {
            { MachineCycle.FETCH, 4 },
            { MachineCycle.RAM_READ, 3 },
            { MachineCycle.RAM_READ_IMM, 3 },
            { MachineCycle.RAM_WRITE, 3 },
            { MachineCycle.BUS_LATCH, 2 },
            { MachineCycle.TMP_LATCH, 2 },
            { MachineCycle.ALU_EXECUTE, 2 },
            
            { MachineCycle.STC, 1 },
            { MachineCycle.CMC, 1 },
        };
}

public enum MachineCycle
{
    FETCH,
    RAM_READ, RAM_READ_IMM,
    RAM_WRITE, 
    BUS_LATCH,
    TMP_LATCH,
    ALU_EXECUTE,

    NONE, JMP, CALL, LDA, STA, LHLD, SHLD, //FIXED INSTRUCTIONS
    
    STC, CMC,
}