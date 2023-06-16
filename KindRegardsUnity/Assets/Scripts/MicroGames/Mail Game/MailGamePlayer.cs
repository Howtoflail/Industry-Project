using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MailGamePlayer : MonoBehaviour
{
    // Start is called before the first frame update
    public MailGameObstacles obstacles;

    [SerializeField]
    GameObject mail;

    [SerializeField]
    private float gravity;

    [SerializeField]
    private float jumpForce;
    private bool isDead;

    private Rigidbody2D rb;

    private MailGameHandler handler;

    void Start()
    {
        handler = GameObject.Find("MailGame").GetComponent<MailGameHandler>();
        rb = GetComponent<Rigidbody2D>();
        obstacles = GameObject.Find("MailGame").GetComponent<MailGameObstacles>();
    }

    // Update is called once per frame
    void FixedUpdate()
    {
        rb.AddForce(new Vector2(0, -gravity));
    }

    void Update()
    {
        if (Input.GetMouseButtonDown(0))
        {
            rb.velocity = Vector2.zero;
            rb.AddForce(new Vector2(0, jumpForce), ForceMode2D.Impulse);
        }
    }

    private void OnCollisionEnter2D(Collision2D other)
    {
        if (other.collider.tag == "Obstacle")
        {
            rb.freezeRotation = true;
            rb.velocity = Vector2.zero;
            handler.GameOver();
        }
    }

    private void OnTriggerEnter2D(Collider2D other)
    {
        if (other.tag == "PlayerDetector")
        {
            handler.AddScore();
            DropMail(other.GetComponent<ChimneyDetecor>().chimney);
        }
        
    }

    public bool IsDead()
    {
        return isDead;
    }

    public void ResetPosition()
    {
        this.GetComponent<RectTransform>().localPosition = new Vector3(0f, 0f, -20);
    }

    private void DropMail(GameObject target)
    {
        var planeDelivery = Instantiate(mail, gameObject.transform.position, Quaternion.identity).GetComponent<PlaneDelivery>();
        
        planeDelivery.target = target;

    }
    
            
    
}
