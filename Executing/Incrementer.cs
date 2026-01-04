using i8080_emulator.Executing.Computing;

namespace i8080_emulator.Executing;
using Signaling;

public partial class DataPath
{
    public void Incrementer()
    {
        if(signals.SideEffect == SideEffect.NONE)
            return;
        
        // PROGRAM COUNTER
        if (signals.SideEffect == SideEffect.PC_INC)
        {
            PC_L++;
            if (PC_L == 0)
                PC_H++;
        }

        // CARRY FLAG CONTROLS
        if (signals.SideEffect == SideEffect.STC)
            FLAGS |= (byte)ALUFlags.Carry;
        if (signals.SideEffect == SideEffect.CMC)
            FLAGS ^= (byte)ALUFlags.Carry;
    }
}