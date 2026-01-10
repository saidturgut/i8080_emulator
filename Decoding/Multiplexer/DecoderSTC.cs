namespace i8080_emulator.Decoding.Multiplexer;
using Executing.Computing;
using Signaling.Cycles;
using Signaling;
using Executing;

public partial class DecoderMultiplexer
{
    protected Decoded PUSH(byte opcode)
    {
        Register[] pair = BB_XXB_BBB(opcode) == 0b11
            ? [Register.A, Register.FLAGS]
            : EncodedRegisterPairs[BB_XXB_BBB(opcode)];

        Decoded decoded = new Decoded { DrivePairs = pair, };
        
        decoded.Cycles.Add(MachineCycle.PUSH_HIGH);
        decoded.Cycles.Add(MachineCycle.PUSH_LOW);

        decoded.Cycles.Add(MachineCycle.MICRO_CYCLE);
        
        return  decoded;
    }
    
    protected Decoded POP(byte opcode)
    {
        Register[] pair = BB_XXB_BBB(opcode) == 0b11
            ? [Register.A, Register.FLAGS]
            : EncodedRegisterPairs[BB_XXB_BBB(opcode)];

        Decoded decoded = new Decoded { LatchPairs = pair, };
        
        decoded.Cycles.Add(MachineCycle.POP_LOW);
        decoded.Cycles.Add(MachineCycle.POP_HIGH);

        return  decoded;
    }

    protected Decoded XTHL()
    {
        Decoded decoded = new Decoded
        {
            AddressDriver = Register.SP_L,
        };
        
        decoded.Cycles.Add(MachineCycle.RAM_READ_XTHL);
        decoded.Cycles.Add(MachineCycle.XTHL_HIGH);
        
        decoded.Cycles.Add(MachineCycle.RAM_READ_TMP);
        decoded.Cycles.Add(MachineCycle.XTHL_LOW);
        
        return decoded;
    }
}