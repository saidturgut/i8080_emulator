namespace i8080_emulator.External;

internal static class RunEmulator
{
    private static readonly CentralProcessUnit 
        CentralProcessUnit = new CentralProcessUnit();

    private static void Main() => CentralProcessUnit.PowerOn();
}