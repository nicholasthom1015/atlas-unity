using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class BallMovement : MonoBehaviour
{
    [SerializeField] private float initialSpeed = 10;
    [SerializeField] private float speedIncrease = 0.25f;
    [SerializeField] private TMP_Text playerScore;
    [SerializeField] private TMP_Text AIScore;

    public GameObject PlayerScoreTransition;
    public GameObject OppenentScoreTransition;
     
    public GameObject PetalsPrefab;

    public Animator LeftScore;
    public Animator RightScore;

    private bool aFinished = false;
    private bool bFinished = false;

    private int hitCounter;
    private Rigidbody2D rb;

    void Start()
    {
        PlayerScoreTransition.SetActive(false);
        OppenentScoreTransition.SetActive(false);
        rb = GetComponent<Rigidbody2D>();
        Invoke("StartBall", 2f);
    }

    void FixedUpdate()
    {
        rb.velocity = Vector2.ClampMagnitude(rb.velocity, initialSpeed + (speedIncrease * hitCounter));
    }

    private void StartBall()
    {
        rb.velocity = new Vector2(-1, -1) * (initialSpeed + speedIncrease * hitCounter);
    }

    private void Resetball()
    {
        rb.velocity = new Vector2(0, 0);
        transform.position = new Vector3(13, 8, 10);
        hitCounter = 0;
        Invoke("StartBall", 2f);
    }

    private void PlayerBounce(Transform myObject)
    {
        hitCounter++;

        Vector2 ballPos = transform.position;
        Vector2 playerPos = myObject.position;

        float xDirection, yDirection;

        if(transform.position.x > 0)
        {
            xDirection = -1;
        }
        else
        {
            xDirection = 1;
        }

        yDirection = (ballPos.y - playerPos.y) / myObject.GetComponent<Collider2D>().bounds.size.y;

        if(yDirection == 0)
        {
            yDirection = 0.25f;
        }
        rb.velocity = new Vector2(xDirection, yDirection) * (initialSpeed + speedIncrease * hitCounter);
    }

    private void OnCollisionEnter2D(Collision2D collision)
    {
        if(collision.gameObject.name == "Player One" || collision.gameObject.name == "Player Two" || collision.gameObject.name == "Bottom")
        {
            GameObject Petals = Instantiate(PetalsPrefab, collision.contacts[0].point, Quaternion.identity);
            Destroy(Petals, Petals.GetComponent<ParticleSystem>().main.duration);
            PlayerBounce(collision.transform);
        }
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.CompareTag("RightGoal"))
        {
            PlayerScoreTransition.SetActive(true);
            Invoke("Resetball", 0.2f);
            Invoke("CloseDoor1", 1f);
            playerScore.text = (int.Parse(playerScore.text) + 1).ToString();
        }

        else if (collision.CompareTag("LeftGoal"))
        {
            OppenentScoreTransition.SetActive(true);
            Invoke("Resetball", 0.2f);
            Invoke("CloseDoor2", 1f);
            AIScore.text = (int.Parse(AIScore.text) + 1).ToString();
        }
    }

    void OpenDoor1()
    {
         if(LeftScore.GetCurrentAnimatorStateInfo(0).normalizedTime >= 1 && !LeftScore.IsInTransition(0))
         {
             if(aFinished = true)
             {
                 CloseDoor1();
             }
         }
     }

     void OpenDoor2()
     {
         if(RightScore.GetCurrentAnimatorStateInfo(0).normalizedTime >= 1 && !RightScore.IsInTransition(0))
         {
             if(bFinished = true)
             {
                 CloseDoor2();
             }
         }
     }

     private void CloseDoor1()
     {
         PlayerScoreTransition.SetActive(false);
     }

     private void CloseDoor2()
     {
        OppenentScoreTransition.SetActive(false);
     }
}
