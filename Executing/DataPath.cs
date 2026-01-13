namespace i8080_emulator.Executing;
using Computing;
using Signaling;
using External;

public partial class DataPath : DataPathROM
{
    private readonly IO IO = new ();
    private readonly RAM RAM = new ();
    private readonly ROM ROM = new ();
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
        ROM.Boot(RAM);
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
        {
            register.Commit();
        }        
        
        if(signals.SideEffect == SideEffect.HALT)
        {
            HALT = true;
        }    
    }

    private bool Permit() 
        => signals.SideEffect != SideEffect.HALT && 
           !PcOverriders.ContainsKey(signals.SideEffect);
    
    public void Debug()
    {
        byte flags = Registers[Register.FLAGS].GetTemp();
        Console.WriteLine($"PROGRAM COUNTER : {Convert.ToString((Registers[Register.PC_H].GetTemp() << 8) + Registers[Register.PC_L].GetTemp(), 16).ToUpper()}");
        Console.WriteLine($"IR : {Convert.ToString(IR.Get(), 16).ToUpper()}");
        Console.WriteLine($"TMP : {Convert.ToString(Registers[Register.TMP].GetTemp(), 16).ToUpper()}");
        Console.WriteLine($"B : {Convert.ToString(Registers[Register.B].GetTemp(), 16).ToUpper()}");
        Console.WriteLine($"C : {Convert.ToString(Registers[Register.C].GetTemp(), 16).ToUpper()}");
        Console.WriteLine($"D : {Convert.ToString(Registers[Register.D].GetTemp(), 16).ToUpper()}");
        Console.WriteLine($"E : {Convert.ToString(Registers[Register.E].GetTemp(), 16).ToUpper()}");
        Console.WriteLine($"A : {Convert.ToString(Registers[Register.A].GetTemp(), 16).ToUpper()}");
        Console.WriteLine($"A : {Convert.ToString(Registers[Register.B].GetTemp(), 16).ToUpper()}");
        Console.WriteLine($"HL : {Convert.ToString((ushort)((Registers[Register.HL_H].GetTemp() << 8) + Registers[Register.HL_L].GetTemp()), 16).ToUpper()}");
        Console.WriteLine($"SP : {Convert.ToString((ushort)((Registers[Register.SP_H].GetTemp() << 8) + Registers[Register.SP_L].GetTemp()), 16).ToUpper()}");
        Console.WriteLine($"WZ : {Convert.ToString((ushort)((Registers[Register.WZ_H].GetTemp() << 8) + Registers[Register.WZ_L].GetTemp()), 16).ToUpper()}");
        Console.WriteLine($"FLAGS : S={(flags >> 7) & 1} Z={(flags >> 6) & 1} AC={(flags >> 4) & 1} P={(flags >> 2) & 1} CY={(flags >> 0) & 1}");
    }

    public void MemoryDump()
    {
        foreach (var slot in RAM.MemoryDump)
        {
            Console.WriteLine("\n" + $"MEMORY[{slot.Key}] : {slot.Value}");
        }
    }
}