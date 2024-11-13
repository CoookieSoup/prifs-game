using UnityEngine;

public class Melee : MonoBehaviour
{
    public float attackDelay = 0;
    public float attackDuration = 0.2f;
    public GameObject hitBoxObject;
    
    private Animator animator;
    private EdgeCollider2D hitbox;

    public AudioClip swordSwingSound;
    private void Start()
    {
        //animator = GetComponent<Animator>();
        hitbox = hitBoxObject.GetComponent<EdgeCollider2D>();
    }

    private void Update()
    {
        if (Input.GetKeyDown(KeyCode.E)) // Attack on "E" key press.
        {
            //animator.SetTrigger("MeleeAttack");
            Invoke("ActivateHitbox", attackDelay);
            Invoke("DeactivateHitbox", attackDelay + attackDuration);
        }
    }

    void ActivateHitbox()
    {
        hitbox.gameObject.SetActive(true);
        Audio.Play(swordSwingSound);
    }

    void DeactivateHitbox()
    {
        hitbox.gameObject.SetActive(false);
    }
}