namespace i8080_emulator.Decoding.Multiplexer;
using Signaling.Cycles;
using Signaling;
using Executing;

public partial class DecoderMultiplexer
{
    // 01
    protected Decoded FamilyMOV(byte opcode)
    {
        Decoded decoded = new Decoded
        {            
            AddressDriver = Register.HL_L,
            DataDriver = EncodedRegisters[BB_BBB_XXX(opcode)],
            DataLatcher = EncodedRegisters[BB_XXX_BBB(opcode)],
            SideEffect = SideEffect.NONE,
        };

        if (decoded.DataLatcher == Register.RAM)
        {
            decoded.Cycles.Add(MachineCycle.RAM_WRITE_EXE);
        }
        else
        {
            decoded.Cycles.Add(decoded.DataDriver == Register.RAM
                ? MachineCycle.RAM_READ_EXE
                : MachineCycle.INTERNAL_LATCH);
        }
        
        return decoded; // 01 110 110 (0x76) is already HLT
    }
}