namespace i8080_emulator.Signaling;
using Decoding;

public partial class ControlUnit
{        
    private readonly DecoderCore Decoder = new DecoderCore();
    private readonly Sequencer Sequencer = new Sequencer();
    
    private MachineCycle currentCycle;
    
    public bool HALT;
    
    public SignalSet Emit(byte IR)
    {
        if (IR == 0x76) HALT = true; // HLT
        
        currentCycle = decoded.Table[Sequencer.mState];

        SignalSet signals = MachineCyclesMethod[currentCycle](Sequencer.tState);
        
        if (signals.SideEffect == SideEffect.DECODE)
            decoded = Decoder.Decode(IR);
        
        return signals;
    }
    
    public void Advance()
        => Sequencer.Advance((byte)
            (decoded.Table.Count - 1), 
            MachineCyclesLength[currentCycle]);
}