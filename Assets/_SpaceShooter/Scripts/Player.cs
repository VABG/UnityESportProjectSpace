using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Player : MonoBehaviour
{
    [SerializeField] float speed = 5;
    [SerializeField] int lives = 3;
    [SerializeField] GameObject shot;
    [SerializeField] Transform barrel;
    private Rigidbody rb;

    [SerializeField] GameObject laser;
    //Förhindrar vår raycast från att kollidera med saker vi inte vill
    [SerializeField] LayerMask layerMask;

    // Start is called before the first frame update
    void Start()
    {
        rb = GetComponent<Rigidbody>();
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
        }

        if (lives <= 0)
        {
            Destroy(gameObject);
        }

        RollShip(rb.velocity.x, 2.1f);
        FireLaser();
    }

    private void RollShip(float velocityX, float strength)
    {
        transform.rotation = Quaternion.Euler(0, 0, -velocityX*strength); 
    }

    private void FireLaser()
    {
        if (!laser.activeSelf) laser.SetActive(true);
        RaycastHit hit;
        Ray r = new Ray(barrel.position, Vector3.forward);
        if (Physics.Raycast(r, out hit, 100.0f, layerMask))
        {
            //hit.collider är det strålen kolliderade med
            //Där man kolliderade >> hit.point
            laser.transform.localScale = new Vector3(1, 1, hit.distance);
            Destroy(hit.collider.gameObject);
        }
        else
        {
            laser.transform.localScale = new Vector3(1, 1, 100);
        }
    }

    private void OnCollisionEnter(Collision collision)
    {
        lives--;
    }

}