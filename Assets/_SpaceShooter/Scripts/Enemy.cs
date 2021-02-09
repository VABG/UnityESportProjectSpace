using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Enemy : MonoBehaviour
{
    [SerializeField] private float speed = 10;
    [SerializeField] private float randomDistance = 5;
    [SerializeField] private float health = 10;
    [SerializeField] ParticleSystem deadPFX;

    Vector3 dir = Vector3.back;
    Rigidbody rb;

    // Start is called before the first frame update
    void Start()
    {
        rb = GetComponent<Rigidbody>();

        transform.localPosition = Vector3.right *
            UnityEngine.Random.Range(-randomDistance, randomDistance);

        rb.velocity = Vector3.back * speed;
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public void Damage(float dmg)
    {
        health -= dmg;
        if (health <= 0) Die();
    }

    private void Die()
    {
        Destroy(this.gameObject);
        // PFX
        ParticleSystem pfx = Instantiate(deadPFX.GetComponent<ParticleSystem>(), transform.position, transform.rotation);
        pfx.GetComponent<Rigidbody>().velocity = this.rb.velocity;
        float scale = Random.Range(.5f, 1.5f);
        pfx.transform.localScale = new Vector3(scale, scale, scale);
        Destroy(pfx.gameObject, pfx.main.startLifetime.constantMax);
    }

    private void OnCollisionEnter(Collision collision)
    {
        Damage(20);
    }
}
