namespace intel8080.Executing;
using Firmware;
using Signaling;

public partial class DataPath
{
    private readonly Tty Tty = new();
    private readonly Disk Disk = new();
    
    private void Input()
    {
        switch (Reg(Register.TMP).Get())
        {
            case 2: Reg(Register.A).Set(Bios.CONST()); break;
            case 3: Reg(Register.A).Set(Bios.CONIN()); break;
            case 7: Reg(Register.A).Set(0x1); break; // READER
            case 9: Pair(Register.HL_L, Bios.SELDSK()); break;
            case 13: Reg(Register.A).Set(Bios.READ()); break;
            case 15: Reg(Register.A).Set(0x0); break; // LISTST
            case 16: Pair(Register.HL_L, Bios.SECTRAN()); break;
            default: throw new Exception($"INVALID INPUT PORT \"{Reg(Register.TMP).Get()}\"");
        }
    }

    private void Output()
    {
        switch (Reg(Register.TMP).Get())
        {
            case 0: Bios.BOOT(); break;
            case 1: Bios.WBOOT(); break;
            case 4: Bios.CONOUT(); break;
            case 5: break; // LIST
            case 6: break; // PUNCH
            case 8: break; // HOME
            case 10: Bios.SETTRK(); break;
            case 11: Bios.SETSEC(); break;
            case 12: Bios.SETDMA(); break;
            case 14: Bios.WRITE(); break;
            default: throw new Exception($"INVALID OUTPUT PORT \"{Reg(Register.TMP).Get()}\"");
        }
    }
    
    private void Pair(Register register, ushort value)
    {
        Registers[(byte)register].Set((byte)(value & 0xFF));
        Registers[(byte)(register + 1)].Set((byte)(value >> 8));
    }
}