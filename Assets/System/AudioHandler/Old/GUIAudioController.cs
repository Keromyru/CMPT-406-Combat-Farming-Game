//TDK443
public class GUIAudioController : AudioController
{
        public override  float getVolume(){
        return GameSound.GUI.getVolume();
    }
}
