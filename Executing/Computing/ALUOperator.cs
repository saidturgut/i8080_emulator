namespace i8080_emulator.Executing.Computing;
using Signals;

public class ALUOperator
{
    private ALU ALU = new ALU();
    
    public void OperateALU(SignalSet signals)
    {
        if(signals.AluOperation == ALUOperation.NONE)
            return;
        if (signals.AluOperation == ALUOperation.ADD)
            ALU.Compute(new ALUInput());
    }
}