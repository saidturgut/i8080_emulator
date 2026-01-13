namespace i8080_emulator.External;

public class Terminal
{
    private readonly Queue<byte> inputBuffer = new();
    
    public byte ReadStatus()
        => (byte)(inputBuffer.Count > 0 ? 1 : 0);

    public byte ReadData()
        => inputBuffer.Dequeue();

    public void WriteData(byte value)
        => Console.Write((char)value);
    
    
    public void HostInput()
    {
        if (Console.KeyAvailable)
        {
            inputBuffer.Enqueue(
                (byte)Console.ReadKey(true).KeyChar);
            return;
        }

        inputBuffer.Enqueue(0);
    }
}