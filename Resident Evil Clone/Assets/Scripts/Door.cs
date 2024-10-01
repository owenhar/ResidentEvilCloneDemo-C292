using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Door : MonoBehaviour
{
    private Animator animator;
    [SerializeField] GameObject lightBulb;
    private AudioSource audioSource;
    // Start is called before the first frame update
    void Start()
    {
        animator = gameObject.GetComponent<Animator>();
        audioSource = gameObject.GetComponent<AudioSource>();
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.gameObject.tag == "Player")
        {
            animator.SetTrigger("OpenDoor");
        }
    }

    public void TurnLightGreen()
    {
        lightBulb.GetComponent<Light>().color = Color.green;
    }

    public void TurnLightRed()
    {
        lightBulb.GetComponent<Light>().color = Color.red;
    }

    private void DoorMoved()
    {
        audioSource.PlayOneShot(audioSource.clip);
    }

    private void OnTriggerExit(Collider other)
    {
        if (other.gameObject.tag == "Player")
        {
            animator.SetTrigger("CloseDoor");
        }
    }
}
