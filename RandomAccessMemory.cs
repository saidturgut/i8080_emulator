namespace i8080_emulator;
using Executing;

public class RandomAccessMemory
{
    private readonly byte[] Memory = new byte[0x10000]; // 64 KB

    public void Init()
    {
        Memory[0] = 0x36;
        Memory[1] = 0x99;
        Memory[2] = 0x76;
    }
    
    public void Read(Bus aBus_H, Bus aBus_L, Bus dBus)
    {
        dBus.Set(Memory[MergeAddress(aBus_H.Get(), aBus_L.Get())]);
    }

    public void Write(Bus aBus_H, Bus aBus_L, Bus dBus)
    {
        Memory[MergeAddress(aBus_H.Get(), aBus_L.Get())] = dBus.Get();
        
        Console.WriteLine(Memory[0x1234]);
    }

    private ushort MergeAddress(byte high, byte low)
    {
        return  (ushort)((high << 8) + low);
    }
}