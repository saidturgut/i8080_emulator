using i8080_emulator.Executing.Computing;

namespace i8080_emulator.Executing;
using Signaling;

public partial class DataPath
{
    public void Incrementer()
    {
        if(signals.SideEffect is SideEffect.NONE or SideEffect.DECODE)
            return;

        if (RegisterPairs.TryGetValue(signals.SideEffect, out var pair))
        {
            if (!pair.Decrement)
            {
                pair.Pair[0].Set((byte)(pair.Pair[0].Get() + 1));
                if(pair.Pair[0].Get() == 0)
                    pair.Pair[1].Set((byte)(pair.Pair[1].Get() + 1));
            }
            else
            {
                pair.Pair[0].Set((byte)(pair.Pair[0].Get() - 1));
                if(pair.Pair[0].Get() == 0xFF)
                    pair.Pair[1].Set((byte)(pair.Pair[1].Get() - 1));
            }
            return;
        }
        
        // CARRY FLAG CONTROLS
        if (signals.SideEffect == SideEffect.STC)
            FLAGS |= (byte)ALUFlags.Carry;
        if (signals.SideEffect == SideEffect.CMC)
            FLAGS ^= (byte)ALUFlags.Carry;
    }
}