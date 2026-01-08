namespace i8080_emulator;
using Executing;
using Signaling;

public class CPU
{    
    private readonly DataPath DataPath = new ();
    private readonly ControlUnit ControlUnit = new ();
    
    public void PowerOn() => Clock();
    
    private void Clock()
    {
        DataPath.Init();
        
        while (!ControlUnit.HALT)
        {
            Tick();
        }
        
        DataPath.MemoryDump();
    }

    private void Tick()
    {
        DataPath.Commit();

        DataPath.Set(
        ControlUnit.Emit());
        
        DataPath.Clear();
        DataPath.Debug();

        DataPath.ControlALU();
        DataPath.AddressBuffer();
        DataPath.MultiplexerDrive();
        
        DataPath.MultiplexerLatch();
        DataPath.Incrementer();
        
        ControlUnit.Decode(
        DataPath.GetIR());
        
        ControlUnit.Advance();
    }
}