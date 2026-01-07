namespace i8080_emulator.Decoding.Multiplexer;
using Executing.Computing;
using Signaling.Cycles;
using Executing;
using Signaling;

public partial class DecoderMultiplexer
{
    // 10
    protected Decoded FamilyALU(byte opcode, bool isNative)
    {
        Decoded decoded = isNative ? ALU(opcode) : INR_DCR(opcode);

        decoded.Cycles.Add(decoded.DataDriver == Register.RAM ? 
            MachineCycle.RAM_READ_TMP : 
            MachineCycle.TMP_LATCH);
        decoded.Cycles.Add(MachineCycle.ALU_EXECUTE);

        var nullable = decoded.AluOperation!.Value;
        
        nullable.UseCarry = CarryUsers.Contains(nullable.Opcode);

        if (nullable.Opcode == ALUOpcode.CMP)
            decoded.DataLatcher = Register.NONE;

        decoded.AluOperation = nullable;
        return decoded;
    }

    private Decoded ALU(byte opcode) => new()
    {
        AddressDriver = Register.HL_L,
        DataDriver = EncodedRegisters[BB_BBB_XXX(opcode)], // OPERAND
        DataLatcher = Register.A, // DESTINATION
        AluOperation = new ALUOperation
        {
            Operation = ALU_10.ElementAt(BB_XXX_BBB(opcode)).Value,
            Opcode = ALU_10.ElementAt(BB_XXX_BBB(opcode)).Key,
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

    protected Decoded DAD(byte opcode)
    {
        Decoded decoded = new()
        {
            AluOperation = new ALUOperation
            {
                Operation = Operation.ADD,
                Opcode = ALUOpcode.DAD,
                A = EncodedRegisterPairs[GetRegisterPair(opcode) - 4][0],
                B = EncodedRegisterPairs[GetRegisterPair(opcode) - 4][1],
                FlagMask = FlagMask.C
            }
        };
        decoded.Cycles.Add(MachineCycle.ALU_EXECUTE);
        return decoded;
    }

    protected Decoded ROTATE(byte opcode)
    {
        Decoded decoded = new()
        {
            AluOperation = new ALUOperation
            {
                Operation = Operation.ROT,
                Opcode = EncodedRotators[BB_BXX_BBB(opcode)],
                A = Register.A,
                FlagMask = FlagMask.C,
            },
            DataLatcher = Register.A,
        };
        decoded.Cycles.Add(MachineCycle.ALU_EXECUTE);
        return decoded;
    }
}