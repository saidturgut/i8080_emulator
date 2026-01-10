namespace i8080_emulator.Executing;
using Signaling;

public partial class DataPath
{
    public byte[] GetValues() => [IR.Get(), Registers[Register.FLAGS].GetTemp()];
    
    public void MultiplexerDrive()
    {        
        if(signals.DataDriver == Register.NONE || !Permit())
            return;
        
        if (signals.DataDriver == Register.RAM)
            RAM.Read(ABUS_H, ABUS_L, DBUS);
        else
        {
            byte value = signals.DataDriver != Register.IR
                ? Registers[signals.DataDriver].Get()
                : (byte)(((IR.Get() & 0b00_111_000) >> 3) * 8);
            
            DBUS.Set(signals.SideEffect != SideEffect.CMA ? 
                value : (byte)~value);
        }
    }
    
    public void MultiplexerLatch()
    {        
        if(signals.DataLatcher == Register.NONE || !Permit())
            return;
        
        if (signals.DataLatcher == Register.IR)
        {
            IR.Set(DBUS.Get());
            return;
        }
        
        if (signals.DataLatcher == Register.RAM)
        {
            RAM.Write(ABUS_H, ABUS_L, DBUS);
            
            if (signals.SideEffect is SideEffect.XTHL or SideEffect.XTHL_SP)
                Registers[signals.DataDriver].Set(Registers[Register.TMP].Get());
        }
        else
        {
            if (signals.SideEffect == SideEffect.SWAP)
                Registers[signals.DataDriver].Set(Registers[signals.DataLatcher].Get());
            
            Registers[signals.DataLatcher].Set(DBUS.Get());
        }
    }
}