using UnityEngine;

public class SoundManager : MonoBehaviour
{
    [SerializeField] 
    private AudioClip jumpSound;

    [SerializeField] 
    private AudioClip DeathSound;

    [SerializeField] 
    private AudioSource audioSource;


    public void PlayJump()
    {
        audioSource.PlayOneShot(jumpSound);
    }

    public void PlayDeath()
    {
        audioSource.PlayOneShot(DeathSound);
    }
}
