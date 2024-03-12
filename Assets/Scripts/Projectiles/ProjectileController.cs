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

    private void OnCollisionEnter(Collision other) 
    {
        Debug.Log("Hit something: " + other.gameObject.layer);
        ContactPoint contact = other.contacts[0];
        Vector3 collisionPoint = contact.point;

        if (other.gameObject.layer.Equals(9))
        {
            Debug.Log("hit wall");
            Destroy(gameObject);
        }
        else if (other.gameObject.CompareTag("Enemy Hurtbox"))
        {
            Debug.Log("Hit enemy");
            other.gameObject.GetComponent<EnemyHurtBox>().DealDamage(damage);
            Destroy(gameObject);
        }
    } 
}
