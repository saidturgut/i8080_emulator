namespace i8080_emulator.Signaling;

public class Sequencer
{
    public byte tState = 1;
    public byte mState = 0;
    
    public void Advance(byte cycleCount, byte tStateMax)
    {
        if (tState < tStateMax) tState++;
        else
        {
            tState = 1;
            
            if (mState == cycleCount)
                mState = 0;
            else
                mState++;
        }
    }
}