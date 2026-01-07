namespace i8080_emulator.Signaling.Cycles;
using Decoding;

public partial class ControlUnitROM
{
    protected static Decoded decoded = new ();
    
    protected static readonly Dictionary<MachineCycle, Func<SignalSet>> MachineCyclesMethod = new()
    {
        { MachineCycle.FETCH, FETCH },
        
        { MachineCycle.RAM_READ_TMP, RAM_READ_TMP },
        { MachineCycle.RAM_WRITE_TMP, RAM_WRITE_TMP },
        { MachineCycle.RAM_READ_EXE, RAM_READ_EXE },
        { MachineCycle.RAM_WRITE_EXE, RAM_WRITE_EXE },

        { MachineCycle.RAM_READ_IMM, RAM_READ_IMM },
        { MachineCycle.RAM_READ_IMM_LOW, RAM_READ_IMM_LOW },
        { MachineCycle.RAM_READ_IMM_HIGH, RAM_READ_IMM_HIGH },

        { MachineCycle.INTERNAL_LATCH, INTERNAL_LATCH },
        { MachineCycle.TMP_LATCH, TMP_LATCH },

        { MachineCycle.ALU_EXECUTE, ALU_EXECUTE },

        { MachineCycle.STC, STC },
        { MachineCycle.CMC, CMC },
        { MachineCycle.CMA, CMA },
        { MachineCycle.INX_DCX, INX_DCX },

        { MachineCycle.RAM_READ_H, RAM_READ_H },
        { MachineCycle.RAM_WRITE_H, RAM_WRITE_H },
    };
}

public enum MachineCycle
{
    FETCH,
    RAM_READ_TMP, RAM_WRITE_TMP, 
    RAM_READ_EXE, RAM_WRITE_EXE, 

    RAM_READ_IMM,
    RAM_READ_IMM_LOW, RAM_READ_IMM_HIGH, 

    INTERNAL_LATCH,
    TMP_LATCH,
    
    ALU_EXECUTE,

    EMPTY, //FIXED INSTRUCTIONS
    
    STC, CMC, CMA,
    INX_DCX, 
    
    RAM_READ_H, RAM_WRITE_H,
}