namespace i8080_emulator.Executing;

public class ClockedRegister(Register nam)
{
    public Register name = nam;
    private byte value;
    private byte temp;

    public void Init(byte input)
    {
        value = input;
        temp = input;
    }
    
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

public enum Register
{
    NONE = 0,
    PC_L = 1, PC_H = 2, 
    TMP = 3,
    A = 4, B = 5, C = 6, D = 7, E = 8, 
    HL_L = 9, HL_H = 10, 
    SP_L = 11, SP_H = 12, 
    WZ_L = 13, WZ_H = 14, 
    FLAGS = 15,
    IR = 16, RAM = 17
}
