using i8080_emulator.Signaling;

namespace i8080_emulator.Decoding.Multiplexer;
using Signaling.Cycles;

public partial class DecoderMultiplexer
{
    protected Decoded FamilyFXD(MachineCycle machineCycle)
    {
        Decoded decoded = new Decoded();

        if (machineCycle != MachineCycle.EMPTY) 
            decoded.Cycles.Add(machineCycle);
        
        return decoded;
    }

    protected Decoded COPY_HL(byte pairIndex)
    {
        Decoded decoded = new Decoded
        {
            RegisterPairs = EncodedRegisterPairs[pairIndex],
        };
        
        if (pairIndex == 1) decoded.SideEffect = SideEffect.SWAP;
        
        decoded.Cycles.Add(MachineCycle.COPY_HL_LOW);
        decoded.Cycles.Add(MachineCycle.COPY_HL_HIGH);
        return decoded;
    }
}