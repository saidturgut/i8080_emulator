namespace i8080_emulator.Decoding;
using Signaling;

public class DecoderFamilies : DecoderModel
{
    // 00
    protected Decoded FamilyIMM(byte opcode)
    {
        Decoded decoded = new Decoded
        {
            DataLatcher = DataLatchers[BB_XXX_BBB(opcode)],
            DataDriver = DataDriver.NONE,
        };
        
        decoded.Table.Add(MachineCycle.RAM_READ_IMM);
        
        if (decoded.DataLatcher == DataLatcher.RAM)
            decoded.Table.Add(MachineCycle.RAM_WRITE);

        return decoded;
    }

    // 01
    protected Decoded FamilyREG(byte opcode)
    {
        Decoded decoded = new Decoded
        {            
            DataDriver = DataDrivers[BB_BBB_XXX(opcode)],
            DataLatcher = DataLatchers[BB_XXX_BBB(opcode)],
        };
        
        if (decoded.DataDriver != DataDriver.RAM && decoded.DataLatcher != DataLatcher.RAM)
        {
            decoded.Table.Add(MachineCycle.EXECUTE_MOV);
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
    protected Decoded FamilyALU(byte opcode, bool imm)
    {
        Decoded decoded = new Decoded
        {
            DataDriver = DataDrivers[BB_BBB_XXX(opcode)]
        };

        if (BB_XXX_BBB(opcode) != 0b111) // CHECK CMP
            decoded.DataLatcher = DataLatcher.A;

        if (decoded.DataDriver == DataDriver.RAM)
        {
            decoded.Table.Add(imm ? 
                MachineCycle.RAM_READ_IMM : 
                MachineCycle.RAM_READ);
        }

        decoded.Table.Add(MachineCycle.EXECUTE_ALU);

        decoded.AluOperation.Operation = ALUOperations[BB_XXX_BBB(opcode)];
        
        return decoded;
    }
    
    protected Decoded FamilySYS(MachineCycle machineCycle)
    {
        Decoded decoded = new Decoded();

        if (machineCycle != MachineCycle.NONE) 
            decoded.Table.Add(machineCycle);
        
        return decoded;
    }
}