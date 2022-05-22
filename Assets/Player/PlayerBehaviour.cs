using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using TMPro;

public class PlayerBehaviour : MonoBehaviour
{
    public int score = 0;
    public BarrierBehavior barrierBehavior;
    public TMP_Text scoreTxt;

    public float jumpForce;

    private bool jump;
    private Rigidbody2D rb;
    private bool starting = false;

    // Start is called before the first frame update
    void Start()
    {
        rb = GetComponent<Rigidbody2D>();
        rb.gravityScale = 0;
    }

    // Update is called once per frame
    void Update()
    {
        #region Jump
        if (Input.GetButtonDown("Jump"))
        {
            jump = true;
            if (!starting)
            {
                starting = true;
                barrierBehavior.StartGame();
                rb.gravityScale = 1;
            }
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

        scoreTxt.text = $"{score}";
    }

    void FixedUpdate()
    {
        Jump();
    }

    /// <summary>
    /// Quand le requin se prend un rocher ou un ameçon, il meurt
    /// </summary>
    /// <param name="collision">Obstacle</param>
    private void OnCollisionEnter2D(Collision2D collision)
    {
        if (collision.collider.CompareTag("Decor"))
        {
            Death();
        }
    }

    /// <summary>
    /// Ajoute 1 au score quand le requin passe dans la zone invisible
    /// </summary>
    /// <param name="collision">Zone invisible</param>
    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.CompareTag("Scoring"))
        {
            score++;
            Destroy(collision.gameObject);
        }
    }

    /// <summary>
    /// Quand le joueur appuis sur la touche "espace", le requin saute
    /// </summary>
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

    /// <summary>
    /// Quand le requin meurt, on charge la scène "GameOver" et on fait passer le score d'une scène à l'autre
    /// </summary>
    public void Death()
    {
        CrossScene.Score = scoreTxt.text;
        SceneManager.LoadScene("GameOver");
    }
}

/// <summary>
/// Permet de faire passer des infos d'une scène à l'autre
/// </summary>
public static class CrossScene
{
    /// <summary>
    /// Score à faire passer
    /// </summary>
    public static string Score { get; set; }
}
