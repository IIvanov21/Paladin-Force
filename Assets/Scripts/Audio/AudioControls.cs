using TMPro;
using UnityEngine;
using UnityEngine.Audio;
using UnityEngine.UI;

public class AudioControls : MonoBehaviour
{
    public AudioMixer audioMixer;

    public Slider masterSlider, playerSlider;

    public TMP_Text masterText, playerText;

    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        masterSlider.onValueChanged.AddListener(SetMasterVolume);
        playerSlider.onValueChanged.AddListener(SetPlayerVolume);   


    }

    void SetMasterVolume(float value)
    {
        audioMixer.SetFloat("MasterVolume",value);
        ConvertDBToPercentage(value, masterText);
    }

    void SetPlayerVolume(float value)
    {
        audioMixer.SetFloat("PlayerVolume",value );
        ConvertDBToPercentage(value, playerText);
    }

    void ConvertDBToPercentage(float db, TMP_Text text)
    {
        float percentage = (db + 80)/80f * 100;
        text.text = percentage.ToString("F0");


    }
}
