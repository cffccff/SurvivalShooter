using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class PlayerHealth : MonoBehaviour
{
   
    public Material redMaterial;
    private Material defaultMaterial;
    public SkinnedMeshRenderer skinnedMesh;
    // Timer to track collision time
    float timeColliding;
    // Time before damage is taken, 1 second default
    public float timeThreshold = 1f;
    private Animator animator;
    private AudioSource audioSource;
    public AudioClip audioClipHurt;
    public AudioClip audioClipDeath;
    public Slider slider;
    private int maxHealth = 5;
    private int currentHealth;
    private void Start()
    {

        animator = GetComponent<Animator>();
         defaultMaterial = skinnedMesh.material;
        audioSource = GetComponent<AudioSource>();
        currentHealth = maxHealth;
        slider.maxValue = maxHealth;
        slider.minValue = 0;
        slider.value = currentHealth;
    }
    private void OnCollisionEnter(Collision collision)
    {
        if (collision.collider.CompareTag("Enemy"))
        {
            // Reset timer
            timeColliding = 0f;
            // Take damage on impact?
            Debug.Log("Enemy started colliding with player.");
            TakeDamageFromEnemy();        
        }

    }
    private void OnCollisionStay(Collision collision)
    {
        if (collision.collider.CompareTag("Enemy"))
        {
            // If the time is below the threshold, add the delta time
            if (timeColliding < timeThreshold)
            {
                timeColliding += Time.deltaTime;
            }
            else
            {
                // Time is over theshold, player takes damage
                //  Debug.Log("Do dmg on stay");
                TakeDamageFromEnemy();
                // Reset timer
                timeColliding = 0f;
            }
        }
    }
  
    private void UpdateMaterialPlayerHurt()
    {
        skinnedMesh.material = redMaterial;
        Invoke(nameof(ReturnDefaultMaterial), 0.5f);
    }
    private void ReturnDefaultMaterial()
    {
        skinnedMesh.material = defaultMaterial;
    }
    private void UpdateSliderValue()
    {
        slider.value = currentHealth;
    }
    private void PlayAudioPlayerHurt()
    {
        audioSource.PlayOneShot(audioClipHurt);
    }
    private void CheckPlayerDeath()
    {
        if (currentHealth <= 0)
        {
            audioSource.PlayOneShot(audioClipDeath);
            GameMusic.Instance.audioMixerSnapshotDead.TransitionTo(20);
            animator.SetTrigger("Death");
            CancelInvoke();
            Destroy(gameObject.GetComponent<Rigidbody>());
            gameObject.GetComponent<PlayerController>().enabled = false;
            Invoke(nameof(DisplayGameOverUI), 2f);

        }
    }
    private void DisplayGameOverUI()
    {
        UI_Interaction.Instance.DisplayGameOverPanel();
       
    }
    private void TakeDamageFromEnemy()
    {
        PlayAudioPlayerHurt();
         currentHealth -= 1;
        UpdateSliderValue();
        UpdateMaterialPlayerHurt();
        CheckPlayerDeath();
    }
   
}
