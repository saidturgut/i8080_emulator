namespace i8080_emulator;
using Executing;

public class RAM
{
    public readonly Dictionary<ushort, byte> MemoryDump = new();
    
    private readonly byte[] Memory = new byte[0x10000]; // 64 KB
    
    private readonly byte[] ROM =
    [
        // LXI SP,3000h
        0x31, 0x00, 0x30,

        // LXI H,1234h
        0x21, 0x34, 0x12,

        // PUSH H
        0xE5,

        // LXI H,ABCDh
        0x21, 0xCD, 0xAB,

        // XTHL
        0xE3,

        // HLT
        0x76                   // HLT
    ];
    
    public void Init()
    {
        for (int i = 0; i < ROM.Length; i++)
            Memory[i] = ROM[i];
    }
    
    public void Read(TriStateBus aBusH, TriStateBus aBusL, TriStateBus dBus)
    {
        Console.WriteLine("RAM READ : " + Merge(aBusH.Get(), aBusL.Get()));
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