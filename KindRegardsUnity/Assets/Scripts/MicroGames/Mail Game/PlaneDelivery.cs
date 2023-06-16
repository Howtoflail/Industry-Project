using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlaneDelivery : MonoBehaviour
{
    // Start is called before the first frame update

    [SerializeField] private float speed = 10f;
    public GameObject target;

    void Start() { }

    // Update is called once per frame
    private void FixedUpdate()
    {
        transform.LookAt(target.transform, Vector3.up);
        transform.Rotate(new Vector3(90, 0, 0));
        transform.position = Vector3.MoveTowards(
            transform.position,
            target.transform.position,
            speed * Time.fixedDeltaTime
        );

        if (Vector3.Distance(transform.position, target.transform.position) < 1f)
        {
            Destroy(gameObject);
        }
        
        
    }
}
