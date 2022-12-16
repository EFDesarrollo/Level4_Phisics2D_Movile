using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyMovment : MonoBehaviour
{
    private Rigidbody2D rb;
    private Animator animator;
    private Camera cam;
    private TimeManager timeManager;
    private GameManager gameManager;
    [Header("Stats")]
    [SerializeField] private float Health = 10;
    [SerializeField] private float points = 100;
    [SerializeField] private float speed;
    [Header("Game Feel")]
    [SerializeField] private GameObject damageParticle;
    [SerializeField] private float damageCameraImpact = 0.8f;
    [SerializeField] private GameObject deathParticle;
    [SerializeField] private float deathCameraInpact = 0.15f;
    public GameObject GemParticle;
    [Header("Movment Logic")]
    [SerializeField] private float detectDistance;
    [SerializeField] private LayerMask actionLayerMask;
    [SerializeField] private string playerWeaponObjectTag;
    // Start is called before the first frame update
    void Start()
    {
        rb = GetComponent<Rigidbody2D>();
        animator = GetComponent<Animator>();
        cam = Camera.main;
        timeManager = Camera.main.GetComponent<TimeManager>();
        gameManager = GameObject.FindGameObjectsWithTag("GameController")[0].GetComponent<GameManager>();
    }

    // Update is called once per frame
    void Update()
    {
        rb.velocity = new Vector2 (-speed*transform.right.x, rb.velocity.y);
        animator.SetBool("isMoving", true);

        RaycastHit2D groundInfo = Physics2D.Raycast(transform.position, -transform.right, detectDistance, actionLayerMask);

        if (groundInfo)
        {
            Flip();
        }
    }
    public void Flip()
    {
        transform.eulerAngles = new Vector3(0,transform.eulerAngles.y + 180, 0);
    }
    private void OnCollisionEnter2D(Collision2D collision)
    {
        Debug.Log("Enemy Collision detected: " + collision.collider.tag);
        if (collision.collider.tag == playerWeaponObjectTag)
        {
            //Debug.Log("Enemy Collision substract health");
            Instantiate(damageParticle, transform.position, Quaternion.Euler(90, 0, 0));
            cam.GetComponent<CameraShake>().Trauma = damageCameraImpact;
            timeManager.DoSlowMotion();
            Health -= 10;
            if (Health <= 0)
            {
                Instantiate(deathParticle,transform.position, Quaternion.Euler(90,0,0));
                GemParticle.SetActive(true);
                timeManager.DoSlowMotion();
                cam.GetComponent<CameraShake>().Trauma = deathCameraInpact;
                gameManager.PlayerPoints += points;
                Destroy(gameObject);
            }
        }
    }

    private void OnDrawGizmos()
    {
        Gizmos.color = Color.red;

        Gizmos.DrawLine(transform.position, transform.position + -transform.right * detectDistance);
    }
}
