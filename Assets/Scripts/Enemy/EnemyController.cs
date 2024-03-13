using System.Collections;
using System.Collections.Generic;
using System.IO;
using UnityEngine;
using UnityEngine.AI;

public class EnemyController : MonoBehaviour
{
    // speed where the run animation looks right
    private const float DEFAULT_SPEED = 2f;

    [SerializeField] private Animator animator;
    [SerializeField] private EnemyHitBox hitBox;
    [SerializeField] private EnemyHurtBox hurtBox;
    [SerializeField] private GameObject ui;
    [SerializeField] private float damage;
    [SerializeField] private float speed;
    [SerializeField] private float attackRadius;
    [SerializeField] private EnemyState currentState;


    private Vector3 targetPosition;
    private float timer;
    private NavMeshAgent agent;
    private GameObject player;
    private bool isDead;

    [SerializeField] private float runAnimationSpeed;
    [SerializeField] private float attackAnimationSpeed;

    void Start()
    {
        animator.SetFloat("runSpeed", runAnimationSpeed);
        animator.SetFloat("attackSpeed", attackAnimationSpeed);

        agent = GetComponent<NavMeshAgent>();
        agent.speed = speed;
        player = GameObject.FindWithTag("Player");
        currentState = EnemyState.CHASING;
        hitBox.SetDamage(damage); 
    }

    void FixedUpdate()
    {
        if (isDead) return;
        targetPosition = player.transform.position;

        bool canAttack = calcDistanceToPlayer() <= attackRadius;
        if (currentState == EnemyState.CHASING && canAttack)
        {
            // start attacking
            currentState = EnemyState.ATTACKING;
            agent.destination = transform.position;
            animator.SetBool("isChasing", false);
            animator.SetBool("isAttacking", true);
        } 
        else if (currentState == EnemyState.CHASING && !canAttack)
        {
            // keep chasing
            HandleChasingState();
        }
        else if (currentState == EnemyState.ATTACKING && !canAttack)
        {
            // start chasing
            agent.speed = speed;
            agent.destination = targetPosition;
            currentState = EnemyState.CHASING;
            animator.SetBool("isChasing", true);
            animator.SetBool("isAttacking", false);
        }
        else if (currentState == EnemyState.ATTACKING && canAttack)
        {
            // keep attacking
            HandleAttackState();
        }
    }

    private void HandleChasingState()
    { 
        agent.destination = targetPosition;
    }

    private void HandleAttackState()
    {
        Vector3 direction = player.transform.position - transform.position;
        Quaternion rotation = Quaternion.LookRotation(direction);
        transform.rotation = Quaternion.Lerp(transform.rotation, rotation, Time.deltaTime * 3);
    }

    private float calcDistanceToPlayer()
    {
        return Vector3.Distance(player.transform.position, this.transform.position);
    }

    private void UpdateTargetPosition()
    {
        if (timer > 0.5) 
        {
            targetPosition = player.transform.position;
            timer = 0;
        }
        timer += Time.deltaTime;
    }

    public void Die() 
    {
        isDead = true;
        animator.SetBool("isDead", true);
        agent.enabled = false;
        hitBox.gameObject.SetActive(false);
        hurtBox.gameObject.SetActive(false);
        ui.gameObject.SetActive(false); 
    }

    public bool IsDead()
    {
        return isDead;
    }
}
