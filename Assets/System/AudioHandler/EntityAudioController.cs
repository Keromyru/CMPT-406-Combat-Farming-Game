//TDK443
public class EntityAudioController : AudioController
{
    public override  float getVolume(){
        return GameSound.Entity.getVolume();
    }
}
