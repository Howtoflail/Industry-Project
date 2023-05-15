using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MailGamePlayer : MonoBehaviour
{
    // Start is called before the first frame update
    public MailGameObstacles obstacles;
    [SerializeField]
    private float gravity;
    [SerializeField]
    private float jumpForce;

    private Rigidbody2D rb;

    private MailGameHandler handler;
    void Start()
    {
        handler = GameObject.Find("MailGame").GetComponent<MailGameHandler>();
        rb = GetComponent<Rigidbody2D>();
        obstacles = GameObject.Find("MailGame").GetComponent<MailGameObstacles>();
    }

    // Update is called once per frame
    void Update()
    {
        rb.AddForce(new Vector2(0, -gravity));
        if(Input.GetMouseButtonDown(0))
        {
            rb.velocity = Vector2.zero;
            rb.AddForce(new Vector2(0, jumpForce), ForceMode2D.Impulse);
        }
    }

    private void OnCollisionEnter2D(Collision2D other)
    {
        if (other.collider.tag == "Obstacle")
        {
            Debug.Log("Game Over");
            rb.freezeRotation = true;
            rb.velocity = Vector2.zero;
            obstacles.FreezeAll();
        }
        if (other.collider.tag == "PlayerDetector")
        {
            handler.AddScore();
        }

    }
}
