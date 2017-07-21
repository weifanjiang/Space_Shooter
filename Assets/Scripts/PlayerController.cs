using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[System.Serializable]
public class Boundary
{
    public float xmin, xmax, zmin, zmax;
}

public class PlayerController : MonoBehaviour {

    Rigidbody rb;
    AudioSource aud;
    public float speed;
    public Boundary boundary;

    public GameObject shot;
    public Transform shotSpawn;
    public float fireRate;
    private float nextFire;
    public float tilt;

    void Start()
    {
        rb = GetComponent<Rigidbody>();
        aud = GetComponent<AudioSource>();
        Boundary boundary = new Boundary();
        nextFire = Time.time + fireRate;
    }

    void Update()
    {
        if (Input.GetButtonDown("Fire1") && Time.time > nextFire)
        {
            nextFire = Time.time + fireRate;
            Instantiate(shot, shotSpawn);
            aud.Play();
        }
    }

    void FixedUpdate()
    {
        float vertical = Input.GetAxis("Vertical");
        float horizontal = Input.GetAxis("Horizontal");

        rb.velocity = new Vector3(horizontal, 0.0f, vertical) * speed;
        rb.position = new Vector3(
            Mathf.Clamp(rb.position.x, boundary.xmin, boundary.xmax),
            0.0f,
            Mathf.Clamp(rb.position.z, boundary.zmin, boundary.zmax)
        );

        rb.rotation = Quaternion.Euler(new Vector3(0.0f, 0.0f, -1 * rb.velocity.x * tilt));
    }
 
}
