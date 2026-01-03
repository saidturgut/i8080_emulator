namespace i8080_emulator;
using Executing;
using Signaling;

public class CPU
{    
    private readonly DataPath DataPath = new DataPath();
    private readonly ControlUnit ControlUnit = new ControlUnit();
    
    public void PowerOn() => Clock();
    
    private void Clock()
    {
        DataPath.Init();
        
        while (!ControlUnit.HALT)
        {
            Tick();
        }
    }

    private void Tick()
    {
        DataPath.Clear();
        DataPath.Set
        (ControlUnit.Emit(DataPath.IR));
        
        DataPath.ResolveALU();
        DataPath.AddressBuffer();
        DataPath.MultiplexerDrive();
        
        DataPath.MultiplexerLatch();
        DataPath.Incrementer();

        DataPath.Debug();
        
        ControlUnit.Advance();
    }
}