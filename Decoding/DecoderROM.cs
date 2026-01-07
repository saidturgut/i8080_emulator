namespace i8080_emulator.Decoding;
using Executing.Computing;
using Signaling.Cycles;
using Executing;
using Signaling;

public class DecoderModel
{
    protected static readonly Dictionary<byte, MachineCycle> FixedOpcodes
        = new ()
        {
            { 0x00, MachineCycle.EMPTY }, // NOP
            { 0x76, MachineCycle.EMPTY }, // HLT
            
            { 0x37, MachineCycle.STC },
            { 0x3F, MachineCycle.CMC },
            { 0x2F, MachineCycle.CMA },
        };

    protected static readonly Dictionary<ALUOpcode, Operation> ALU_10 = new()
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
    };
    
    protected static readonly Dictionary<ALUOpcode, Operation> ALU_00 = new()
    {
        // 00 FAMILY
        { ALUOpcode.INR, Operation.ADD }, // 100
        { ALUOpcode.DCR, Operation.SUB }, // 101
    };

    protected static readonly Register[] EncodedRegisters =
    {
        Register.B, // 000
        Register.C, // 001
        Register.D, // 010
        Register.E, // 011
        Register.HL_H, // 100
        Register.HL_L, // 101
        Register.RAM, // 110
        Register.A, // 111
    };

    protected static readonly ALUOpcode[] CarryUsers =
    [
        ALUOpcode.ADC, ALUOpcode.SBB,
    ];
    
    protected static readonly Register[][] EncodedRegisterPairs =
    {
        [Register.C, Register.B],
        [Register.E, Register.D],
        [Register.HL_L, Register.HL_H],
        [Register.SP_L, Register.SP_H],
    };

    protected static readonly SideEffect[] IncrementOpcodes =
    {
        SideEffect.BC_INC, // 00
        SideEffect.DE_INC, // 01
        SideEffect.HL_INC, // 10
        SideEffect.SP_INC, // 11
        SideEffect.BC_DCR, // 00 + 4
        SideEffect.DE_DCR, // 01 + 4
        SideEffect.HL_DCR, // 10 + 4
        SideEffect.SP_DCR, // 11 + 4
    };

    protected static readonly ALUOpcode[] EncodedRotators =
    {
        ALUOpcode.RLC, ALUOpcode.RRC, ALUOpcode.RAL, ALUOpcode.RAR,
    };

    protected static byte BB_XXX_BBB(byte opcode) 
        => (byte)((opcode & 0b00_111_000) >> 3);

    protected static byte BB_BBB_XXX(byte opcode)
        => (byte)(opcode & 0b00_000_111);

    protected static byte BB_BBX_BBB(byte opcode)
        => (byte)((opcode & 0b00_001_000) >> 3);


    protected static byte BB_XXB_BBB(byte opcode)
        => (byte)((opcode & 0b00_110_000) >> 4);
    
    protected static byte BB_BXX_BBB(byte opcode)
        => (byte)((opcode & 0b00_011_000) >> 3);
    
    protected static int GetRegisterPair(byte opcode) =>
        (((opcode & 0x30) >> 4) + ((opcode & 0x8) >> 3) * 4);
    
    protected static int GetRegisterPairNormal(byte opcode) => 
        (opcode & 0x30) >> 4;
}