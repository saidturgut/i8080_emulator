namespace i8080_emulator.Decoding.Multiplexer;
using Executing.Computing;
using Signaling.Cycles;
using Signaling;
using Executing;

public partial class DecoderMultiplexer
{
    private Decoded WZ_AND_JMP() => new()
    {
        DrivePairs = EncodedRegisterPairs[4], // PC
        LatchPairs = EncodedRegisterPairs[5], // WZ
        SideEffect = SideEffect.JMP,
    };
    
    protected Decoded JMP()
    {
        Decoded decoded = WZ_AND_JMP();
        
        decoded.Cycles.Add(MachineCycle.RAM_READ_IMM_LOW);
        decoded.Cycles.Add(MachineCycle.RAM_READ_IMM_HIGH);
        
        decoded.Cycles.Add(MachineCycle.MICRO_CYCLE);
        return decoded;
    }
    
    protected Decoded RST()
    {
        Decoded decoded = new Decoded
        {
            DrivePairs = EncodedRegisterPairs[4], // M2, M3 | PC
            DataDriver = Register.IR, // M1
            DataLatcher = Register.WZ_L,// M1
            SideEffect = SideEffect.JMP, // M4
        };
        decoded.Cycles.Add(MachineCycle.INTERNAL_LATCH);
        
        decoded.Cycles.Add(MachineCycle.PUSH_HIGH);
        decoded.Cycles.Add(MachineCycle.PUSH_LOW);
        
        decoded.Cycles.Add(MachineCycle.MICRO_CYCLE);
        return decoded;
    }
    
    protected Decoded CALL()
    {
        Decoded decoded = WZ_AND_JMP();
        
        decoded.Cycles.Add(MachineCycle.RAM_READ_IMM_LOW);
        decoded.Cycles.Add(MachineCycle.RAM_READ_IMM_HIGH);
        
        decoded.Cycles.Add(MachineCycle.PUSH_HIGH);
        decoded.Cycles.Add(MachineCycle.PUSH_LOW);

        decoded.Cycles.Add(MachineCycle.MICRO_CYCLE);

        return decoded;
    }
    
    protected Decoded RET()
    {
        Decoded decoded = WZ_AND_JMP();
        
        decoded.Cycles.Add(MachineCycle.POP_LOW);
        decoded.Cycles.Add(MachineCycle.POP_HIGH);
        decoded.Cycles.Add(MachineCycle.MICRO_CYCLE);
        return decoded;
    }
    
    protected Decoded COPY_HL(byte pairIndex)
    {
        Decoded decoded = new Decoded
        {
            DrivePairs = EncodedRegisterPairs[2],// HL
            LatchPairs = EncodedRegisterPairs[pairIndex],
        };
        
        if (pairIndex == 1) decoded.SideEffect = SideEffect.SWAP;
        
        decoded.Cycles.Add(MachineCycle.COPY_RP_LOW);
        decoded.Cycles.Add(MachineCycle.COPY_RP_HIGH);
        return decoded;
    }
}