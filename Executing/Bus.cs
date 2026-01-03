namespace i8080_emulator.Executing;

public class Bus
{
    private byte value;
    private bool driven;

    public void Clear()
    {
        driven = false;
    }
    
    public void Set(byte input)
    {
        if (driven)
            throw new Exception("BUS CONTENTION");
        
        driven = true;
        
        value = input;
    }

    public byte Get()
    {
        return value;
    }
}