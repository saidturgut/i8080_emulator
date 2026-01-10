namespace i8080_emulator.Executing;
using Computing;
using Signaling;

// ALU CONTROL, ADDRESS BUFFER, MULTIPLEXER, INCREMENTER
public partial class DataPath : DataPathROM
{
    private readonly RAM RAM = new ();
    private readonly ALU ALU = new ();
    
    private readonly TriStateBus DBUS = new (); // DATA BUS 
    private readonly TriStateBus ABUS_H = new (); 
    private readonly TriStateBus ABUS_L = new ();
    
    private readonly PipelineRegister IR = new ();
    
    private SignalSet signals = new ();
    
    public bool HALT;

    public override void Init()
    {
        base.Init();
        RAM.Init();
    }
    
    public void Clear()
    {
        DBUS.Clear();
        ABUS_H.Clear();
        ABUS_L.Clear();
    }

    public void Set(SignalSet input) =>      
        signals = input;

    public void Commit()
    {
        foreach (ClockedRegister register in Registers.Values)
            register.Commit();
        
        if(signals.SideEffect == SideEffect.HALT)
            HALT = true;
    }

    private bool Permit() 
        => signals.SideEffect != SideEffect.HALT && !PcOverriders.ContainsKey(signals.SideEffect);

    public void Debug()
    {
        if (Registers[Register.PC_L].GetTemp() == 0XFF)
        {
            Environment.Exit(5);
        }
        
        byte flags = Registers[Register.FLAGS].GetTemp();
        Console.WriteLine($"PROGRAM COUNTER : {(ushort)((Registers[Register.PC_H].GetTemp() << 8) + Registers[Register.PC_L].GetTemp())}");
        Console.WriteLine($"IR : {IR.Get()}");
        Console.WriteLine($"TMP : {Registers[Register.TMP].GetTemp()}");
        Console.WriteLine($"B : {Registers[Register.B].GetTemp()}");
        Console.WriteLine($"C : {Registers[Register.C].GetTemp()}");
        Console.WriteLine($"D : {Registers[Register.D].GetTemp()}");
        Console.WriteLine($"E : {Registers[Register.E].GetTemp()}");
        Console.WriteLine($"H : {Registers[Register.HL_H].GetTemp()}");
        Console.WriteLine($"L : {Registers[Register.HL_L].GetTemp()}");
        Console.WriteLine($"A : {Registers[Register.A].GetTemp()}");
        Console.WriteLine($"HL : {(ushort)((Registers[Register.HL_H].GetTemp() << 8) + Registers[Register.HL_L].GetTemp())}");
        Console.WriteLine($"SP : {(ushort)((Registers[Register.SP_H].GetTemp() << 8) + Registers[Register.SP_L].GetTemp())}");
        Console.WriteLine($"WZ : {(ushort)((Registers[Register.WZ_H].GetTemp() << 8) + Registers[Register.WZ_L].GetTemp())}");
        Console.WriteLine($"FLAGS : S={(flags >> 7) & 1} Z={(flags >> 6) & 1} AC={(flags >> 4) & 1} P={(flags >> 2) & 1} CY={(flags >> 0) & 1}");
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