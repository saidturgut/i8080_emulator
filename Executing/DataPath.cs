namespace i8080_emulator.Executing;
using Signaling;
using Components;

public partial class DataPath
{
    private readonly Ram Ram = new ();
    
    private readonly TriStateBus Dbus = new ();
    private readonly TriStateBus AbusL = new ();
    private readonly TriStateBus AbusH = new (); 

    private SignalSet signals = new ();

    public void Init()
    {
        Ram.Init();
        
        for (int i = 0; i < Registers.Length; i++)
            Registers[i] = new ClockedRegister();
        
        Reg(Register.PSW).Set(0x2);
        
        DebugInit();
    }
    
    public void Clear()
    {
        Dbus.Clear();
        AbusL.Clear();
        AbusH.Clear();
    }

    public void Receive(SignalSet input) =>
        signals = input;

    public byte GetIr()
        => Registers[(byte)Register.IR].Get();
    
    private ClockedRegister Reg(Register register)
        => Registers[(byte)register];
    
    public void Commit()
    {
        foreach (ClockedRegister register in Registers)
            register.Commit();
    }
}
