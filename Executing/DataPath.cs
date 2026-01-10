namespace i8080_emulator.Executing;
using InputOutput;
using Computing;
using Signaling;

// ALU CONTROL, ADDRESS BUFFER, MULTIPLEXER, INCREMENTER
public partial class DataPath : DataPathROM
{
    private readonly RAM RAM = new ();
    private readonly IO IO = new ();
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
        Registers[Register.PC_H].Set(0x01);
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
        byte flags = Registers[Register.FLAGS].GetTemp();
        System.Console.WriteLine($"PROGRAM COUNTER : {(ushort)((Registers[Register.PC_H].GetTemp() << 8) + Registers[Register.PC_L].GetTemp())}");
        System.Console.WriteLine($"IR : {IR.Get()}");
        System.Console.WriteLine($"TMP : {Registers[Register.TMP].GetTemp()}");
        System.Console.WriteLine($"B : {Registers[Register.B].GetTemp()}");
        System.Console.WriteLine($"C : {Registers[Register.C].GetTemp()}");
        System.Console.WriteLine($"D : {Registers[Register.D].GetTemp()}");
        System.Console.WriteLine($"E : {Registers[Register.E].GetTemp()}");
        System.Console.WriteLine($"H : {Registers[Register.HL_H].GetTemp()}");
        System.Console.WriteLine($"L : {Registers[Register.HL_L].GetTemp()}");
        System.Console.WriteLine($"A : {Registers[Register.A].GetTemp()}");
        System.Console.WriteLine($"HL : {(ushort)((Registers[Register.HL_H].GetTemp() << 8) + Registers[Register.HL_L].GetTemp())}");
        System.Console.WriteLine($"SP : {(ushort)((Registers[Register.SP_H].GetTemp() << 8) + Registers[Register.SP_L].GetTemp())}");
        System.Console.WriteLine($"WZ : {(ushort)((Registers[Register.WZ_H].GetTemp() << 8) + Registers[Register.WZ_L].GetTemp())}");
        System.Console.WriteLine($"FLAGS : S={(flags >> 7) & 1} Z={(flags >> 6) & 1} AC={(flags >> 4) & 1} P={(flags >> 2) & 1} CY={(flags >> 0) & 1}");
    }

    public void MemoryDump()
    {
        System.Console.WriteLine();
        foreach (var slot in RAM.MemoryDump)
        {
            System.Console.WriteLine($"MEMORY[{slot.Key}] : {slot.Value}");
        }
    }
}