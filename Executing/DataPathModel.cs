namespace i8080_emulator.Executing;
using Signaling;

public class DataPathModel
{
    public readonly Dictionary<R, Register> Registers = new()
    {
        { R.PC_H, new Register() }, { R.PC_L, new Register() }, // PROGRAM COUNTER
        { R.IR, new Register() }, // INSTRUCTION REGISTER
        { R.SP_H, new Register() }, { R.SP_L, new Register() }, // STACK POINTER
        { R.A, new Register() }, // ACCUMULATOR
        { R.B, new Register() }, { R.C, new Register() }, // B & C PAIR
        { R.D, new Register() }, { R.E, new Register() }, // D & E PAIR
        { R.TMP, new Register() }, // TEMP REGISTER
        { R.H, new Register() }, { R.L, new Register() }, // ADDRESS POINTER
    };
 
    protected Dictionary<DataDriver, Register> DataDrivers = new();
    protected Dictionary<DataLatcher, Register> DataLatchers = new();
    protected Dictionary<SideEffect, RegisterPair> RegisterPairs = new();
    
    public virtual void Init()
    {
        DataDrivers = new()
        {
            { DataDriver.TMP , Registers[R.TMP] },
            { DataDriver.A , Registers[R.A] },
            { DataDriver.B , Registers[R.B] }, { DataDriver.C , Registers[R.C] },
            { DataDriver.D , Registers[R.D] }, { DataDriver.E , Registers[R.E] },
            { DataDriver.H , Registers[R.H] }, { DataDriver.L , Registers[R.L] },
        };
        
        DataLatchers = new()
        {
            { DataLatcher.TMP , Registers[R.TMP] }, { DataLatcher.IR , Registers[R.IR] },
            { DataLatcher.A , Registers[R.A] },
            { DataLatcher.B , Registers[R.B] }, { DataLatcher.C , Registers[R.C] },
            { DataLatcher.D , Registers[R.D] }, { DataLatcher.E , Registers[R.E] },
            { DataLatcher.H , Registers[R.H] }, { DataLatcher.L , Registers[R.L] },
        };
        
        RegisterPairs = new()
        {
            { SideEffect.PC_INC, new RegisterPair { Pair = [Registers[R.PC_L], Registers[R.PC_H]] } },
            { SideEffect.BC_INC, new RegisterPair { Pair = [Registers[R.C], Registers[R.B]] } }, { SideEffect.BC_DCR, new RegisterPair { Pair = [Registers[R.C], Registers[R.B]], Decrement = true} },
            { SideEffect.DE_INC, new RegisterPair { Pair = [Registers[R.E], Registers[R.D]] } }, { SideEffect.DE_DCR, new RegisterPair { Pair = [Registers[R.E], Registers[R.D]], Decrement = true } },
            { SideEffect.HL_INC, new RegisterPair { Pair = [Registers[R.L], Registers[R.H]] } }, { SideEffect.HL_DCR, new RegisterPair { Pair = [Registers[R.L], Registers[R.H]], Decrement = true } },
            { SideEffect.SP_INC, new RegisterPair { Pair = [Registers[R.SP_L], Registers[R.SP_H]] } }, { SideEffect.SP_DCR, new RegisterPair { Pair = [Registers[R.SP_L], Registers[R.SP_H]], Decrement = true } },
        };

    }
}

public struct Register
{
    private byte Value;

    public void Set(byte input)
    {
        Value = input;
    }

    public byte Get() => Value;
};

public enum R
{
    PC_H, PC_L, IR, SP_H, SP_L,
    A, B, C, D, E, 
    TMP, H, L,
    FLAGS,
}

public struct RegisterPair()
{
    public Register[] Pair = [];
    public bool Decrement = false;
}
