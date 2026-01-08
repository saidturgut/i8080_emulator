namespace i8080_emulator.Decoding.Multiplexer;
using Executing.Computing;
using Signaling.Cycles;
using Executing;
using Signaling;

public partial class DecoderMultiplexer
{
    protected Decoded ROT(byte opcode)
    {
        Decoded decoded = new()
        {
            AluOperation = new ALUOperation
            {
                Operation = Operation.ROT,
                Opcode = EncodedRotators[BB_XXX_BBB(opcode)],
                A = Register.A,
                FlagMask = FlagMask.C,
            },
            DataLatcher = Register.A,
        };
        decoded.Cycles.Add(MachineCycle.ALU_EXECUTE);
        return decoded;
    }
    
    protected Decoded DAD(byte opcode)
    {
        Decoded decoded = new()
        {
            AluOperation = new ALUOperation
            {
                Operation = Operation.ADD,
                Opcode = ALUOpcode.DAD,
                A = EncodedRegisterPairs[GetRegisterPair(opcode) - 4][0],
                B = EncodedRegisterPairs[GetRegisterPair(opcode) - 4][1],
                FlagMask = FlagMask.C
            }
        };
        decoded.Cycles.Add(MachineCycle.ALU_EXECUTE);
        return decoded;
    }
}