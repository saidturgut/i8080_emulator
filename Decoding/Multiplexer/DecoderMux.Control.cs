namespace i8080_emulator.Decoding.Multiplexer;
using Signaling.Multiplexer;
using Signaling;

// 11b INSTRUCTIONS
public partial class DecoderMux
{
    protected static Decoded PUSH(Cft type) => new()
    {
        AddressDriver = Register.SP_L,
        DataDriver = Register.RAM,
        DataLatcher = Register.PC_H,
        
        EncodeLatch = (byte)(zz_xxx_zzz() << 3), // OPERAND * 8
        
        Queue = 
        [
            Register.SP_L, EncodedPairsPsw[type is Cft.CALL or Cft.RST ? 0x4 : zz_xxz_zzz()][1], 
            Register.SP_L, EncodedPairsPsw[type is Cft.CALL or Cft.RST ? 0x4 : zz_xxz_zzz()][0], 
            ..type switch
            {
                Cft.CALL => JMP(Cft.JMP).Queue,
                Cft.RST => RST().Queue,
                _ => []
            }
        ],
        
        MicroCycles = [MicroCycle.DEC_PAIR, MicroCycle.LOAD_PAIR, MicroCycle.DEC_PAIR, MicroCycle.LOAD_PAIR,
            ..type switch
            {
                Cft.CALL => JMP(Cft.JMP).MicroCycles,
                Cft.RST => RST().MicroCycles,
                _ => []
            }]
    };
    
    protected static Decoded JMP(Cft type) => new()
    {
        Queue = [Register.WZ_L, Register.WZ_H, Register.WZ_L, Register.PC_L, Register.WZ_H, Register.PC_H],
        MicroCycles = [..MovePairImm, ..SwapPairs]
    };
    
    private static Decoded RST() => new()
    {
        Queue = [Register.PC_L],
        MicroCycles = [MicroCycle.LOAD_PAIR_ENCODE, MicroCycle.ZERO_REG]
    };
    
    protected static Decoded POP(Cft type) => new()
    {
        AddressDriver = Register.SP_L,
        IncAction = IncAction.INC,
        DataDriver = Register.RAM,
        
        Queue = [EncodedPairsPsw[type is Cft.RET ? 0x4 : zz_xxz_zzz()][0], EncodedPairsPsw[type is Cft.RET ? 0x4 : zz_xxz_zzz()][1]],
        
        MicroCycles = [..MovePairLoad]
    };
    
    protected enum Cft
    {
        PUSH, POP, JMP, CALL, RET, RST,
    }
}