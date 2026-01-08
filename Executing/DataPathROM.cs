namespace i8080_emulator.Executing;
using Signaling;

public class DataPathROM
{
    protected readonly Dictionary<Register, RegisterObject> Registers = new()
    {
        { Register.PC_L, new RegisterObject() }, { Register.PC_H, new RegisterObject() }, // PROGRAM COUNTER
        { Register.IR, new RegisterObject() }, // INSTRUCTION REGISTER
        { Register.TMP, new RegisterObject() }, // TEMP DATA REGISTER
        { Register.A, new RegisterObject() }, // ACCUMULATOR
        { Register.B, new RegisterObject() }, { Register.C, new RegisterObject() }, // B & C PAIR
        { Register.D, new RegisterObject() }, { Register.E, new RegisterObject() }, // D & E PAIR
        { Register.HL_L, new RegisterObject() }, { Register.HL_H, new RegisterObject() }, // ABS ADDRESS REGISTER
        { Register.SP_L, new RegisterObject() }, { Register.SP_H, new RegisterObject() }, // STACK POINTER
        { Register.WZ_L, new RegisterObject() }, { Register.WZ_H, new RegisterObject() }, // TEMP ADDRESS REGISTER
    };
    
    protected Dictionary<Register, RegisterObject[]> RegisterPairs = new();
    protected Dictionary<SideEffect, IncrementPair> PairIncrements = new();
    
    public virtual void Init()
    {
        RegisterPairs = new()
        {
            { Register.PC_L , [Registers[Register.PC_L], Registers[Register.PC_H]] },
            { Register.C , [Registers[Register.C], Registers[Register.B]]},
            { Register.E , [Registers[Register.E], Registers[Register.D]]},
            { Register.HL_L , [Registers[Register.HL_L], Registers[Register.HL_H]]},
            { Register.SP_L , [Registers[Register.SP_L], Registers[Register.SP_H]]},
            { Register.WZ_L , [Registers[Register.WZ_L], Registers[Register.WZ_H]]},
        };
        
        PairIncrements = new()
        {
            { SideEffect.PC_INC, new IncrementPair { Pair = RegisterPairs[Register.PC_L] } },
            { SideEffect.BC_INC, new IncrementPair { Pair = RegisterPairs[Register.C] } }, { SideEffect.BC_DCR, new IncrementPair { Pair = RegisterPairs[Register.C], Decrement = true} },
            { SideEffect.DE_INC, new IncrementPair { Pair = RegisterPairs[Register.E] } }, { SideEffect.DE_DCR, new IncrementPair { Pair = RegisterPairs[Register.E], Decrement = true } },
            { SideEffect.HL_INC, new IncrementPair { Pair = RegisterPairs[Register.HL_L] } }, { SideEffect.HL_DCR, new IncrementPair { Pair = RegisterPairs[Register.HL_L], Decrement = true } },
            { SideEffect.SP_INC, new IncrementPair { Pair = RegisterPairs[Register.SP_L] } }, { SideEffect.SP_DCR, new IncrementPair { Pair = RegisterPairs[Register.SP_L], Decrement = true } },
            { SideEffect.WZ_INC, new IncrementPair { Pair = RegisterPairs[Register.WZ_L] } },
        };
    }
}

public class RegisterObject
{
    private byte value;

    public void Set(byte input) 
        => value = input;
   
    public byte Get() => value;
};

public enum Register
{
    NONE,
    PC_L, PC_H, 
    IR, RAM, TMP,
    SP_L, SP_H, 
    A, B, C, D, E, 
    HL_L, HL_H, 
    WZ_H, WZ_L,
}

public class IncrementPair
{
    public RegisterObject[] Pair = [];
    public bool Decrement = false;
}