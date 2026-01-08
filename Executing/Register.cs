using System.ComponentModel;

namespace i8080_emulator.Executing;

public class ClockedRegister(Register nam)
{
    private Register name = nam;
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
    NONE,
    PC_L, PC_H, 
    IR, 
    RAM, TMP,
    SP_L, SP_H, 
    A, B, C, D, E, 
    HL_L, HL_H, 
    WZ_H, WZ_L,
    FLAGS,
}
