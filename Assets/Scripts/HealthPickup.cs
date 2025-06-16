using UnityEngine;

public class HealthPickup : MonoBehaviour
{
    public AudioSource audioSource;
    private void OnTriggerEnter(Collider other)
    {
        if(other.CompareTag("Player"))
        {
            audioSource.Play();
            Debug.Log("Player healed!");
            gameObject.GetComponent<MeshRenderer>().enabled = false;    
            Invoke(nameof(DestroyObject), 5f);
        }
    }

    private void Update()
    {
        transform.Rotate(new Vector3(0.0f, 5.0f, 2.0f));
    }

    void DestroyObject()
    {
        Destroy(gameObject);

    }
}
