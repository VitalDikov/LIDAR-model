using UnityEngine;

public class Move : MonoBehaviour
{
    public int speed;

    private bool isMoving;

    private Rigidbody2D rb;
    // Start is called before the first frame update
    void Start()
    {
        rb = GetComponent<Rigidbody2D>();
    }

    // Update is called once per frame
    void Update()
    {
        // Rotation
        Vector2 direction = Camera.main.ScreenToWorldPoint(Input.mousePosition) - transform.position;
        float angle = Mathf.Atan2(direction.y, direction.x) * Mathf.Rad2Deg;
        Quaternion rotation = Quaternion.AngleAxis(angle - 90, Vector3.forward);
        transform.rotation = rotation;
        // Rotation

        if (Input.GetKey("w")) {
            isMoving = true;
        } else {
            isMoving = false;
        }

        if (isMoving) {
            rb.velocity = transform.up * speed;
            //rb.AddForce(transform.up * speed);
        } else {
            rb.velocity = Vector3.zero;
        }
    }
}
