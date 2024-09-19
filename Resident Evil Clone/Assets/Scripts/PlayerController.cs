using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerController : MonoBehaviour
{
    [SerializeField] float moveSpeed;
    [SerializeField] float jumpForce;
    [SerializeField] float mouseSensitivitiy;
    [SerializeField] float verticalLookLimit;
    [SerializeField] private Transform firePoint;

    private bool isGrounded = true;
    private float xRotation = 0;

    [SerializeField] Transform fpsCamera;
    private Rigidbody rb;


    // Start is called before the first frame update
    void Start()
    {
        rb = GetComponent<Rigidbody>();

        Cursor.lockState = CursorLockMode.Locked;
        Cursor.visible = false;

        
    }

    // Update is called once per frame
    void Update()
    {
        LookAround();
        Move();
        Jump();
        Shoot();
    }
    

    void LookAround() // Mainly concerned with mouse movement
    {
        float mouseX = Input.GetAxis("Mouse X") * mouseSensitivitiy * Time.deltaTime;
        float mouseY = Input.GetAxis("Mouse Y") * mouseSensitivitiy * Time.deltaTime;

        transform.Rotate(Vector3.up * mouseX);

        xRotation -= mouseY;
        xRotation = Mathf.Clamp(xRotation, -verticalLookLimit, verticalLookLimit);
        fpsCamera.localRotation = Quaternion.Euler(xRotation, 0, 0);
    }

    void Move()
    {
        float moveX = Input.GetAxis("Horizontal");
        float moveZ = Input.GetAxis("Vertical");

        Vector3 move = transform.right * moveX + transform.forward * moveZ;
        move.Normalize();
        Vector3 moveVelocity = move * moveSpeed;

        moveVelocity.y = rb.velocity.y;

        rb.velocity = moveVelocity;

    }

    void Jump()
    {
        if (Input.GetButtonDown("Jump") && isGrounded)
        {
            rb.AddForce(Vector3.up * jumpForce, ForceMode.Impulse);
            //isGrounded = false;
        }

    }

    private void OnCollisionEnter(Collision collision)
    {
        if (collision.gameObject.tag == "Ground")
        {
            isGrounded = true;
        }
    }

    private void OnCollisionExit(Collision collision)
    {
        if (collision.gameObject.tag == "Ground")
        {
            isGrounded = false;
        }
    }

    private void Shoot()
    {
        if (Input.GetMouseButtonDown(0))
        {
            RaycastHit hit;
            if (Physics.Raycast(firePoint.position, firePoint.forward, out hit, 100) && hit.transform)
            {
                Debug.DrawRay(firePoint.position, firePoint.forward * hit.distance, Color.red, 2f);
                hit.transform.GetComponent<ZombieController>().TakeDamage(1);
            }
        }
    }


}
