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
    
    private Dictionary<RP, Register[]> RegisterPairs = new();
    protected Dictionary<SideEffect, IncrementPair> PairIncrements = new();
    
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
            
            { DataLatcher.SP_H , Registers[R.SP_H] }, { DataLatcher.SP_L , Registers[R.SP_L] },
        };
        
        RegisterPairs = new()
        {
            { RP.PC, [Registers[R.PC_L], Registers[R.PC_H]] },
            { RP.BC, [Registers[R.C], Registers[R.B]] },
            { RP.DE, [Registers[R.E], Registers[R.D]] },
            { RP.HL, [Registers[R.L], Registers[R.H]] }, 
            { RP.SP, [Registers[R.SP_L], Registers[R.SP_H]] },
        };
        
        PairIncrements = new()
        {
            { SideEffect.PC_INC, new IncrementPair { Pair = RegisterPairs[RP.PC] } },
            { SideEffect.BC_INC, new IncrementPair { Pair = RegisterPairs[RP.BC] } }, { SideEffect.BC_DCR, new IncrementPair { Pair = RegisterPairs[RP.BC], Decrement = true} },
            { SideEffect.DE_INC, new IncrementPair { Pair = RegisterPairs[RP.DE] } }, { SideEffect.DE_DCR, new IncrementPair { Pair = RegisterPairs[RP.DE], Decrement = true } },
            { SideEffect.HL_INC, new IncrementPair { Pair = RegisterPairs[RP.HL] } }, { SideEffect.HL_DCR, new IncrementPair { Pair = RegisterPairs[RP.HL], Decrement = true } },
            { SideEffect.SP_INC, new IncrementPair { Pair = RegisterPairs[RP.SP] } }, { SideEffect.SP_DCR, new IncrementPair { Pair = RegisterPairs[RP.SP], Decrement = true } },
        };
    }
}

public class Register
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
}

public enum RP
{
    NONE,
    PC, SP, BC, DE, HL
}

public class IncrementPair
{
    public Register[] Pair = [];
    public bool Decrement = false;
}
