namespace i8080_emulator.Signals;

public class Sequencer
{    
    public byte mState = 0;
    
    public void Advance(byte cycleCount)
    {
        if (mState == cycleCount)
            mState = 0;
        else
            mState++;
    }
}