namespace i8080_emulator.Decoding.Multiplexer;
using Executing.Computing;
using Signaling.Cycles;
using Executing;
using Signaling;

public partial class DecoderMultiplexer
{    
    // 10
    protected Decoded FamilyALU(byte opcode, bool isNative, bool imm)
    {
        Decoded decoded;
        if (isNative)
        {
            var resolved = ResolveOpcode(opcode, imm);
            
            Console.WriteLine(resolved.driver.ToString() + resolved.operation.ToString()  + resolved.aluOpcode.ToString() );
            decoded = ALU(resolved.driver, resolved.operation, resolved.aluOpcode);
        }
        else
            decoded = INR_DCR(opcode);
        
        if (imm)
            decoded.Cycles.Add(MachineCycle.RAM_READ_IMM);
        else
        {
            decoded.Cycles.Add(decoded.DataDriver == Register.RAM ? 
                MachineCycle.RAM_READ_TMP : 
                MachineCycle.TMP_LATCH);
        }
        
        decoded.Cycles.Add(MachineCycle.ALU_EXECUTE);

        var nullable = decoded.AluOperation!.Value;
        
        nullable.UseCarry = CarryUsers.Contains(nullable.Opcode);

        if (nullable.Opcode == ALUOpcode.CMP)
            decoded.DataLatcher = Register.NONE;

        decoded.AluOperation = nullable;
        return decoded;
    }
    
    (Register driver, Operation operation, ALUOpcode aluOpcode) ResolveOpcode(byte opcode, bool imm)
    {
        if (!imm)
        {
            return (EncodedRegisters[BB_BBB_XXX(opcode)], ALU_10.ElementAt(BB_XXX_BBB(opcode)).Value, 
                ALU_10.ElementAt(BB_XXX_BBB(opcode)).Key);
        }
        else
        {
            byte index = (byte)((opcode >> 3) & 0b00_000_111);
            return (Register.NONE, ALU_10.ElementAt(index).Value,
                ALU_10.ElementAt(index).Key);
        }
    }

    private Decoded ALU(Register driver, Operation operation, ALUOpcode aluOpcode) => new()
    {
        AddressDriver = Register.HL_L,
        DataDriver = driver, // OPERAND
        DataLatcher = Register.A, // DESTINATION
        AluOperation = new ALUOperation
        {
            Operation = operation,
            Opcode = aluOpcode,
            A = Register.A, // DESTINATION
            B = Register.TMP, // OPERAND GOES TO TMP
            FlagMask = 0,
        }
    };
    
    private Decoded INR_DCR(byte opcode) => new()
    {        
        AddressDriver = Register.HL_L,
        DataDriver = EncodedRegisters[BB_XXX_BBB(opcode)], // OPERAND AND DESTINATION 
        DataLatcher = EncodedRegisters[BB_XXX_BBB(opcode)], // IS SAME
        AluOperation = new ALUOperation
        {
            Operation = ALU_00.ElementAt((byte)(BB_BBB_XXX(opcode) - 4)).Value,
            Opcode = ALU_00.ElementAt((byte)(BB_BBB_XXX(opcode) - 4)).Key,
            A = Register.TMP, // DESTINATION, IT GOES TO REGISTER
            B = Register.NONE, // IT'S GONNA BE 1
            FlagMask = FlagMask.SZAP,
        }
    };
}