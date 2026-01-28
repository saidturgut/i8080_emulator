namespace i8080_emulator.Executing;
using Computing;
using Signaling;

public partial class DataPath
{
    private readonly Alu Alu = new();
    
    public void AluAction()
    {
        if(signals.AluAction is null)
            return;

        AluAction action = signals.AluAction.Value;
        
        AluOutput output = Alu.Compute(new AluInput
        {
            A = Registers[(byte)Register.A].Get(),
            B = Registers[(byte)Register.TMP].Get(),
            C = (byte)(GetFlag(PswFlag.Carry) && action.UseCarry ? 1 : 0),
        }, action.Operation);
        
        Registers[(byte)Register.A].Set(output.Result);
        
        Registers[(byte)Register.PSW].Set((byte)(
            (Registers[(byte)Register.PSW].Get() & (byte)~action.FlagMask) | 
            (output.Flags & (byte)action.FlagMask)));
    }
}