namespace i8080_emulator;
using Executing;

public class RAM
{
    public Dictionary<ushort, byte> MemoryDump = new Dictionary<ushort, byte>();
    
    private readonly byte[] Memory = new byte[0x10000]; // 64 KB
    
    private readonly byte[] ROM =
    [
// --- Force CY = 1 ---
        0x3E, 0xFF,       // MVI A,FF
        0x06, 0x01,       // MVI B,01
        0x80,             // ADD B  (A=00, CY=1)

        // --- Hammer INR/DCR (CY must survive) ---
        0x0E, 0x0F,       // MVI C,0F
        0x0C,             // INR C  (10, AC=1)
        0x0D,             // DCR C  (0F, AC=1)

        0x0E, 0xFF,       // MVI C,FF
        0x0C,             // INR C  (00, Z=1)
        0x0D,             // DCR C  (FF, S=1)

        // --- Multiple wraps ---
        0x16, 0x7F,       // MVI D,7F
        0x14,             // INR D  (80)
        0x15,             // DCR D  (7F)

        0x16, 0x00,       // MVI D,00
        0x15,             // DCR D  (FF)

        // --- Final noise ---
        0x04,             // INR B
        0x05,             // DCR B

        0x76              // HLT
    ];
    
    public void Init()
    {
        for (int i = 0; i < ROM.Length; i++)
            Memory[i] = ROM[i];
    }
    
    public void Read(Bus aBus_H, Bus aBus_L, Bus dBus)
    {
        dBus.Set(Memory[Merge(aBus_H.Get(), aBus_L.Get())]);
    }

    public void Write(Bus aBus_H, Bus aBus_L, Bus dBus)
    {
        Memory[Merge(aBus_H.Get(), aBus_L.Get())] = dBus.Get();
        MemoryDump[Merge(aBus_H.Get(), aBus_L.Get())] = Memory[Merge(aBus_H.Get(), aBus_L.Get())];
    }

    private ushort Merge(byte high, byte low)
    {
        return  (ushort)((high << 8) + low);
    }
}