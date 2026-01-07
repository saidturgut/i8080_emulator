namespace i8080_emulator.Executing;
using Computing;
using Signaling;

public partial class DataPath
{
    public void Incrementer()
    {
        if(signals.SideEffect == SideEffect.NONE)
            return;
        
        if (PairIncrements.TryGetValue(signals.SideEffect, out var pair))
        {
            if (!pair.Decrement)
                Increment(pair.Pair[0], pair.Pair[1]);
            else
                Decrement(pair.Pair[0], pair.Pair[1]);
            return;
        }
        
        // CARRY FLAG CONTROLS
        if (signals.SideEffect == SideEffect.STC)
            FLAGS |= (byte)ALUFlag.Carry;
        if (signals.SideEffect == SideEffect.CMC)
            FLAGS ^= (byte)ALUFlag.Carry;
    }

    private static void Increment(RegisterObject low, RegisterObject high)
    {
        low.Set((byte)(low.Get() + 1));
        if(low.Get() == 0)
            high.Set((byte)(high.Get() + 1));
    }
    
    private static void Decrement(RegisterObject low, RegisterObject high)
    {
        low.Set((byte)(low.Get() - 1));
        if(low.Get() == 0xFF)
            high.Set((byte)(high.Get() - 1));
    }
}