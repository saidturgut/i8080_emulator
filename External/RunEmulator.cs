namespace i8080_emulator.External;

internal static class RunEmulator
{
    private static readonly CPU 
        Cpu = new CPU();

    private static void Main() => Cpu.PowerOn();
}