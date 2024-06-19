using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class OptionMenu : MonoBehaviour
{
    
    public void SetVolume (float volume) {
        // need to be done
    }
    public void SetResolution (float resolution) {
        // need to be done
    }
    public void SetQuality (int qualityIndex) {
        QualitySettings.SetQualityLevel(qualityIndex);
    }
    

}
