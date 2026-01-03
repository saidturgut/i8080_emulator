namespace i8080_emulator.Decoding;
using Executing.Computing;
using Signaling;

public class DecoderModel
{
    protected readonly Dictionary<byte, MachineCycle> FixedOpcodes
        = new ()
        {
            { 0x00, MachineCycle.NONE },
            { 0x76, MachineCycle.NONE },
            { 0xC3, MachineCycle.JMP },
            { 0xCD, MachineCycle.CALL },
            { 0x3A, MachineCycle.LDA },
            { 0x32, MachineCycle.STA },
            { 0x2A, MachineCycle.LHLD },
            { 0x22, MachineCycle.SHLD },
        };

    protected readonly Operation[] ALUOperations =
    {
        Operation.ADD, // ADD
        Operation.ADD, // ADC
        Operation.SUB, // SUB
        Operation.SBB, // SBB
        Operation.AND, // ANA
        Operation.XOR, // XRA
        Operation.OR, // ORA
        Operation.SUB, // CMP
    };
    
    protected readonly DataDriver[] DataDrivers =
    {
        DataDriver.B,
        DataDriver.C,
        DataDriver.D,
        DataDriver.E,
        DataDriver.H,
        DataDriver.L,
        DataDriver.RAM,
        DataDriver.A,
    };
    
    protected readonly DataLatcher[] DataLatchers =
    {
        DataLatcher.B,
        DataLatcher.C,
        DataLatcher.D,
        DataLatcher.E,
        DataLatcher.H,
        DataLatcher.L,
        DataLatcher.RAM,
        DataLatcher.A,
    };
    
    protected byte BB_XXX_BBB(byte opcode)
    {
        return (byte)((opcode & 0b00_111_000) >> 3);
    }

    protected byte BB_BBB_XXX(byte opcode)
    {
        return (byte)(opcode & 0b00_000_111);
    }
}