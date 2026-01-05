namespace i8080_emulator.Executing;
using Computing;
using Signaling;

// ALU RESOLVER, ADDRESS BUFFER, MULTIPLEXER, INCREMENTER
public partial class DataPath : DataPathModel
{
    private readonly RAM RAM = new ();
    private readonly ALU ALU = new ();
    
    private readonly Bus DBUS = new (); // DATA BUS 
    private readonly Bus ABUS_H = new (); 
    private readonly Bus ABUS_L = new ();
    
    private byte FLAGS;
    
    private SignalSet signals = new ();
    
    public override void Init()
    {
        base.Init();
        
        Registers[R.A].Set(0x12);
        Registers[R.E].Set(0x34);
        
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
        Console.WriteLine($"PROGRAM COUNTER : {(ushort)((Registers[R.PC_H].Get() << 8) + Registers[R.PC_L].Get())}");
        Console.WriteLine($"IR : {Registers[R.IR].Get()}");
        Console.WriteLine($"B : {Registers[R.B].Get()}");
        Console.WriteLine($"C : {Registers[R.C].Get()}");
        Console.WriteLine($"D : {Registers[R.D].Get()}");
        Console.WriteLine($"E : {Registers[R.E].Get()}");
        Console.WriteLine($"H : {Registers[R.H].Get()}");
        Console.WriteLine($"L : {Registers[R.L].Get()}");
        Console.WriteLine($"A : {Registers[R.A].Get()}");
        Console.WriteLine($"HL : {(ushort)((Registers[R.H].Get() << 8) + Registers[R.L].Get())}");
        Console.WriteLine($"SP : {(ushort)((Registers[R.SP_H].Get() << 8) + Registers[R.SP_L].Get())}");
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