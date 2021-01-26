using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class shot : MonoBehaviour
{
    public float shotSpeed = 10;
    private float movementSpeed;
    // Start is called before the first frame update
    void Start()
    {
        GetComponent<Rigidbody>().velocity = new Vector3(0, 0, shotSpeed);
    }

    // Update is called once per frame
    void Update()
    {
        //Förflyttning
        transform.position += Vector3.left * Time.deltaTime * movementSpeed;
        //Grader per sekund
        float rotationSpeed = 720;
        transform.Rotate(new Vector3(0, rotationSpeed*Time.deltaTime, 0));
    }

    private void OnCollisionEnter(Collision collision)
    {
        Destroy(gameObject);
    }
}
