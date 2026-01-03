namespace i8080_emulator.Signals;
using Decoding;

public class SignalSets
{    
    private readonly DecoderCore _decoderCore = new DecoderCore();
    protected Decoded decoded = new Decoded();
    
    protected SignalSet Fetch()
    {
        return new SignalSet
        {
            AddressDriver =  AddressDriver.PC,
            DataDriver = DataDriver.RAM,
            DataLatcher = DataLatcher.IR,
            SideEffect = SideEffect.PC_INC,
        };
    }

    protected SignalSet Decode(byte IR)
    {
        decoded = _decoderCore.Decode(IR);
        return new SignalSet();
    }

    protected SignalSet RamRead()
    {
        return new SignalSet
        {
            AddressDriver = AddressDriver.HL,
            DataDriver = DataDriver.RAM,
            DataLatcher = decoded.DataLatcher,
        };
    }

    protected SignalSet RamReadImm()
    {
        return new SignalSet
        {
            AddressDriver = AddressDriver.PC,
            DataDriver = DataDriver.RAM,
            DataLatcher = decoded.DataLatcher,
            SideEffect = SideEffect.PC_INC,
        };
    }
    
    protected SignalSet RamWrite()
    {
        return new SignalSet
        {
            AddressDriver = AddressDriver.HL,
            DataDriver = decoded.DataDriver,
            DataLatcher = DataLatcher.RAM,
        };
    }
}