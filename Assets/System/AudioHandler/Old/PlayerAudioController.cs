//TDK443
public class PlayerAudioController : AudioController
{    
    public override  float getVolume(){
        return GameSound.Player.getVolume();
    }
}
