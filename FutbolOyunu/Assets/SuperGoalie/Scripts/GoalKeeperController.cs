using System.Collections;
using System.Collections.Generic;
using UnityEngine;

//Kalecnini zýplamasý saga sola gýtmesý ýký kalecý kontrolu falan var burda
public class GoalKeeperController : MonoBehaviour
{

    public float speed;
    public float horizontal1;
    public float horizontal2;
    private float minRange = -3.20f;
    private float maxRange = 3.20f;

    public float jump=0.01f;

    private Rigidbody rb;

    private bool jumpingStatus = false;

    public bool kaleciController;

    void Start()
    {
        speed = 10f;
        rb = GetComponent<Rigidbody>();
        kaleciController = true;
    }


    void Update()
    {
        GoalKeeperMovement();
    }

    // Kaleci'nin hareketi
    private void GoalKeeperMovement()
    {
        horizontal1 = Input.GetAxis("Horizontal2");
        horizontal2 = Input.GetAxis("Horizontal1");

        if (kaleciController)
        {
            if (Input.GetButtonDown("Jump1") && !jumpingStatus)
            {
                rb.velocity = new Vector3(rb.velocity.x, Mathf.Sqrt(0.05f * jump * Mathf.Abs(Physics.gravity.y)), rb.velocity.z);
                jumpingStatus = true;
            }
            transform.Translate(Vector3.left * speed * horizontal1 * Time.deltaTime);
        }

        else
        {
            if (Input.GetButtonDown("Jump2") && !jumpingStatus)
            {
                rb.velocity = new Vector3(rb.velocity.x, Mathf.Sqrt(0.05f * jump * Mathf.Abs(Physics.gravity.y)), rb.velocity.z);
                jumpingStatus = true;
            }
            transform.Translate(Vector3.left * speed * horizontal2 * Time.deltaTime);
        }

     

        GoalKeeperArea();

    }

    // Burda Kalecinin gidiceði mesafeyi belirliyorum belli mesafeye kadar gidebilsin die
    private void GoalKeeperArea()
    {
        Vector3 temporaryPosition = transform.position;
        temporaryPosition.x = Mathf.Clamp(transform.position.x, minRange, maxRange);

        transform.position = temporaryPosition;
        // en sola ve en saða gidince karakter kafayý yýyebýlýyor onu engellemek ýcýn yazdým bunu
        transform.rotation = Quaternion.Euler(0, 180, 0);
    }

    void OnCollisionEnter(Collision collision)
    {
        jumpingStatus = false;
    }



}
