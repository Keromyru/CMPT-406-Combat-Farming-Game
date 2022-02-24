//TDK443
public class EnvironmentAudioController : AudioController
{
    public override  float getVolume(){
        return GameSound.Environment.getVolume();
    }
}
