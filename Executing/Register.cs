namespace i8080_emulator.Executing;

public class ClockedRegister(Register nam)
{
    public Register name = nam;
    private byte value;
    private byte temp;
    
    public void Commit() 
        => value = temp;
    
    public void Set(byte input) 
        => temp = input;

    public byte Get()
    {
        if (value != temp)
            throw new Exception($"NAME : {name} | ABSOLUTE : {value} / TEMPORARY : {temp} UNRELIABLE VALUE!!");
        
        return value;
    }
    
    public byte GetTemp()
        => temp;
};

public class PipelineRegister
{
    private byte value;
    
    public void Set(byte input) 
        => value = input;
   
    public byte Get() => value;
}