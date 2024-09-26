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
    [SerializeField] Weapon currentWeapon;
    
    

    private Rigidbody rb;
    private List<IPickupable> inventory = new List<IPickupable>();


    // Start is called before the first frame update
    void Start()
    {
        rb = GetComponent<Rigidbody>();

        Cursor.lockState = CursorLockMode.Locked;
        Cursor.visible = false;

        if (currentWeapon != null)
        {
            //PlayerManager.instance.UpdateAmmo(0);
        }
        
    }

    // Update is called once per frame
    void Update()
    {
        LookAround();
        Move();
        Jump();
        Shoot();
        if (Input.GetKeyDown(KeyCode.R))
        {
            AttemptReload();
        }
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

    private void AttemptReload()
    {
        if (currentWeapon == null)
        {
            return;
        }

        Enums.MagazineType weaponType = currentWeapon.magazineType;
        Magazine mag = (Magazine) inventory.Find((x) =>
        {
            if (x is Magazine mag)
            {
                return mag.GetMagType() == weaponType;
            }
            return false;
        });
        if (mag != null)
        {
            inventory.Remove(mag);
            currentWeapon.Reload(mag);
        }

        if (currentWeapon != null)
        {
            PlayerManager.instance.UpdateAmmo(currentWeapon.CheckAmmo());
        }
    }

    private void OnCollisionEnter(Collision collision)
    {
        if (collision.gameObject.tag == "Ground")
        {
            isGrounded = true;
        }
        else if (collision.gameObject.GetComponent<IPickupable>() != null)
        {
            IPickupable pickUp = collision.gameObject.GetComponent<IPickupable>();
            inventory.Add(pickUp);
            pickUp.Pickup(this);
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
            if (currentWeapon != null)
            {
                currentWeapon.Fire();
                PlayerManager.instance.UpdateAmmo(currentWeapon.CheckAmmo());
            }
        }
    }


}
