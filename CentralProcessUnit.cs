namespace i8080_emulator;
using Executing;
using Signals;

public class CentralProcessUnit
{    
    private readonly DataPath DataPath = new DataPath();
    private readonly ControlUnit ControlUnit = new ControlUnit();
    
    public void PowerOn() => Clock();
    
    private void Clock()
    {
        DataPath.Init();
        
        while (!ControlUnit.Terminate)
        {
            Thread.Sleep(200);
            
            Tick();
        }
    }

    private void Tick()
    {
        DataPath.Clear();
        DataPath.Set
        (ControlUnit.Emit(DataPath.IR));
        
        DataPath.OperateALU();
        DataPath.AddressBuffer();
        DataPath.MultiplexerDrive();
        
        DataPath.MultiplexerLatch();
        DataPath.Incrementer();
        
        ControlUnit.Advance();
    }
}