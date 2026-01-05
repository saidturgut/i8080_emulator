namespace i8080_emulator.Executing;
using Computing;
using Signaling;

// ALU RESOLVER, ADDRESS BUFFER, MULTIPLEXER, INCREMENTER
public partial class DataPath
{
    private readonly RAM RAM = new RAM();
    private readonly ALU ALU = new ALU();
    
    private readonly Bus DBUS = new Bus(); // DATA BUS 
    private readonly Bus ABUS_H = new Bus(); 
    private readonly Bus ABUS_L = new Bus(); 
    
    private byte PC_H, PC_L; // PROGRAM COUNTER
    private byte SP_H, SP_L; // STACK POINTER
    
    public byte IR; // INSTRUCTION REGISTER
    
    private byte A, // ACCUMULATOR
        B, C, D, E, // GENERAL REGISTERS
        H, L; // MEMORY ADDRESS REGISTERS

    private byte TMP; // BRIDGE BETWEEN RAM AND REGISTERS

    private byte FLAGS = 0x2;
    
    private SignalSet signals = new SignalSet();
    
    public void Init()
    {
        RAM.Init();
    }
    
    public void Clear()
    {        
        DBUS.Clear();
        ABUS_H.Clear();
        ABUS_L.Clear();
    }

    public void Set(SignalSet input)
    {
        signals = input;
    }

    public void Debug()
    {
        Console.WriteLine($"PROGRAM COUNTER : {(ushort)((PC_H << 8) + PC_L)}");
        Console.WriteLine($"IR : {IR}");
        Console.WriteLine($"B : {B}");
        Console.WriteLine($"C : {C}");
        Console.WriteLine($"D : {D}");
        Console.WriteLine($"E : {E}");
        Console.WriteLine($"H : {H}");
        Console.WriteLine($"L : {L}");
        Console.WriteLine($"A : {A}");
        Console.WriteLine($"HL : {(ushort)((H << 8) + L)}");
        Console.WriteLine(
            $"FLAGS : S={(FLAGS >> 7) & 1} Z={(FLAGS >> 6) & 1} AC={(FLAGS >> 4) & 1} P={(FLAGS >> 2) & 1} CY={(FLAGS >> 0) & 1}");
    }

    public void MemoryDump()
    {
        Console.WriteLine();
        foreach (var slot in RAM.MemoryDump)
        {
            Console.WriteLine($"MEMORY[{slot.Key}] : {slot.Value}");
        }
    }
}