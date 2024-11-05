using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Pla : MonoBehaviour
{

    [SerializeField] private float movementSpeed;
    [SerializeField] private bool isAI;
    [SerializeField] private bool PlayerTwo;
    [SerializeField] private GameObject ball;

    private Rigidbody2D rb; 
    private Vector2 playerMove;
    public KeyCode upKey;
    public KeyCode downKey;

    // Start is called before the first frame update
    void Start()
    {
        rb = GetComponent<Rigidbody2D>();
    }

    // Update is called once per frame
    void Update()
    {
        if (isAI)
        {
            AIControl();
        }
        
        else if (PlayerTwo)
        {
            PlayerTwoControl();
        }

        else
        {
            playerControl();
        }
    }

    private void playerControl()
    {
        //playerMove = new Vector2(0, Input.GetAxisRaw("Vertical"));
        float Move1 = Input.GetAxisRaw("Vertical");

        Vector2 move = new Vector2(0, Move1);
        move *= movementSpeed * Time.deltaTime;
        transform.Translate(move);

    }

    private void AIControl()
    {
        if(ball.transform.position.y > transform.position.y + (0.5f))
        {
             playerMove = new Vector2(0, 1);
        }
        else if(ball.transform.position.y < transform.position.y - (0.5f))
        {
            playerMove = new Vector2(0, -1);
        }
        else
        {
            playerMove = new Vector2(0, 0);
        }
    }

    private void PlayerTwoControl()
    {
        float Move2 = Input.GetAxisRaw("Horizontal");

        Vector2 move = new Vector2(0, Move2);
        move *= movementSpeed * Time.deltaTime;
        transform.Translate(move);
    }

    void FixedUpdate()
    {
        rb.velocity = playerMove * movementSpeed;
    }    
}
