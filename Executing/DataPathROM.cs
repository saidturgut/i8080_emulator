namespace i8080_emulator.Executing;
using Signaling;

public class DataPathROM
{
    protected readonly Dictionary<Register, ClockedRegister> Registers = new()
    {
        { Register.PC_L, new ClockedRegister(Register.PC_L) }, { Register.PC_H, new ClockedRegister(Register.PC_H) }, // PROGRAM COUNTER
        { Register.TMP, new ClockedRegister(Register.TMP) }, // TEMP DATA REGISTER
        { Register.A, new ClockedRegister(Register.A) }, // ACCUMULATOR
        { Register.B, new ClockedRegister(Register.B) }, { Register.C, new ClockedRegister(Register.C) }, // B & C PAIR
        { Register.D, new ClockedRegister(Register.D) }, { Register.E, new ClockedRegister(Register.E) }, // D & E PAIR
        { Register.HL_L, new ClockedRegister(Register.HL_L) }, { Register.HL_H, new ClockedRegister(Register.HL_H) }, // ABS ADDRESS REGISTER
        { Register.SP_L, new ClockedRegister(Register.SP_L) }, { Register.SP_H, new ClockedRegister(Register.SP_H) }, // STACK POINTER
        { Register.WZ_L, new ClockedRegister(Register.WZ_L) }, { Register.WZ_H, new ClockedRegister(Register.WZ_H) }, // TEMP ADDRESS REGISTER
    };
    
    protected Dictionary<Register, ClockedRegister[]> RegisterPairs = new();
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

public class IncrementPair
{
    public ClockedRegister[] Pair = [];
    public bool Decrement = false;
}