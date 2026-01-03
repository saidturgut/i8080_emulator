namespace i8080_emulator.Executing;
using Signals;

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
    }
}