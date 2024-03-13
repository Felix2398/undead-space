using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ProjectileController : MonoBehaviour
{
    [SerializeField] private LayerMask collisionMask;
    [SerializeField] GameObject bulletHolePrefab;
    private float speed;
    private Vector3 direction;
    private float damage;
    private float range;
    private int enemyPenetrationCount;

    // Start is called before the first frame update
    void Start()
    {
        Destroy(gameObject, range); // Zerstört das Bullet nach einer bestimmten Zeit
    }

    // Update is called once per frame
    void Update()
    {
        transform.Translate(direction * speed * Time.deltaTime);
    }

    public void SetDamage(float damage)
    {
        this.damage = damage;
    }

    public void SetDirection(Vector3 direction)
    {
        this.direction = direction;
    }

    public void SetSpeed(float speed)
    {
        this.speed = speed;
    }

    public void SetRange(float range)
    {
        this.range = range;
    }

    public void SetEnemyPenetrationCount(int enemyPenetrationCount)
    {
        this.enemyPenetrationCount = enemyPenetrationCount;
    }

    private void OnCollisionEnter(Collision other) 
    {
        ContactPoint contact = other.contacts[0];
        Vector3 collisionPoint = contact.point;

        if (other.gameObject.layer.Equals(9))
        {
            Destroy(gameObject);
        }
        else if (other.gameObject.CompareTag("Enemy Hurtbox"))
        {
            other.gameObject.GetComponent<EnemyHurtBox>().DealDamage(damage);
            enemyPenetrationCount--;
            if (enemyPenetrationCount <= 0)
            {
                Destroy(gameObject);
            }
        }
    } 
}
