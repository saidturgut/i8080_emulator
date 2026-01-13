namespace i8080_emulator.Signaling;
using Decoding;
using Cycles;

public class ControlUnit : ControlUnitROM
{        
    private readonly Decoder Decoder = new ();
    private readonly Sequencer Sequencer = new ();
    
    private MachineCycle currentCycle = MachineCycle.FETCH;
    
    public SignalSet Emit() 
        => MachineCyclesMethod[currentCycle]();

    public void Decode(byte[] values)
    {
        if (currentCycle == MachineCycle.FETCH)
        {
            decoded = Decoder.Decode(values);
        }
    }

    public void Advance(bool HALT)
    {
        if(HALT)
        {
            return;
        }
        
        Sequencer.Advance((byte)(decoded.Cycles.Count - 1));
        currentCycle = decoded.Cycles[Sequencer.mState];
    }
}