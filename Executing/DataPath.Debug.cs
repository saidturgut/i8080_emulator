namespace i8080_emulator.Executing;
using Signaling;

public partial class DataPath
{
    private void DebugInit()
    {
        Reg(Register.SP_L).Init(0xFE);
        Reg(Register.SP_H).Init(0xFF);
    }
    
    public void Debug(bool DEBUG_MODE)
    {
        Ram.DEBUG_MODE = DEBUG_MODE;
        if(!DEBUG_MODE) return;
        
        ushort flags = DebugAccess(Register.PSW);
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

        if (DebugAccess(Register.NONE) != 0)
            throw new Exception("CORRUPTED \"NONE\" REGISTER!!");
        
        Console.WriteLine("---------------");
        
    }

    private byte DebugAccess(Register register) 
        => Registers[(byte)register].Get();
    
    private ushort Merge(byte low, byte high)
        => (ushort)(low + (high << 8));

    private string Hex(ushort input)         
        => $"0x{Convert.ToString(input, 16).ToUpper()}";
}