using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Laser : MonoBehaviour
{
    [SerializeField] LayerMask layerMask;
    [SerializeField] GameObject actualLaser;
    [SerializeField] ParticleSystem particles;
    private AudioSource sound;
    private float laserLength = 50;
    private float targetVolume = 0;

    // Start is called before the first frame update
    void Start()
    {
        sound = GetComponent<AudioSource>();
        sound.Play();
        sound.volume = 0;
        //sound.Pause();
    }

    // Update is called once per frame
    void Update()
    {
        sound.volume = Mathf.Lerp(sound.volume, targetVolume, Time.deltaTime * 10);
    }

    public void FireLaser()
    {
        if (!actualLaser.activeSelf) actualLaser.SetActive(true);
        RaycastHit hit;
        Ray r = new Ray(actualLaser.transform.position, Vector3.forward);
        if (Physics.Raycast(r, out hit, laserLength, layerMask))
        {
            //hit.collider är det strålen kolliderade med
            //Där man kolliderade >> hit.point
            actualLaser.transform.localScale = new Vector3(1, 1, hit.distance);

            //if (!sound.isPlaying) sound.UnPause();
            //PFX
            if(!particles.isPlaying) particles.Play();
            targetVolume = 1;

            particles.transform.position = hit.point;

            Enemy e = hit.collider.GetComponent<Enemy>();

            if (e != null)
            {
                e.Damage(50 * Time.deltaTime);
            }
        }
        else
        {
            //sound.Pause();
            targetVolume = 0;
            actualLaser.transform.localScale = new Vector3(1, 1, laserLength);
            //PFX
            particles.transform.position = transform.position + new Vector3(0,0,100);
        }
    }

    public void UnFireLaser()
    {
        if (actualLaser.activeSelf) actualLaser.SetActive(false);
    }

}
