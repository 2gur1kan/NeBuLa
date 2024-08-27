using UnityEngine;
using UnityEngine.UI;

public class sesAyari : MonoBehaviour
{
    public void setAudio(float value)
    {
        AudioListener.volume = value;
    }
}