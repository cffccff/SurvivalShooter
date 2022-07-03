using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class PlayerController : MonoBehaviour
{
    float horizontal;
    float vertical;
   public Vector2 mousePos;
    public float moveSpeed=5.0f;
    private Rigidbody rg;
    public Camera cam;
    public float speedRotation =10f;
    private Animator animator;
    private bool isFire = false;
    public GameObject gunPosition;
    public LineRenderer lineRenderer;
    public ParticleSystem gunParticle;
    public ParticleSystem gunBarrel;
    private AudioSource audioClipShoot;
    [SerializeField] int damageShoot = 1;
    private void Start()
    {
        rg = GetComponent<Rigidbody>();
        animator = GetComponent<Animator>();
        audioClipShoot = GetComponent<AudioSource>();
        
    }
    private void Update()
    {
        PlayerRotateWithMouse();


    }
   private void PlayerRotateWithMouse()
    {
        Plane playerplane = new Plane(Vector3.up, transform.position);
        Ray ray = Camera.main.ScreenPointToRay(mousePos);
        if (playerplane.Raycast(ray, out float hitdist))
        {
            Vector3 targetpoint = ray.GetPoint(hitdist);
            Quaternion targetrotation = Quaternion.LookRotation(targetpoint - transform.position);
            transform.rotation = Quaternion.Slerp(transform.rotation, targetrotation, speedRotation * Time.deltaTime);
        }
    }
    public void OnFireInput(bool isFireContext)
    {
        this.isFire = isFireContext;
        if (isFire == true)
        {
            StartCoroutine(Shoot());
        }
    }
    IEnumerator Shoot()
    {
        if (Physics.Raycast(gunPosition.transform.position, gunPosition.transform.TransformDirection(Vector3.forward), out RaycastHit hit, Mathf.Infinity))
        {
            if (hit.collider.gameObject.CompareTag("Enemy"))
            {
                string name = hit.transform.name;
                Debug.Log("Did Hit: " + name);
                EnemyHealth gotit = hit.collider.gameObject.GetComponent<EnemyHealth>();
                gotit.TakeDamgeFromPlayer(damageShoot);


            }
            lineRenderer.SetPosition(0, gunPosition.transform.position);
            lineRenderer.SetPosition(1, hit.point);
        }
        else
        {
            lineRenderer.SetPosition(0, gunPosition.transform.position);
            lineRenderer.SetPosition(1, gunPosition.transform.TransformDirection(Vector3.forward)); 
            Debug.Log("Did not Hit");
        }
        gunParticle.Play();
        gunBarrel.Play();
        audioClipShoot.Play();
        lineRenderer.enabled = true;
        yield return new WaitForSeconds(0.09f);
        lineRenderer.enabled = false;
    }
    public void OnMoveInput(float horizontalContext,float verticalContext)
    {
        this.vertical = verticalContext;
        this.horizontal = horizontalContext;
    }
    public void OnMousePosition(Vector2 mousePosContext)
    {
        this.mousePos = mousePosContext;
    }

    private void FixedUpdate()
    {
        Vector3 moveDirection = Vector3.forward * vertical + Vector3.right * horizontal;
        rg.velocity = (moveDirection * moveSpeed);

        if (horizontal != 0 || vertical != 0)
        {
            animator.SetTrigger("Run");
            return;
        }
        if (horizontal == 0 && vertical == 0)
        {
            animator.SetTrigger("Idle");

        }
    }
   

}
