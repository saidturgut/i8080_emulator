namespace i8080_emulator.Signaling.Cycles;
using Decoding;

public partial class ControlUnitROM
{
    protected static Decoded decoded = new ();
    
    protected static readonly Dictionary<MachineCycle, Func<SignalSet>> MachineCyclesMethod = new()
    {
        { MachineCycle.EMPTY, EMPTY },
        { MachineCycle.HALT, HALT },
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
        { MachineCycle.EXECUTE_DECODED, EXECUTE_DECODED },

        { MachineCycle.ALU_EXECUTE, ALU_EXECUTE },

        { MachineCycle.MICRO_CYCLE, MICRO_CYCLE },
        
        { MachineCycle.CMA, CMA },
        { MachineCycle.INX_DCX, INX_DCX },

        { MachineCycle.RAM_READ_H, RAM_READ_H },
        { MachineCycle.RAM_WRITE_H, RAM_WRITE_H },
        
        { MachineCycle.COPY_RP_LOW, COPY_RP_LOW },
        { MachineCycle.COPY_RP_HIGH, COPY_RP_HIGH },
        
        { MachineCycle.RAM_READ_XTHL, RAM_READ_XTHL },
        { MachineCycle.XTHL_HIGH, XTHL_HIGH },
        { MachineCycle.XTHL_LOW, XTHL_LOW },
        
        { MachineCycle.PUSH_HIGH, PUSH_HIGH },
        { MachineCycle.PUSH_LOW, PUSH_LOW },
        
        { MachineCycle.POP_LOW, POP_LOW },
        { MachineCycle.POP_HIGH, POP_HIGH },
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
    EXECUTE_DECODED,
    
    ALU_EXECUTE,

    EMPTY, HALT,
    
    MICRO_CYCLE,
    
    CMA,
    INX_DCX, 
    
    RAM_READ_H, RAM_WRITE_H,
    
    COPY_RP_LOW, COPY_RP_HIGH,
    
    XTHL_HIGH, XTHL_LOW, RAM_READ_XTHL,
    
    PUSH_HIGH, PUSH_LOW,
    POP_LOW, POP_HIGH,
}