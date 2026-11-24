
using UnityEngine;
using UnityEngine.Audio;
using UnityEngine.UI;
public class Audio : MonoBehaviour
{
    // Warning: los cinco campos son [SerializeField] public. El public sobra y los deja mutables desde cualquier clase. Pasalos a [SerializeField] private.
    [SerializeField] public AudioMixer audioMixer;
    [SerializeField] public Slider volumeWorldSlider;
    [SerializeField] public Slider volumeSFXSlider;
    [SerializeField] public Slider volumeUISlider;
    [SerializeField] public Slider volumeMusicSlider;



    void Awake()
    {
        // Warning: los sliders arrancan en -30 en la escena pero el mixer no se actualiza hasta que el usuario mueve el slider, así que lo que muestra
        // la UI no coincide con el volumen real. Aplicá el valor inicial (SetGameplayVolume(volumeWorldSlider.value), etc.) en Awake o Start.
        // Además el máximo de los sliders es +20 dB, que satura la salida: dejá el máximo en 0.
        volumeWorldSlider.onValueChanged.AddListener(SetGameplayVolume);
        volumeSFXSlider.onValueChanged.AddListener(SetSFXVolume);
        volumeUISlider.onValueChanged.AddListener(SetUIVolume);
        volumeMusicSlider.onValueChanged.AddListener(SetMusicVolume);

    }


    private void OnDestroy()
    {
        volumeWorldSlider.onValueChanged.RemoveAllListeners();
        volumeSFXSlider.onValueChanged.RemoveAllListeners();
        volumeUISlider.onValueChanged.RemoveAllListeners();
        volumeMusicSlider.onValueChanged.RemoveAllListeners();
    }
    private void SetGameplayVolume(float volume)
    {
        audioMixer.SetFloat("GameplayVolume", volume);
    }
    private void SetSFXVolume(float volume)
    {
        audioMixer.SetFloat("SFXVolume", volume);

    }
    private void SetUIVolume(float volume)
    {
        audioMixer.SetFloat("UIVolume", volume);

    }
    private void SetMusicVolume(float volume)
    {
        // Suggestion: el parámetro se llama "Music" mientras los otros terminan en "Volume". Unificá el naming y guardá los nombres como const string.
        audioMixer.SetFloat("Music", volume);

    }
}
