namespace i8080_emulator.Executing;
using Computing;
using Signals;

public partial class DataPath
{
    private readonly RandomAccessMemory RAM = new RandomAccessMemory();
    private readonly ALUOperator ALUOperator = new ALUOperator();
    
    private readonly Bus DBUS = new Bus(); // DATA BUS 
    private readonly Bus ABUS_H = new Bus(); 
    private readonly Bus ABUS_L = new Bus(); 
    
    private byte PC_H, PC_L; // PROGRAM COUNTER
    private byte SP_H, SP_L; // STACK POINTER
    
    public byte IR; // INSTRUCTION REGISTER
    
    private byte A, // ACCUMULATOR
        B, C, D, E, // GENERAL REGISTERS
        H, L; // MEMORY ADDRESS REGISTERS
    
    private SignalSet signals = new SignalSet();
    
    public void Init()
    {
        H = 0x12;
        L = 0x34;
        
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

        Console.WriteLine($"PROGRAM COUNTER : {PC_H} + {PC_L}");
        Console.WriteLine($"IR : {IR}");
        Console.WriteLine($"A : {A}");
        Console.WriteLine($"HL : {(ushort)((H << 8) + L)}");
    }

    public void OperateALU()
    {
        ALUOperator.OperateALU(signals);
    }
}