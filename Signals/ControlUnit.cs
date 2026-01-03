namespace i8080_emulator.Signals;

public class ControlUnit : SignalSets
{
    private readonly Sequencer Sequencer = new Sequencer();
    private MachineCycle currentCycle;
    
    public bool Terminate;
    
    public SignalSet Emit(byte IR)
    {
        if (IR == 0x76) Terminate = true; // HLT
        
        currentCycle = decoded.Table[Sequencer.mState];
        
        switch (currentCycle)
        {
            case MachineCycle.FETCH:
                return Fetch();
            case MachineCycle.DECODE:
                return Decode(IR);
            case MachineCycle.RAM_READ:
                return RamRead();
            case MachineCycle.RAM_WRITE:
                return RamWrite();
            case MachineCycle.RAM_READ_IMM:
                return RamReadImm();
        }

        throw new Exception("UNKNOWN CYCLE");
    }
    
    public void Advance()
        => Sequencer.Advance((byte)(decoded.Table.Count - 1));
}