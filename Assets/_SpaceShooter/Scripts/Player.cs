using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Player : MonoBehaviour
{
    [SerializeField] float speed = 5;
    [SerializeField] int lives = 3;
    [SerializeField] GameObject shot;
    [SerializeField] Transform barrel;
    [SerializeField] Transform playerTransform;
    [SerializeField] ParticleSystem deathPFX;
    private AudioSource audioSource;

    private Rigidbody rb;
    private int powerUpLevel = 0;

    [SerializeField] List<Laser> laser;
    //Förhindrar vår raycast från att kollidera med saker vi inte vill
    [SerializeField] LayerMask layerMask;

    // Start is called before the first frame update
    void Start()
    {
        rb = GetComponent<Rigidbody>();
        audioSource = GetComponent<AudioSource>();
    }

    // Update is called once per frame
    void Update()
    {
        if (Input.GetKey(KeyCode.A))
        {
            //rb.velocity = new Vector3(-speed, 0,0);
            rb.velocity -= new Vector3(speed * Time.deltaTime, 0, 0);
        }
        else if (Input.GetKey(KeyCode.D))
        {
            //rb.velocity = new Vector3(speed, 0, 0);
            rb.velocity += new Vector3(speed * Time.deltaTime, 0, 0);
        }
        else
        {
            //rb.velocity = Vector3.zero;
        }

        if (Input.GetKeyDown(KeyCode.Space))
        {
            Instantiate(shot, barrel.position, Quaternion.identity);
            audioSource.Play();
        }

        if (lives <= 0)
        {
            Destroy(gameObject);
            //PFX
            ParticleSystem pfx = Instantiate(deathPFX.GetComponent<ParticleSystem>(), transform.position, transform.rotation);
            pfx.GetComponent<Rigidbody>().velocity = this.rb.velocity;
            float scale = Random.Range(.5f, 1.5f);
            pfx.transform.localScale = new Vector3(scale, scale, scale);
            Destroy(pfx.gameObject, pfx.main.startLifetime.constantMax);
        }

        RollShip(rb.velocity.x, 2.1f);
        FireLaser();
    }

    private void RollShip(float velocityX, float strength)
    {
        playerTransform.rotation = Quaternion.Euler(0, 0, -velocityX*strength); 
    }

    public void PowerUp()
    {
        powerUpLevel++;
        if(powerUpLevel > laser.Count)
        {
            powerUpLevel = laser.Count;
        }
    }

    private void FireLaser()
    {
        for (int i = 0; i < powerUpLevel; i++)
        {
            laser[i].FireLaser();
        }
    }

    private void OnCollisionEnter(Collision collision)
    {
        lives--;
    }
}