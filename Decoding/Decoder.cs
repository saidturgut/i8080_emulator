using i8080_emulator.Executing;
using i8080_emulator.Executing.Components;

namespace i8080_emulator.Decoding;
using Signaling.Multiplexer;
using Multiplexer;

public class Decoder : DecoderMux
{
    public Decoded Decode(byte ir, Psw psw)
    {
        opcode = ir;
        
        if (FixedOpcodes.ContainsKey(opcode))
            return FIXED();
        
        switch (opcode >> 6)
        {
            case 0x0:
            {
                switch (opcode)
                {
                    case 0x22: return LHLD_SHLD(false);
                    case 0x2A: return LHLD_SHLD(true);
                    case 0x32: return LDA_STA(false);
                    case 0x3A: return LDA_STA(true);
                }
                switch (zz_zzx_xxx())
                {
                    case 0x1: return LXI();
                    case 0x2: return LDAX_STAX(false);
                    case 0xA: return LDAX_STAX(true);
                    case 0x3: return INX_DCX(true);
                    case 0xB: return INX_DCX(false);
                    case 0x9: return DAD();
                }
                switch (zz_zzz_xxx())
                {
                    case 0x4: return INR_DCR(true);
                    case 0x5: return INR_DCR(false);
                    case 0x6: return MVI();
                    case 0x7: return BIT(zz_xxx_zzz());
                }
                break;
            }
            case 0x1: return MOV();
            case 0x2: return ALU(true, zz_xxx_zzz());
            case 0x3:
            {
                switch (opcode)
                {
                    case 0xC3: return JMP(Cft.JMP);
                    case 0xCD: return PUSH(Cft.CALL);
                    case 0xC9: return POP(Cft.RET);
                    case 0xDB: return IO(IoAction.INPUT);
                    case 0xD3: return IO(IoAction.OUTPUT);
                    case 0xE3: return XTHL();
                    case 0xE9: return PCHL();
                    case 0xEB: return XCHG();
                    case 0xF9: return SPHL();
                }
                switch (zz_zzx_xxx())
                {
                    case 0x1: return POP(Cft.POP);
                    case 0x5: return PUSH(Cft.PUSH);
                }
                switch (zz_zzz_xxx())
                {
                    case 0x2: return COND(psw, JMP, Cft.JMP); 
                    case 0x4: return COND(psw, PUSH, Cft.CALL);
                    case 0x0: return COND(psw, POP, Cft.RET);
                    case 0x6: return ALU(false, zz_xxx_zzz());
                    case 0x7: 
                        //Console.WriteLine($"RST: {zz_xxx_zzz() << 3}");
                        return PUSH(Cft.RST);
                }
                break;
            }
        }

        throw new Exception($"ILLEGAL OPCODE \"{opcode}\"");
    }

    private static Decoded COND(Psw psw, Func<Cft, Decoded> method, Cft type) =>
        psw.Condition(zz_xxx_zzz()) ? method(type) : IDLE();
}