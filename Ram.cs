namespace intel8080;
using Executing.Components;
using Testing;

public class Ram
{
    private readonly byte[] Memory = new byte[0x10000];

    private readonly byte[] Tester =
    [
        0x3C,
        0x00,
        0x00,
        0xC3, 0x08, 0x00,
        0x76,
        0x76,
        0xD7, 0x76, 0x00,
        0x76, 0x76, 0x76, 0x76, 0x76, 
        0x3C,
        0xC9,
    ];
    
    public void Init()
    {
        if (true) HexDump.Run(Memory);
    }

    public void LoadByte(ushort address, byte data)
        => Memory[address] = data;
    
    public void LoadArray(ushort address, byte[] data)
    {
        for (int i = 0; i < data.Length; i++)
            Memory[address + i] = data[i];
    }

    public byte Read(ushort address)
        => Memory[address];
    
    public void Write(ushort address, byte data)
        => Memory[address] = data;
}