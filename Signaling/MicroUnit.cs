namespace i8080_emulator.Signaling;
using Executing.Components;
using Decoding.Multiplexer;
using Multiplexer;
using Decoding;

public class MicroUnit : MicroUnitMux
{
    private readonly Decoder Decoder = new();

    private SignalSet Signals = new();
    
    public State State = State.FETCH;
    
    private byte currentCycle;
    
    public void Init()
    {
        decoded = DecoderMux.FETCH();
    }
    
    public SignalSet Emit()
    {
        Signals = MicroCycles[(byte)decoded.MicroCycles[currentCycle]]();
        State = Signals.State;
        return Signals;
    }

    public void Debug(bool DEBUG_MODE)
    {
        if(!DEBUG_MODE) return;
        Console.WriteLine($"CYCLE: \"{decoded.MicroCycles[currentCycle]}\"");
    }

    public void Advance(byte ir, Psw psw)
    {
        if (Signals.Index) queueIndex++;
        
        if (currentCycle != decoded.MicroCycles.Count - 1)
        {
            currentCycle++;
        }
        else
        {
            switch (State)
            {
                case State.HALT: break;
                case State.FETCH: throw new Exception("ILLEGAL");
                case State.DECODE: decoded = Decoder.Decode(ir, psw); break;
                case State.EXECUTE: decoded = DecoderMux.FETCH(); break;
            }
            
            currentCycle = 0;
            queueIndex = 0;
        }
    }
}