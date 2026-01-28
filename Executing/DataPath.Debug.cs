namespace i8080_emulator.Executing;
using Signaling;

public partial class DataPath
{
    private void DebugInit()
    {
        Registers[(byte)Register.HL_L].Set(0x20);
        Registers[(byte)Register.A].Set(0xAA);
        Registers[(byte)Register.B].Set(0xAA);
        Registers[(byte)Register.PSW].Set(0x1);
    }
    
    public void Debug()
    {
        ushort flags = Registers[(byte)Register.PSW].Get();
        Console.WriteLine($"IR: {Hex(DebugAccess(Register.IR))}");

        Console.WriteLine($"PC: {Hex(Merge(DebugAccess(Register.PC_L), DebugAccess(Register.PC_H)))}");
        Console.WriteLine($"SP: {Hex(Merge(DebugAccess(Register.SP_L), DebugAccess(Register.SP_H)))}");
        Console.WriteLine($"HL: {Hex(Merge(DebugAccess(Register.HL_L), DebugAccess(Register.HL_H)))}");
        Console.WriteLine($"WZ: {Hex(Merge(DebugAccess(Register.WZ_L), DebugAccess(Register.WZ_H)))}");
        
        Console.WriteLine($"A: {Hex(DebugAccess(Register.A))}");
        Console.WriteLine($"B: {Hex(DebugAccess(Register.B))}");
        Console.WriteLine($"C: {Hex(DebugAccess(Register.C))}");
        Console.WriteLine($"D: {Hex(DebugAccess(Register.D))}");
        Console.WriteLine($"E: {Hex(DebugAccess(Register.E))}");
        Console.WriteLine($"TMP: {Hex(DebugAccess(Register.TMP))}");
        
        Console.WriteLine($"S Z A P C");
        Console.WriteLine($"{(flags >> 7) & 1} {(flags >> 6) & 1} {(flags >> 4) & 1} {(flags >> 2) & 1} {(flags >> 0) & 1}");
        
        Console.WriteLine("---------------");
        
    }

    private byte DebugAccess(Register register) 
        => Registers[(byte)register].Get();
    
    private ushort Merge(byte low, byte high)
        => (ushort)(low + (high << 8));

    private string Hex(ushort input)         
        => $"0x{Convert.ToString(input, 16).ToUpper()}";
}