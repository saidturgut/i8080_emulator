using i8080_emulator.Testing;

namespace i8080_emulator;
using Executing;

public class Ram
{
    private readonly byte[] Memory = new byte[0x10000];

    public bool DEBUG_MODE;
    private const bool HEX_DUMP = false;
    
    public void Init()
    {
       HexLoader.Load(File.ReadAllLines("tinybas.hex"), Memory);
       
        if (HEX_DUMP) HexDump.Run(Memory);
    }
    
    public void Read(TriStateBus aBusL, TriStateBus aBusH, TriStateBus dBus)
    {
        dBus.Set(Memory[Merge(aBusL.Get(), aBusH.Get())]);
    }
    
    public void Write(TriStateBus aBusL, TriStateBus aBusH, TriStateBus dBus)
    {
        Memory[Merge(aBusL.Get(), aBusH.Get())] = dBus.Get();
        if(!DEBUG_MODE) return;
        Console.WriteLine($"RAM[{Hex(Merge(aBusL.Get(), aBusH.Get()))}]: {Hex(dBus.Get())}");
    }

    private ushort Merge(byte low, byte high)
        => (ushort)(low + (high << 8));
    
    private string Hex(ushort input)         
        => $"0x{Convert.ToString(input, 16).ToUpper()}";
}