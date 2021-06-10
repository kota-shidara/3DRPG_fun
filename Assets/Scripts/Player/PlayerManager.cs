using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerManager : MonoBehaviour
{
    float x;
    float z;
    public float moveSpeed;

    Rigidbody rb;
    Animator animator;
    public Collider weaponCollider;
    public PlayerUIManager playerUIManager;
    public GameObject gameOverText;
    public Transform target;

    public int maxHp = 100;
    int hp;
    public int maxStamina = 100;
    int stamina;
    bool isDie;

    // Start is called before the first frame update
    void Start()
    {
        hp = maxHp;
        stamina = maxStamina;
        playerUIManager.Init(this);

        rb = GetComponent<Rigidbody>();
        animator = GetComponent<Animator>();
        HideColliderWeapon();
    }

    // Update is called once per frame
    void Update()
    {
        if (isDie)
        {
            return;
        }

        x = Input.GetAxisRaw("Horizontal");
        z = Input.GetAxisRaw("Vertical");

        IncreaseStamina();

        if (Input.GetKeyDown(KeyCode.Space))
        {
            if (stamina >= 20)
            {
                stamina -= 20;
                playerUIManager.UpdateStamina(stamina);
                Attack();
            }
        }

    }

    void IncreaseStamina()
    {
        stamina ++;
        if (stamina >= maxStamina)
        {
            stamina = maxStamina;
        }
        playerUIManager.UpdateStamina(stamina);
    }

    void Attack()
    {
        float distance = Vector3.Distance(transform.position, target.position);
        if (distance <= 2f)
        {
            transform.LookAt(target);
        }
        animator.SetTrigger("Attack");
    }


    private void FixedUpdate()
    {
        if (isDie)
        {
            rb.velocity = new Vector3(0, 0, 0);
            return;
        }

        Vector3 direction = transform.position + new Vector3(x, 0, z);
        transform.LookAt(direction);
        rb.velocity = new Vector3(x, 0, z) * moveSpeed;
        animator.SetFloat("Speed", rb.velocity.magnitude);
    }

    public void ShowColliderWeapon()
    {
        weaponCollider.enabled = true;
    }

    public void HideColliderWeapon()
    {
        weaponCollider.enabled = false;
    }

    void Damage(int damage)
    {
        hp -= damage;
        if (hp <= 0)
        {
            hp = 0;
            isDie = true;
            animator.SetTrigger("Die");
            gameOverText.SetActive(true);
        }

        playerUIManager.UpdateHP(hp);
    }

    private void OnTriggerEnter(Collider other)
    {

        if (isDie)
        {
            return;
        }

        Damager damager = other.GetComponent<Damager>();
        if (damager != null)
        {
            animator.SetTrigger("Hurt");
            Damage(damager.damage);
        }
    }
}
