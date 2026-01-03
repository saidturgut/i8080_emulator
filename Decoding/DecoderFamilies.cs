using i8080_emulator.Executing.Computing;

namespace i8080_emulator.Decoding;
using Signaling;

public class DecoderFamilies : DecoderRegisters
{
    // 11
    protected Decoded GroupSYS(MachineCycle machineCycle)
    {
        Decoded decoded = new Decoded();

        if (machineCycle != MachineCycle.NONE) 
            decoded.Table.Add(machineCycle);
        
        return decoded;
    }

    // 00
    protected Decoded GroupIMM(byte opcode)
    {
        // NEXT MEMORY BYTE IS THE IMMEDIATE_LOW
        
        Decoded decoded = new Decoded
        {
            DataLatcher = GetLatcher(BB_XXX_BBB(opcode)),
            DataDriver = DataDriver.NONE,
        };
        
        decoded.Table.Add(MachineCycle.RAM_READ_IMM);
        
        if (decoded.DataLatcher == DataLatcher.RAM)
            decoded.Table.Add(MachineCycle.RAM_WRITE);

        return decoded;
    }

    // 01
    protected Decoded GroupREG(byte opcode)
    {
        Decoded decoded = new Decoded
        {            
            DataLatcher = GetLatcher(BB_XXX_BBB(opcode)),
            DataDriver = GetDriver(BB_BBB_XXX(opcode)),
        };
        
        if (decoded.DataDriver != DataDriver.RAM && decoded.DataLatcher != DataLatcher.RAM)
        {
            decoded.Table.Add(MachineCycle.EXECUTE_ALU);
        }
        else
        {
            if (decoded.DataDriver == DataDriver.RAM)
                decoded.Table.Add(MachineCycle.RAM_READ);
        
            if(decoded.DataLatcher == DataLatcher.RAM)
                decoded.Table.Add(MachineCycle.RAM_WRITE);
        }
        
        return decoded; // 01 110 110 (0x76) is already HLT
    }

    // 10
    protected Decoded GroupALU(byte opcode, bool imm)
    {
        Decoded decoded = new Decoded();

        decoded.DataDriver = GetDriver(BB_BBB_XXX(opcode));
        decoded.DataLatcher = DataLatcher.A;

        if (decoded.DataDriver == DataDriver.RAM)
        {
            decoded.Table.Add(imm ? 
                MachineCycle.RAM_READ_IMM : 
                MachineCycle.RAM_READ);
        }

        decoded.Table.Add(MachineCycle.EXECUTE_ALU);
        
        switch (BB_XXX_BBB(opcode))
        {
            case 0b000://ADD
                decoded.AluOperation.Operation = Operation.ADD;
                break;  
            case 0b001://ADC
                decoded.AluOperation.Operation = Operation.ADD;
                break;
            case 0b010://SUB
                decoded.AluOperation.Operation = Operation.SUB;
                break;
            case 0b011://SBB
                decoded.AluOperation.Operation = Operation.SBB;
                break;
            case 0b100://ANA
                decoded.AluOperation.Operation = Operation.AND;
                break;
            case 0b101://XRA
                decoded.AluOperation.Operation = Operation.XOR;
                break;
            case 0b110://ORA
                decoded.AluOperation.Operation = Operation.OR;
                break;
            case 0b111://CMP
                decoded.DataLatcher = DataLatcher.NONE;
                decoded.AluOperation.Operation = Operation.SUB;
                break;
        }
        
        return decoded;
    }
}