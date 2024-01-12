using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Bullet : MonoBehaviour
{
    public GameObject hitEffect;
    Vector3 lastVelocity;
    int bounceCount = 3;
    Rigidbody2D rb;

    private void Start()
    {
        rb = GetComponent<Rigidbody2D>();
    }
    private void Update()
    {
        lastVelocity = rb.velocity;
    }
    private void OnCollisionEnter2D(Collision2D collision)
    {
        GameManager.instance.player.GetComponent<PlayerMovement>().hitEffect.Play();
        if (collision.collider.tag == "Enemy")
        {
            collision.collider.enabled = false;
            collision.gameObject.GetComponent<Enemy>().EnemyDeath();
        }
        else if(collision.collider.tag == "Player")
        {
            GameManager.PlayerDeathEvent?.Invoke();
        }
        else if(bounceCount>0)
        {
            var speed = lastVelocity.magnitude;
            var direction = Vector3.Reflect(lastVelocity.normalized, collision.contacts[0].normal);

            rb.velocity = direction * 30;//30 from shooting script
            bounceCount--;
            return;
        }
        GameObject go = Instantiate(hitEffect, transform.position, Quaternion.identity);
        Destroy(go, 1f); 
        Destroy(gameObject);
    }
}
