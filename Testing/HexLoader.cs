namespace i8080_emulator.Testing;
using System.Globalization;

// NOT MY WORK
public static class HexLoader
{
    public static void Load(string[] lines, byte[] ram)
    {
        foreach (var rawLine in lines)
        {
            var raw = rawLine.TrimEnd('\r', '\n');

            if (string.IsNullOrWhiteSpace(raw))
                continue;

            if (raw[0] == '\x1A')
                break;

            if (raw[0] != ':')
                throw new Exception("INVALID HEX RECORD");

            int len  = ParseByte(raw, 1);
            int addr = ParseWord(raw, 3);
            int type = ParseByte(raw, 7);
            
            if (type == 0x00)
            {
                for (int i = 0; i < len; i++)
                {
                    byte value = ParseByte(raw, 9 + i * 2);
                    ram[addr + i] = value;
                }
            }
            else if (type == 0x01)
            {
                break;
            }
            else if (type == 0x04)
            {
                throw new Exception("EXTENDED LINEAR ADDRESSES NOT SUPPORTED ON 8080");
            }
        }
    }

    private static byte ParseByte(string s, int index)
        => byte.Parse(s.AsSpan(index, 2), NumberStyles.HexNumber);

    private static int ParseWord(string s, int index)
        => int.Parse(s.AsSpan(index, 4), NumberStyles.HexNumber);
}