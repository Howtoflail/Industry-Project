using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MailGamePlayer : MonoBehaviour
{
    // Start is called before the first frame update
    public MailGameObstacles obstacles;

    private float deliveries;

    [SerializeField]
    private float deliveriesToComplete,
        chanceToDrop;

    private AudioSource audioSource;

    [SerializeField]
    AudioClip jumpClip;

    [SerializeField]
    float pitchMin,
        pitchMax;

    [SerializeField]
    GameObject mail;

    [SerializeField]
    private float gravity;

    [SerializeField]
    private float jumpForce;
    private bool isDead;

    private Rigidbody2D rb;

    private MailGameHandler handler;

    private List<GameObject> mails;

    void Start()
    {
        deliveries = 0;
        audioSource = GetComponent<AudioSource>();
        mails = new List<GameObject>();
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
            audioSource.pitch = Random.Range(pitchMin, pitchMax);
            audioSource.clip = jumpClip;
            audioSource.Play();
            rb.velocity = Vector2.zero;
            rb.AddForce(new Vector2(0, jumpForce), ForceMode2D.Impulse);
        }

        if (deliveries >= deliveriesToComplete)
        {
            handler.Win();
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
            DropMail(other.GetComponent<ChimneyDetecor>().targetObject);
        }
    }

    public bool IsDead()
    {
        return isDead;
    }

    public void ResetPosition()
    {
        deliveries = 0;
        this.GetComponent<RectTransform>().localPosition = new Vector3(0f, 0f, -20);
        // foreach(GameObject mailObjects in mails)
        // {
        //     Destroy(mailObjects);
        // }
        // mails.Clear();
    }

    private void DropMail(GameObject target)
    {
        if (Random.Range(0f, 1f) <= chanceToDrop)
        {
            GameObject newMail = Instantiate(
                mail,
                gameObject.transform.position,
                Quaternion.identity
            );

            deliveries++;

            newMail.GetComponent<PlaneDelivery>().target = target;
        }

        // mails.Add(newMail);
    }
}
