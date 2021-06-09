using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public class EnemyManager : MonoBehaviour
{

    public Transform target;
    NavMeshAgent agent;
    Animator animator;
    public Collider weaponCollider;
    public EnemyUIManager enemyUIManager;
    public GameObject gameClearText;

    public int maxHp = 100;
    int hp;


    // Start is called before the first frame update
    void Start()
    {
        hp = maxHp;
        enemyUIManager.Init(this);

        agent = GetComponent<NavMeshAgent>();
        agent.destination = target.position;
        animator = GetComponent<Animator>();
        HideColliderWeapon();
    }

    // Update is called once per frame
    void Update()
    {
        agent.destination = target.position;
        animator.SetFloat("Distance", agent.remainingDistance);
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
            animator.SetTrigger("Die");
            Destroy(gameObject, 2f);
            gameClearText.SetActive(true);
        }
        enemyUIManager.UpdateHP(hp);
    }

    private void OnTriggerEnter(Collider other)
    {
        Damager damager = other.GetComponent<Damager>();
        if (damager != null)
        {
            animator.SetTrigger("Hurt");
            Damage(damager.damage);
        }
    }

    public void LookAtTarget()
    {
        transform.LookAt(target);
    }
       
}

