namespace i8080_emulator.External;
using Executing;

public class IO
{
    private readonly Terminal Terminal = new();

    public void HostInput() => Terminal.HostInput();
    
    public void Read(TriStateBus aBusL, TriStateBus dBus)
    {
        dBus.Set(
            aBusL.Get() switch
            {
                0x00 => Terminal.ReadStatus(),
                0x01 => Terminal.ReadData(),
                _ => throw new Exception("INVALID READ PORT")
            }
        );
    }

    public void Write(TriStateBus aBusL, TriStateBus dBus)
    {
        switch (aBusL.Get())
        {
            case 0x02:
                Terminal.WriteData(dBus.Get()); break;
            default:
                throw new Exception("INVALID WRITE PORT");
        }
    }
}