namespace i8080_emulator.Decoding;
using Signals;

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
            decoded.InternalLatch = true;
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
    protected Decoded GroupALU(byte opcode)
    {
        Decoded decoded = new Decoded();
        
        switch (BB_XXX_BBB(opcode))
        {
            case 0b000://ADD
                break;  
            case 0b001://ADC
                break;
            case 0b010://SUB
                break;
            case 0b011://SBB
                break;
            case 0b100://ANA
                break;
            case 0b101://XRA
                break;
            case 0b110://ORA
                break;
            case 0b111://CMP
                break;
        }
        
        return decoded;
    }
}