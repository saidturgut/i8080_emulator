namespace i8080_emulator.Decoding.Multiplexer;
using Executing.Computing;
using Signaling.Cycles;
using Signaling;
using Executing;

public partial class DecoderMultiplexer
{
    private bool CHECK_FLAGS(byte opcode, byte flags) => opcode switch
    {
        0b000 => (flags & (byte)ALUFlag.Zero) == 0,
        0b001 => (flags & (byte)ALUFlag.Zero) != 0,
        0b010 => (flags & (byte)ALUFlag.Carry) == 0,
        0b011 => (flags & (byte)ALUFlag.Carry) != 0,
        0b100 => (flags & (byte)ALUFlag.Parity) == 0,
        0b101 => (flags & (byte)ALUFlag.Parity) != 0,
        0b110 => (flags & (byte)ALUFlag.Sign) == 0,
        0b111 => (flags & (byte)ALUFlag.Sign) != 0,
        _ => false
    };

    protected Decoded CONDITIONALS(byte opcode, byte flags)
    {
        Decoded decoded = new Decoded();
        
        if (!CHECK_FLAGS(BB_XXX_BBB(opcode), flags))
        {
            decoded.Cycles.Add(MachineCycle.EMPTY);
            return decoded;
        }

        switch (BB_BBB_XXX(opcode))
        {
            case 0b100: return CALL();
            case 0b010: return JMP();
            case 0b000: return RET();
        }
        throw new InvalidOperationException();
    }
}