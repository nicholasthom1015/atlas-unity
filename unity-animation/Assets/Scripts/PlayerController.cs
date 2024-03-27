using System.Collections;
//using System.Collections.Generic;
using UnityEngine.SceneManagement;
using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class Player : MonoBehaviour
//public class CursorLockExample : MonoBehaviour
{
    public float speed = 10f; //Controls velocity multiplier
    public Vector2 turn;
    public float sensitivity = .5f;

    private Animator animator;
    private Rigidbody rb;

    // Start is called before the first frame update
    void Start()
    {
        animator = GetComponentInChildren<Animator>();
        rb = GetComponent<Rigidbody>();
        Cursor.lockState = CursorLockMode.Locked;
    }

    // Update is called once per frame
    void Update()
    {
        float xMove = Input.GetAxisRaw("Horizontal"); // d key changes value to 1, a key changes value to -1
        float zMove = Input.GetAxisRaw("Vertical"); // w key changes value to 1, s key changes value to -1
        float yMove = Input.GetAxisRaw("Jump");

        Vector3 move = new Vector3(xMove, yMove, zMove);
        move *= speed * Time.deltaTime;
        transform.Translate(move);

        turn.x += Input.GetAxis("Mouse X") * sensitivity;
        transform.localRotation = Quaternion.Euler(0, turn.x, 0);

        if (move.magnitude != 0)
        {
            animator.SetBool("IsMoving", true);
        }
        else
        {
            animator.SetBool("IsMoving", false);
        }
    }

        

    void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Death Zone"))
        {
            transform.position = new Vector3(0, 3, 1);
        }
    }
}
