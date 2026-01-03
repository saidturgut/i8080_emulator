namespace i8080_emulator.Signaling;

public partial class ControlUnit
{
    private static readonly Dictionary<MachineCycle, byte> MachineCyclesData
        = new()
        {
            { MachineCycle.FETCH, 4 },
            { MachineCycle.RAM_READ, 3 },
            { MachineCycle.RAM_READ_IMM, 3 },
            { MachineCycle.RAM_WRITE, 3 },
            { MachineCycle.EXECUTE_MOV, 2 },
            { MachineCycle.EXECUTE_ALU, 3 },
        };
}

public enum MachineCycle
{
    FETCH,
    RAM_READ, RAM_READ_IMM,
    RAM_WRITE, 
    EXECUTE_ALU,
    EXECUTE_MOV,

    NONE, JMP, CALL, LDA, STA, LHLD, SHLD, //FIXED INSTRUCTIONS
}