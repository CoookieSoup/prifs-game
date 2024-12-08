using UnityEngine;

public class Melee : MonoBehaviour
{
    public float attackDelay = 0;
    public float attackDuration = 0.2f;
    public GameObject hitBoxObject;
    
    private Animator animator;
    private EdgeCollider2D hitbox;

    public AudioClip swordSwingSound;

    public Player_script playerScript;

    public bool hide_crossbow = false;
    private void Start()
    {
        //animator = GetComponent<Animator>();
        hitbox = hitBoxObject.GetComponent<EdgeCollider2D>();
        playerScript = FindObjectOfType<Player_script>();
    }

    private void Update()
    {
        if (Input.GetKeyDown(KeyCode.E) && !playerScript.isDead) // Attack on "E" key press.
        {
            //animator.SetTrigger("MeleeAttack");
            Invoke("ActivateHitbox", attackDelay);
            Invoke("DeactivateHitbox", attackDelay + attackDuration);
        }
    }

    void ActivateHitbox()
    {
        hide_crossbow = true;
        hitbox.gameObject.SetActive(true);
        Audio.Play(swordSwingSound);
    }

    void DeactivateHitbox()
    {
        hide_crossbow = false;
        hitbox.gameObject.SetActive(false);
    }
}