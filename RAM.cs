namespace i8080_emulator;
using Executing;

public class RAM
{
    public readonly Dictionary<ushort, byte> MemoryDump = new();

    private byte[] Memory = new byte[0x10000]; // 64 KB
    
    public void LoadArray(ushort address, byte[] data)
    {
        for (int i = 0; i < data.Length; i++)
        {
            Memory[i + address] = data[i];
        }    
    }

    public void LoadByte(int address, byte value)
    {
        Memory[address] = value;
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

    public void COLD()
    {
        for (int i = 0xD800; i <= 0xD9FF; i++)
            Memory[i] = 0x00;
        
        Memory[0xCF42] = 0x00;
        
        for (int i = 0; i < 16; i++)
            Memory[0xF300 + i] = 0x00;
        
        Memory[0xCF43] = 0x00;   // low
        Memory[0xCF44] = 0xF3;   // high
        
        Memory[0xFFFF] = 0x76;   // HLT
    }
    
    public void HexDump()
    {
        const int bytesPerLine = 16;

        for (int i = 0; i < Memory.Length; i += bytesPerLine)
        {
            // address
            Console.Write($"{0 + i:X4}: ");

            // hex bytes
            for (int j = 0; j < bytesPerLine; j++)
            {
                if (i + j < Memory.Length)
                    Console.Write($"{Memory[i + j]:X2} ");
                else
                    Console.Write("   ");
            }

            Console.Write(" |");

            // ASCII
            for (int j = 0; j < bytesPerLine && i + j < Memory.Length; j++)
            {
                byte b = Memory[i + j];
                Console.Write(b >= 32 && b <= 126 ? (char)b : '.');
            }

            Console.WriteLine("|");
        }
    }
}