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
        
        while (!DataPath.HALT)
        {
            DataPath.HostInput();
            
            Tick();
            
            //Thread.Sleep(100);
        }
        
        DataPath.MemoryDump();
    }

    private void Tick()
    {
        // COMBINATIONAL PHASE
        DataPath.Clear();
        DataPath.Set(
        ControlUnit.Emit());

        DataPath.ALUControl();
        
        DataPath.PreIncrement();
        DataPath.AddressBuffer();
        DataPath.MultiplexerDrive();
        
        DataPath.IOControl();
        
        DataPath.MultiplexerLatch();
        DataPath.Increment();
        
        ControlUnit.Decode(
        DataPath.GetValues());
        
        // FALLING EDGE
        DataPath.Commit();
        DataPath.Debug();

        ControlUnit.Advance(DataPath.HALT);
    }
}