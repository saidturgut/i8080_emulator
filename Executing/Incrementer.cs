namespace i8080_emulator.Executing;
using Computing;
using Signaling;

public partial class DataPath
{
    public void Increment()
    {
        if(signals.SideEffect == SideEffect.NONE || !Permit())
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
            Registers[Register.FLAGS].Set((byte)
                (Registers[Register.FLAGS].Get() | (byte)ALUFlag.Carry));
        if (signals.SideEffect == SideEffect.CMC)
            Registers[Register.FLAGS].Set((byte)
                (Registers[Register.FLAGS].Get() ^ (byte)ALUFlag.Carry));
    }

    private static void Increment(ClockedRegister low, ClockedRegister high)
    {
        byte prev = low.Get();
        low.Set((byte)(prev + 1));
        
        if (prev == 0xFF)
            high.Set((byte)(high.Get() + 1));
    }
    
    private static void Decrement(ClockedRegister low, ClockedRegister high)
    {
        byte prev = low.Get();
        low.Set((byte)(prev - 1));
        
        if (prev == 0x00)
            high.Set((byte)(high.Get() - 1));
    }
}