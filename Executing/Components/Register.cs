namespace i8080_emulator.Executing.Components;

public class ClockedRegister
{
    private byte commit;
    private byte value;

    public void Init(byte input)
    {
        commit = input;
        value = input;
    }
    
    public void Set(byte input) 
        => value = input;

    public byte Get()
    {
        if (commit != value)
            throw new Exception($"COMMIT : {commit} | VALUE : {value} UNRELIABLE VALUE!!");
        
        return commit;
    }
    
    public void Commit() 
        => commit = value;
};
