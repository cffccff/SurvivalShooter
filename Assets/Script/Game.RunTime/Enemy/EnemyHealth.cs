using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.AI;
using MarchingBytes;
public class EnemyHealth : MonoBehaviour
{
    private int enemyHealth;
    private Animator animator;
    private AudioSource audioSource;
    public ParticleSystem hitParticleSystem;
    [Header("Clip from Hellephant")]
    public AudioClip audioClipHurtHellephant;
    public AudioClip audioClipDeathHellephant;

    [Header("Clip from Zombear")]
    public AudioClip audioClipHurtBear;
    public AudioClip audioClipDeathBear;

    [Header("Clip from zombunny")]
    public AudioClip audioClipHurtBunny;
    public AudioClip audioClipDeathBunny;
    //  public Transform enemyTransform;
    // Start is called before the first frame update
    void Start()
    {
        animator = GetComponent<Animator>();
        audioSource = GetComponent<AudioSource>();
        enemyHealth = 10;
       // score = GameObject.FindGameObjectWithTag("Score").GetComponent<TextMeshProUGUI>();

    }
    public void TakeDamgeFromPlayer(int damage)
    {
        if (gameObject.name.StartsWith("Hellephant"))
        {
            audioSource.PlayOneShot(audioClipHurtHellephant);
        }
        else if (gameObject.name.StartsWith("Zombear"))
        {
            audioSource.PlayOneShot(audioClipHurtBear);
        }
        else
        {
            audioSource.PlayOneShot(audioClipHurtBunny);
        }
        hitParticleSystem.Play();
        enemyHealth -= damage;
       
        Debug.Log("Health left:" + enemyHealth);
        if (enemyHealth <= 0)
        {
            UI_Interaction.Instance.enemyKilled++;
           // score.text = "Enemy killed: "+ UI_Interaction.Instance.enemyKilled;
            DestroyEnemy();
        }
    }
    private void DestroyEnemy()
    {
       
        if (gameObject.name.StartsWith("Hellephant"))
        {
            audioSource.PlayOneShot(audioClipDeathHellephant);
        }
        else if (gameObject.name.StartsWith("Zombear"))
        {
            audioSource.PlayOneShot(audioClipDeathBear);
        }
        else
        {
            audioSource.PlayOneShot(audioClipDeathBunny);
        }
        
        animator.SetTrigger("Death");
        gameObject.GetComponent<EnemyNavMesh>().enabled = false;
        gameObject.GetComponent<NavMeshAgent>().enabled = false;
        gameObject.GetComponent<BoxCollider>().enabled = false;
        enemyHealth = 10;
        Invoke(nameof(DisableEnemy), 1.6f);
        
    }
    private void DisableEnemy()
    {
        gameObject.SetActive(false);
    }
    
}
