using i8080_emulator.Executing.Computing;

namespace i8080_emulator.Decoding;
using Signaling;

public class DecoderMultiplexer : DecoderModel
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
    protected Decoded Family10(byte opcode, bool isNative)
    {
        Decoded decoded = isNative ? ALU(opcode) : INR_DCR(opcode);

        decoded.Table.Add(decoded.DataDriver == DataDriver.RAM ? 
            MachineCycle.RAM_READ : 
            MachineCycle.TMP_LATCH);

        decoded.Table.Add(MachineCycle.ALU_EXECUTE);

        var nullable = decoded.AluOperation!.Value;
        
        nullable.UseCarry = CarryUsers.Contains(nullable.Opcode);

        if (nullable.Opcode == ALUOpcode.CMP)
            decoded.DataLatcher = DataLatcher.NONE;
        
        return decoded;
    }

    private Decoded ALU(byte opcode)
    {
        Decoded decoded = new Decoded
        {
            DataDriver = DataDrivers[BB_BBB_XXX(opcode)],
            DataLatcher = DataLatcher.A,
            AluOperation = new ALUOperation(),
        };
        var nullable = new ALUOperation();
        
        byte bb_xxx_bbb = BB_XXX_BBB(opcode);
        nullable.Operation = ALU_10.ElementAt(bb_xxx_bbb).Value;
        nullable.Opcode = ALU_10.ElementAt(bb_xxx_bbb).Key;
        nullable.A = DataLatcher.A;
        nullable.B = DataDriver.TMP;
        nullable.FlagMask = 0;
        
        decoded.AluOperation = nullable;
        return decoded;
    }

    private Decoded INR_DCR(byte opcode)
    {
        Decoded decoded = new Decoded
        {            
            DataDriver = DataDrivers[BB_XXX_BBB(opcode)],
            DataLatcher = DataLatchers[BB_XXX_BBB(opcode)],
            AluOperation = new ALUOperation(),
        };
        var nullable = new ALUOperation();
        
        byte bb_bbb_xxx = (byte)(BB_BBB_XXX(opcode) - 4);
        nullable.Operation = ALU_00.ElementAt(bb_bbb_xxx).Value;
        nullable.Opcode = ALU_00.ElementAt(bb_bbb_xxx).Key;
        nullable.A = DataLatcher.TMP;
        nullable.B = DataDriver.NONE;
        nullable.FlagMask = 1;

        decoded.AluOperation = nullable;
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