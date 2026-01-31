namespace i8080_emulator;

public class Tty
{
    private readonly Queue<byte> inputBuffer = new();
    
    public void HostInput()
    {
        while (Console.KeyAvailable)
        {
            inputBuffer.Enqueue((byte)Console.ReadKey(intercept: true).KeyChar);
        }
    }

    public byte ReadStatus()
    {
        return inputBuffer.Count > 0 ? (byte)0xFF : (byte)0x00;
    }

    public byte ReadData()
    {
        return inputBuffer.Dequeue();;
    }

    public void WriteData(byte data)
    {
        Console.Write((char)data);
    }
}