namespace i8080_emulator.Decoding;
using Signals;

public class DecoderRegisters
{
    protected DataLatcher GetLatcher(byte encoded)
    {
        switch (encoded)
        {
            case 0b000://B
                return DataLatcher.B;
            case 0b001://C
                return DataLatcher.C;
            case 0b010://D
                return DataLatcher.D;
            case 0b011://E
                return DataLatcher.E;
            case 0b100://H
                return DataLatcher.H;
            case 0b101://L
                return DataLatcher.L;
            case 0b110://RAM
                return DataLatcher.RAM;
            case 0b111://A
                return DataLatcher.A;
        }
        
        return 0;
    }
    
    protected DataDriver GetDriver(byte encoded)
    {
        switch (encoded)
        {
            case 0b000://B
                return DataDriver.B;
            case 0b001://C
                return DataDriver.C;
            case 0b010://D
                return DataDriver.D;
            case 0b011://E
                return DataDriver.E;
            case 0b100://H
                return DataDriver.H;
            case 0b101://L
                return DataDriver.L;
            case 0b110://RAM
                return DataDriver.RAM;
            case 0b111://A
                return DataDriver.A;
        }
        
        return 0;
    }

    protected byte BB_XXX_BBB(byte opcode)
    {
        return (byte)((opcode & 0b00_111_000) >> 3);
    }

    protected byte BB_BBB_XXX(byte opcode)
    {
        return (byte)(opcode & 0b00_000_111);
    }

}