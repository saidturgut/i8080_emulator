namespace i8080_emulator.Decoding;
using Signaling;

public class DecoderFamilies : DecoderModel
{
    // 00
    protected Decoded Family00(byte opcode)
    {
        Decoded decoded = new Decoded
        {
            DataLatcher = DataLatchers[BB_XXX_BBB(opcode)],
        };
        
        decoded.Table.Add(MachineCycle.RAM_READ_IMM);

        if (decoded.DataLatcher == DataLatcher.RAM)
            decoded.Table.Add(MachineCycle.RAM_WRITE);
        else
        {
            decoded.DataDriver = DataDriver.TMP;
            decoded.Table.Add(MachineCycle.BUS_LATCH);
        }
        
        return decoded;
    }

    // 01
    protected Decoded Family01(byte opcode)
    {
        Decoded decoded = new Decoded
        {            
            DataDriver = DataDrivers[BB_BBB_XXX(opcode)],
            DataLatcher = DataLatchers[BB_XXX_BBB(opcode)],
        };

        if (decoded.DataLatcher == DataLatcher.RAM)
        {
            decoded.Table.Add(MachineCycle.TMP_LATCH);
            decoded.Table.Add(MachineCycle.RAM_WRITE);
        }
        else
        {
            if (decoded.DataDriver == DataDriver.RAM)
                decoded.Table.Add(MachineCycle.RAM_READ);
            
            decoded.Table.Add(MachineCycle.BUS_LATCH);
        }
        
        return decoded; // 01 110 110 (0x76) is already HLT
    }

    // 10
    protected Decoded Family10(byte opcode)
    {
        Decoded decoded = new Decoded
        {
            DataDriver = DataDrivers[BB_BBB_XXX(opcode)],
            DataLatcher = DataLatcher.TMP,
        };

        decoded.Table.Add(decoded.DataDriver == DataDriver.RAM ? 
            MachineCycle.RAM_READ : 
            MachineCycle.TMP_LATCH);

        decoded.Table.Add(MachineCycle.ALU_EXECUTE);

        byte bb_xxx_bbb = BB_XXX_BBB(opcode);
        decoded.AluOperation.Operation = ALUOperations[bb_xxx_bbb];
        decoded.AluOperation.CarryIn = bb_xxx_bbb == 1;
        
        return decoded;
    }
    
    protected Decoded Family11(MachineCycle machineCycle)
    {
        Decoded decoded = new Decoded();

        if (machineCycle != MachineCycle.NONE) 
            decoded.Table.Add(machineCycle);
        
        return decoded;
    }
}