namespace i8080_emulator.Decoding.Multiplexer;
using Signaling.Cycles;
using Signaling;
using Executing;

public partial class DecoderMultiplexer
{
    private static Decoded WZAddress()
    {
        Decoded decoded = new()
        {
            AddressDriver = Register.WZ_L,
            RegisterPairs = [Register.WZ_L, Register.WZ_H],
        };
        decoded.Cycles.Add(MachineCycle.RAM_READ_IMM_LOW);
        decoded.Cycles.Add(MachineCycle.RAM_READ_IMM_HIGH);
        return decoded;
    }
    private static Decoded PairAddress(byte opcode) => new()
    {
        AddressDriver = EncodedRegisterPairs[GetRegisterPairNormal(opcode)][0],
        DataDriver = Register.TMP,
        DataLatcher = Register.A,
    };
    
    protected Decoded LDA_STA(byte opcode, bool isX)
    {
        Decoded decoded = isX ? PairAddress(opcode) : WZAddress();
        
        if (BB_BBX_BBB(opcode) == 0)// STORE
        {
            decoded.DataDriver = Register.A;
            decoded.Cycles.Add(MachineCycle.RAM_WRITE_EXE);
        }
        else
        {
            decoded.DataLatcher = Register.A;
            decoded.Cycles.Add(MachineCycle.RAM_READ_EXE);
        }
        return decoded;
    }
    
    protected Decoded LHLD_SHLD(byte opcode)
    {
        Decoded decoded = WZAddress();
        decoded.SideEffect = SideEffect.WZ_INC;
        
        if (BB_BBX_BBB(opcode) == 0)// STORE
        {
            decoded.DataDriver = Register.HL_L;
            decoded.Cycles.Add(MachineCycle.RAM_WRITE_EXE);
            decoded.Cycles.Add(MachineCycle.RAM_WRITE_H);
        }
        else
        {
            decoded.DataLatcher = Register.HL_L;
            decoded.Cycles.Add(MachineCycle.RAM_READ_EXE);
            decoded.Cycles.Add(MachineCycle.RAM_READ_H);
        }
        
        return decoded;
    }

}