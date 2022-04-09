using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class PlayerBehaviour : MonoBehaviour
{
    public float life;
    public int score;

    public float jumpForce;

    private bool jump;
    private Rigidbody2D rb;

    // Start is called before the first frame update
    void Start()
    {
        rb = GetComponent<Rigidbody2D>();
    }

    // Update is called once per frame
    void Update()
    {
        #region Jump
        if (Input.GetButtonDown("Jump"))
        {
            jump = true;
        }
        #endregion

        #region Animation
        if (rb.velocity.y > 1)
        {
            transform.rotation = Quaternion.Euler(0, 0, 40);
        } else if (rb.velocity.y < -1)
        {
            transform.rotation = Quaternion.Euler(0, 0, -40);
        } else
        {
            transform.rotation = Quaternion.identity;
        }
        #endregion
    }

    void FixedUpdate()
    {
        Jump();
    }

    private void OnCollisionEnter2D(Collision2D collision)
    {
        if (collision.collider.CompareTag("Decor"))
        {
            Death();
        }
    }

    private void Jump()
    {
        if (jump)
        {
            jump = false;
            Vector2 move = rb.velocity;
            move.y = jumpForce;
            rb.velocity = move;
        }
    }

    public void Death()
    {
        SceneManager.LoadScene("Menu");
    }
}
