namespace i8080_emulator.Decoding;
using Executing.Computing;
using Signaling.Cycles;
using Executing;
using Signaling;

public class DecoderModel
{
    protected static readonly Dictionary<byte, FixedOpcode> FixedMicroCycles = new ()
        {
            { 0x76, new FixedOpcode(MachineCycle.HALT, SideEffect.HALT) }, // HLT
            
            { 0xE9, new FixedOpcode(MachineCycle.MICRO_CYCLE, SideEffect.PCHL) },

            { 0x37, new FixedOpcode(MachineCycle.MICRO_CYCLE, SideEffect.STC) },
            { 0x3F, new FixedOpcode(MachineCycle.MICRO_CYCLE, SideEffect.CMC) },
            { 0x2F, new FixedOpcode(MachineCycle.CMA, SideEffect.CMA) },
            
            { 0x00, new FixedOpcode(MachineCycle.EMPTY, SideEffect.NONE) }, // NOP
            { 0x40, new FixedOpcode(MachineCycle.EMPTY, SideEffect.NONE) }, // NOP
            { 0x49, new FixedOpcode(MachineCycle.EMPTY, SideEffect.NONE) }, // NOP
            { 0x52, new FixedOpcode(MachineCycle.EMPTY, SideEffect.NONE) }, // NOP
            { 0x5B, new FixedOpcode(MachineCycle.EMPTY, SideEffect.NONE) }, // NOP
            { 0x64, new FixedOpcode(MachineCycle.EMPTY, SideEffect.NONE) }, // NOP
            { 0x6D, new FixedOpcode(MachineCycle.EMPTY, SideEffect.NONE) }, // NOP
            { 0x7F, new FixedOpcode(MachineCycle.EMPTY, SideEffect.NONE) }, // NOP
            
            { 0x08, new FixedOpcode(MachineCycle.EMPTY, SideEffect.NONE) }, // NOP
            { 0x10, new FixedOpcode(MachineCycle.EMPTY, SideEffect.NONE) }, // NOP
            { 0x18, new FixedOpcode(MachineCycle.EMPTY, SideEffect.NONE) }, // NOP
            { 0x20, new FixedOpcode(MachineCycle.EMPTY, SideEffect.NONE) }, // NOP
            { 0x28, new FixedOpcode(MachineCycle.EMPTY, SideEffect.NONE) }, // NOP
            { 0x30, new FixedOpcode(MachineCycle.EMPTY, SideEffect.NONE) }, // NOP
            { 0x38, new FixedOpcode(MachineCycle.EMPTY, SideEffect.NONE) }, // NOP
        };
    
    protected static readonly Dictionary<ALUOpcode, Operation> ALU_10 = new()
    {
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
        [Register.PC_L, Register.PC_H],
        [Register.WZ_L, Register.WZ_H],
    };

    protected static readonly SideEffect[] IncrementOpcodes =
    {
        SideEffect.BC_INC, SideEffect.DE_INC, 
        SideEffect.HL_INC, SideEffect.SP_INC,
        SideEffect.BC_DCR, SideEffect.DE_DCR,
        SideEffect.HL_DCR, SideEffect.SP_DCR, 
    };

    protected static readonly ALUOpcode[] EncodedRotators =
    {
        ALUOpcode.RLC, ALUOpcode.RRC, ALUOpcode.RAL, ALUOpcode.RAR, ALUOpcode.DAA,
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
    protected static byte BBBB_XXXX(byte opcode)
        => (byte)(opcode & 0b0000_1111);
    protected static byte XXXX_BBBB(byte opcode)
        => (byte)((opcode & 0b1111_0000) >> 4);

    protected static int GetRegisterPair(byte opcode) =>
        (((opcode & 0x30) >> 4) + ((opcode & 0x8) >> 3) * 4);
    protected static int GetRegisterPairNormal(byte opcode) => 
        (opcode & 0x30) >> 4;
}

public struct FixedOpcode(MachineCycle cycle, SideEffect effect)
{
    public readonly MachineCycle MachineCycle = cycle;
    public readonly SideEffect SideEffect = effect;
}