using UnityEngine;

public class AudioController : MonoBehaviour
{
    private AudioSource audioSource;
    public InputReader inputReader;

    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        audioSource = GetComponent<AudioSource>();
        audioSource.Play();
    }

    // Update is called once per frame
    void Update()
    {
        if(inputReader.MovementValue==Vector2.zero)
        {
            audioSource.Pause();
        }
        else
        {
            audioSource.UnPause();
        }

    }
}
