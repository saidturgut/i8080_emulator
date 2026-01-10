namespace i8080_emulator.Executing;
using Signaling;

public partial class DataPath
{
    public void PreIncrement()
    {
        if(signals.SideEffect == SideEffect.NONE)
            return;

        if (signals.SideEffect == SideEffect.SP_NXT)
            Decrement(Registers[Register.SP_L], Registers[Register.SP_H]);
        
        if(PcOverriders.TryGetValue(signals.SideEffect, out var overrider))
        {
            Registers[Register.PC_L].Set(overrider[0].Get());
            Registers[Register.PC_H].Set(overrider[1].Get());
        }
    }
    
    public void AddressBuffer()
    {
        if(signals.AddressDriver == Register.NONE || !Permit())
            return;
        
        if (RegisterPairs.TryGetValue(signals.AddressDriver, out var addressPair))
        {
            byte low = addressPair[0].GetTemp();
            byte high = addressPair[1].GetTemp();
            
            if (signals.SideEffect == SideEffect.XTHL_SP)
            {        
                low++;
                if (low == 0x00) high++;
            }
            
            ABUS_L.Set(low);
            ABUS_H.Set(high);
        }
    }
}