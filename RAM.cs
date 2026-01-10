using i8080_emulator.InputOutput;

namespace i8080_emulator;
using Executing;

public class RAM
{
    public readonly Dictionary<ushort, byte> MemoryDump = new();
    
    private readonly byte[] Memory = new byte[0x10000]; // 64 KB
    
    private readonly byte[] ROM =
    [
        0x0E, 0x41,        // MVI C,'A'
        0xCD, 0xC0, 0xF3,  // CALL 0xF3C0
        0x76
    ];
    
    public void Init()
    {
        for (int i = 0; i < BIOS.VECTORS.Length; i++)
            Memory[i + 0x0000] = BIOS.VECTORS[i];
        
        byte[] program = File.ReadAllBytes(Path.Combine(AppContext.BaseDirectory, "Tests", "8080EXER.COM"));
        
        for (int i = 0; i < program.Length; i++)
            Memory[i + 0x0100] = program[i];
        
        for (int i = 0; i < BIOS.WBOOT.Length; i++)
            Memory[i + 0xF200] = BIOS.WBOOT[i];
        
        for (int i = 0; i < BIOS.JUMP_TABLE.Length; i++)
            Memory[i + 0xF300] = BIOS.JUMP_TABLE[i];

        for (int i = 0; i < BIOS.RET.Length; i++)
            Memory[i + 0xF380] = BIOS.RET[i];

        for (int i = 0; i < BIOS.CONST.Length; i++)
            Memory[i + 0xF3A0] = BIOS.CONST[i];

        for (int i = 0; i < BIOS.CONIN.Length; i++)
            Memory[i + 0xF3B0] = BIOS.CONIN[i];
        
        for (int i = 0; i < BIOS.CONOUT.Length; i++)
            Memory[i + 0xF3C0] = BIOS.CONOUT[i];
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