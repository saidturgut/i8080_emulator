namespace i8080_emulator.Decoding;
using Signaling;

public partial class DecoderMultiplexer : DecoderModel
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

    protected Decoded INX_DCX(byte opcode)
    {
        Decoded decoded = new() { SideEffect = 
            IncrementOpcodes[((opcode & 0x30) >> 4) + ((opcode & 0x8) >> 3) * 4]
            
        };
        decoded.Table.Add(MachineCycle.INX_DCX);
        return decoded;
    }
    
    protected Decoded FamilyFXD(MachineCycle machineCycle)
    {
        Decoded decoded = new Decoded();

        if (machineCycle != MachineCycle.NONE) 
            decoded.Table.Add(machineCycle);
        
        return decoded;
    }
}