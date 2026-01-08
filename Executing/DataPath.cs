namespace i8080_emulator.Executing;
using Computing;
using Signaling;

// ALU RESOLVER, ADDRESS BUFFER, MULTIPLEXER, INCREMENTER
public partial class DataPath : DataPathROM
{
    private readonly RAM RAM = new ();
    private readonly ALU ALU = new ();
    
    private readonly TriStateBus DBUS = new (); // DATA BUS 
    private readonly TriStateBus ABUS_H = new (); 
    private readonly TriStateBus ABUS_L = new ();
    
    private byte FLAGS;
    
    private SignalSet signals = new ();
    
    public override void Init()
    {
        base.Init();
        /*
        Registers[R.B].Set(0);
        Registers[R.C].Set(0xFF);
        Registers[R.D].Set(0xFF);
        Registers[R.E].Set(0xFF);
        Registers[R.H].Set(0x12);
        Registers[R.L].Set(0xFE);
        */
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
        Console.WriteLine($"PROGRAM COUNTER : {(ushort)((Registers[Register.PC_H].Get() << 8) + Registers[Register.PC_L].Get())}");
        Console.WriteLine($"IR : {Registers[Register.IR].Get()}");
        Console.WriteLine($"TMP : {Registers[Register.TMP].Get()}");
        Console.WriteLine($"B : {Registers[Register.B].Get()}");
        Console.WriteLine($"C : {Registers[Register.C].Get()}");
        Console.WriteLine($"D : {Registers[Register.D].Get()}");
        Console.WriteLine($"E : {Registers[Register.E].Get()}");
        Console.WriteLine($"H : {Registers[Register.HL_H].Get()}");
        Console.WriteLine($"L : {Registers[Register.HL_L].Get()}");
        Console.WriteLine($"A : {Registers[Register.A].Get()}");
        Console.WriteLine($"HL : {(ushort)((Registers[Register.HL_H].Get() << 8) + Registers[Register.HL_L].Get())}");
        Console.WriteLine($"SP : {(ushort)((Registers[Register.SP_H].Get() << 8) + Registers[Register.SP_L].Get())}");
        Console.WriteLine($"WZ : {(ushort)((Registers[Register.WZ_H].Get() << 8) + Registers[Register.WZ_L].Get())}");
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