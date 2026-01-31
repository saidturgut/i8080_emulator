namespace i8080_emulator;
using Signaling.Multiplexer;
using Signaling;
using Executing;

public class Cpu
{    
    private readonly DataPath DataPath = new ();
    private readonly MicroUnit MicroUnit = new ();
    
    private const bool DEBUG_MODE = false;
    
    public void PowerOn() => Clock();
    
    private void Clock()
    {
        DataPath.Init();
        MicroUnit.Init();
        
        while (MicroUnit.State is not State.HALT)
        {
            DataPath.HostInput();
            
            Tick();
        }
    }

    private void Tick()
    {
        MicroUnit.Debug(DEBUG_MODE);

        DataPath.Clear();
        DataPath.Receive(
        MicroUnit.Emit());
        
        DataPath.AddressDrive();
        DataPath.DataDrive();
        DataPath.AluAction();
        DataPath.IoControl();
        DataPath.DataLatch();

        DataPath.Commit();
        
        DataPath.Debug(DEBUG_MODE);
        
        MicroUnit.Advance(DataPath.GetIr(), DataPath.Psw);
    }
}