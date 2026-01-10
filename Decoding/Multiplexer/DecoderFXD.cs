namespace i8080_emulator.Decoding.Multiplexer;
using Signaling.Cycles;

public partial class DecoderMultiplexer
{
    protected Decoded FamilyFXD(FixedOpcode opcode)
    {
        Decoded decoded = new Decoded { SideEffect = opcode.SideEffect };

        decoded.Cycles.Add(opcode.MachineCycle);
        
        return decoded;
    }
}