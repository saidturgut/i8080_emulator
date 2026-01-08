namespace i8080_emulator;
using Executing;

public class RAM
{
    public readonly Dictionary<ushort, byte> MemoryDump = new();
    
    private readonly byte[] Memory = new byte[0x10000]; // 64 KB
    
    private readonly byte[] ROM =
    [
        0x21, 0x34, 0x12,   // LXI H,1234h
        0x11, 0x78, 0x56,   // LXI D,5678h
        0xEB,               // XCHG
        0x76
    ];
    
    public void Init()
    {
        for (int i = 0; i < ROM.Length; i++)
            Memory[i] = ROM[i];
    }
    
    public void Read(TriStateBus aBusH, TriStateBus aBusL, TriStateBus dBus)
    {
        dBus.Set(Memory[Merge(aBusH.Get(), aBusL.Get())]);
    }

    public void Write(TriStateBus aBusH, TriStateBus aBusL, TriStateBus dBus)
    {
        Memory[Merge(aBusH.Get(), aBusL.Get())] = dBus.Get();
        MemoryDump[Merge(aBusH.Get(), aBusL.Get())] = Memory[Merge(aBusH.Get(), aBusL.Get())];
    }

    private ushort Merge(byte high, byte low)
    {
        return  (ushort)((high << 8) + low);
    }
}