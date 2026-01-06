namespace i8080_emulator.Decoding.Multiplexer;
using Signaling.Cycles;
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
            decoded.Table.Add(MachineCycle.INTERNAL_LATCH);
        }
        
        return decoded;
    }

    protected Decoded INX_DCX(byte opcode)
    {
        Decoded decoded = new() { SideEffect = 
            IncrementOpcodes[GetRegisterPair(opcode)], };
        
        decoded.Table.Add(MachineCycle.INX_DCX);
        return decoded;
    }

    protected Decoded LXI(byte opcode)
    {
        Decoded decoded = new() { RegisterPair = 
            RegisterPairs[GetRegisterPair(opcode)] };
        
        decoded.Table.Add(MachineCycle.LXI_LOW);
        decoded.Table.Add(MachineCycle.LXI_HIGH);
        
        return decoded;
    }
}