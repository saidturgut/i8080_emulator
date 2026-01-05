namespace i8080_emulator.Decoding;
using Executing.Computing;
using Signaling;

public class DecoderModel
{
    protected readonly Dictionary<byte, MachineCycle> FixedOpcodes
        = new ()
        {
            { 0x00, MachineCycle.NONE }, // NOP
            { 0x76, MachineCycle.NONE }, // HLT
            { 0xC3, MachineCycle.JMP },
            { 0xCD, MachineCycle.CALL },
            { 0x3A, MachineCycle.LDA },
            { 0x32, MachineCycle.STA },
            { 0x2A, MachineCycle.LHLD },
            { 0x22, MachineCycle.SHLD },
            
            { 0x37, MachineCycle.STC },
            { 0x3F, MachineCycle.CMC },
        };

    protected readonly Dictionary<ALUOpcode, Operation> ALUTable = new()
    {
        // 10 FAMILY
        { ALUOpcode.ADD, Operation.ADD }, // 000
        { ALUOpcode.ADC, Operation.ADD }, // 001
        { ALUOpcode.SUB, Operation.SUB }, // 010
        { ALUOpcode.SBB, Operation.SUB }, // 011
        { ALUOpcode.ANA, Operation.AND }, // 100
        { ALUOpcode.XRA, Operation.XOR }, // 101
        { ALUOpcode.ORA, Operation.OR }, // 110
        { ALUOpcode.CMP, Operation.SUB }, // 111

        // 00 FAMILY
        { ALUOpcode.INR, Operation.ADD }, // 100
        { ALUOpcode.DCR, Operation.SUB }, // 101
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