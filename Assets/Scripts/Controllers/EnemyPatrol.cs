using UnityEngine;
using System.Collections;

public class EnemyPatrol : MonoBehaviour
{
    public Transform[] patrolPoints;
    public float speed = 2f;
    public float waitTimeAtPoint = 1f;

    private int currentPointIndex = 0;
    private bool movingForward = true;
    private bool isWaiting = false;
    private Animator animator;
    private Rigidbody2D rb; // optional kalau pakai physics

    void Start()
    {
        animator = GetComponent<Animator>();
        rb = GetComponent<Rigidbody2D>(); // optional
    }

    void Update()
    {
        if (isWaiting || patrolPoints.Length == 0)
        {
            animator.SetFloat("Speed", 0f);
            return;
        }

        Transform target = patrolPoints[currentPointIndex];
        Vector2 direction = (target.position - transform.position).normalized;

        // Gerak
        transform.position = Vector2.MoveTowards(transform.position, target.position, speed * Time.deltaTime);

        // Flip arah
        if (direction.x > 0.01f)
            transform.localScale = new Vector3(1f, transform.localScale.y, 1f); // kanan
        else if (direction.x < -0.01f)
            transform.localScale = new Vector3(-1f, transform.localScale.y, 1f); // kiri

        // Atur animasi
        float movementSpeed = speed;
        if (Vector2.Distance(transform.position, target.position) < 0.05f)
        {
            movementSpeed = 0f;
            StartCoroutine(WaitAtPoint());
        }

        animator.SetFloat("Speed", movementSpeed);
    }



    IEnumerator WaitAtPoint()
    {
        isWaiting = true;
        yield return new WaitForSeconds(waitTimeAtPoint);

        // Tentukan arah selanjutnya
        if (movingForward)
        {
            currentPointIndex++;
            if (currentPointIndex >= patrolPoints.Length)
            {
                movingForward = false;
                currentPointIndex = patrolPoints.Length - 2; // balik ke titik sebelumnya
            }
        }
        else
        {
            currentPointIndex--;
            if (currentPointIndex < 0)
            {
                movingForward = true;
                currentPointIndex = 1; // balik arah
            }
        }

        isWaiting = false;
    }
}
