namespace i8080_emulator.Executing;
using Computing;
using Signaling;

public partial class DataPath
{
    public void ALUControl()
    {
        if(signals.AluOperation is null)
            return;

        ClockedRegister FLAGS = Registers[Register.FLAGS];
        
        var nullable = signals.AluOperation!.Value;
        bool CarryFlag = (byte)(FLAGS.Get() & (byte)ALUFlag.Carry) != 0;
        bool AuxCarryFlag = (byte)(FLAGS.Get() & (byte)ALUFlag.AuxCarry) != 0;
        ALUOutput output;
        
        if (nullable.Opcode == ALUOpcode.DAD) { DAD(nullable); return; }
        
        if (nullable.Operation == Operation.ROT)
        {
            if (nullable.Opcode == ALUOpcode.DAA)
            {
                output = ALU.BCDFixer(Registers[nullable.A].Get(), CarryFlag, AuxCarryFlag);
                Protocol(output, output.FlagMask);
                return;
            }
            
            output = ALU.Rotate(nullable.Opcode, Registers[nullable.A].Get(), (byte)(CarryFlag ? 1 : 0));
            Protocol(output, ALUROM.FlagMasks[nullable.FlagMask]);
            return;
        }

        // STANDARD
        ALUInput input = StandardInput(nullable, CarryFlag);
        output = ALU.Compute(input);
        Protocol(output, ALUROM.FlagMasks[nullable.FlagMask]);
    }
    
    private ALUInput StandardInput(ALUOperation nullable, bool CarryFlag) => new ALUInput(
        nullable.Operation, // ALU OPERATION
        Registers[nullable.A].Get(), // A
        nullable.B != Register.NONE ? Registers[nullable.B].Get() : (byte)1, // B
        CarryFlag,
        nullable.UseCarry); // CARRY

    private void DAD(ALUOperation nullable)
    {
        ALUOutput dadOutput1 = ALU.Compute(new ALUInput
        (nullable.Operation, Registers[Register.HL_L].Get(), 
            Registers[nullable.A].Get(), false, false));
            
        Registers[Register.HL_L].Set(dadOutput1.Result);
            
        ALUOutput dadOutput2 = ALU.Compute(new ALUInput
        (nullable.Operation, Registers[Register.HL_H].Get(), 
            Registers[nullable.B].Get(), 
            (byte)(dadOutput1.Flags & (byte)ALUFlag.Carry) == 1, true));
            
        Registers[Register.HL_H].Set(dadOutput2.Result);
        UpdateFlags(ALUROM.FlagMasks[FlagMask.C], dadOutput2.Flags);
    }
    
    private void Protocol(ALUOutput output, ALUFlag flagMask)
    {
        DBUS.Set(output.Result);
        UpdateFlags(flagMask, output.Flags);
    }
    
    private void UpdateFlags(ALUFlag mask, byte newFlags)
    {
        Registers[Register.FLAGS].Set((byte)
            ((Registers[Register.FLAGS].Get() & (byte)~mask) | (newFlags & (byte)mask)));
    }
}