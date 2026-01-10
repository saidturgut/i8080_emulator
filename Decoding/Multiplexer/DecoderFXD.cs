using i8080_emulator.Executing;
using i8080_emulator.Signaling;

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
    
    protected Decoded IO(bool input)
    {
        Decoded decoded = new Decoded
        {
            LatchPairs = EncodedRegisterPairs[5],// WZ
            AddressDriver = Register.WZ_L,
        };

        if (input)
        {
            decoded.DataLatcher = Register.A;
            decoded.SideEffect = SideEffect.IO_READ;
        }
        else
        {
            decoded.DataDriver = Register.A;
            decoded.SideEffect = SideEffect.IO_WRITE;
        }
        
        decoded.Cycles.Add(MachineCycle.RAM_READ_IMM_LOW);
        decoded.Cycles.Add(MachineCycle.EXECUTE_DECODED);
        
        return decoded;
    }
}