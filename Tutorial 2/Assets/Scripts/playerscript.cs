using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class playerscript : MonoBehaviour
{
    private bool facingRight = true;
    private Rigidbody2D rd2d;

    public float speed;

    public TextMeshProUGUI scoreText;

    public TextMeshProUGUI livesText;

    public GameObject winTextObject;

    public GameObject loseTextObject;

    private int score = 0;

    private int lives = 3;

    public AudioClip musicClipOne;

    public AudioClip musicClipTwo;

    public AudioSource musicSource;

    Animator anim;

    // Start is called before the first frame update
    void Start()
    {
        rd2d = GetComponent<Rigidbody2D>();

        winTextObject.SetActive(false);

        SetScoreText();

        loseTextObject.SetActive(false);

        SetLivesText();

        anim = GetComponent<Animator>();

    }

    void Update()
    {
        if (Input.GetKeyDown(KeyCode.W))

        {

          anim.SetInteger("State", 1);

         }

     if (Input.GetKeyUp(KeyCode.W))

        {

          anim.SetInteger("State", 0);

         }

     if (Input.GetKeyDown(KeyCode.R))

        {

          musicSource.clip = musicClipTwo;

          musicSource.Play();

          anim.SetInteger("State", 2);

         }

     if (Input.GetKeyUp(KeyCode.R))

        {

          musicSource.Stop();

          anim.SetInteger("State", 0);

         }

     if (Input.GetKeyDown(KeyCode.L))

        {

          musicSource.loop = true;

         }

     if (Input.GetKeyUp(KeyCode.L))

        {

          musicSource.loop = false;

        }
    }
    
    // Update is called once per frame
    void FixedUpdate()
    {
        float hozMovement = Input.GetAxis("Horizontal");
        float vertMovement = Input.GetAxis("Vertical");
        rd2d.AddForce(new Vector2(hozMovement * speed, vertMovement * speed));

        if (facingRight == false && hozMovement > 0)
        {
            Flip();
        }
        else if (facingRight == true && hozMovement < 0)
        {
            Flip();
        }
    }
    void Flip()
   {
     facingRight = !facingRight;

     Vector2 Scaler = transform.localScale;

     Scaler.x = Scaler.x * -1;

     transform.localScale = Scaler;
   }
    private void OnCollisionEnter2D(Collision2D collision)
    {
       if (collision.collider.tag == "Coin")
        {
            score += 1;

            collision.gameObject.SetActive(false);

            Destroy(collision.collider.gameObject);

            SetScoreText();
        }
        else if (collision.collider.tag == "Enemy")
        {
            lives -= 1;
            
            collision.gameObject.SetActive(false);

            Destroy(collision.collider.gameObject);
            
            SetLivesText();
        }
        if (score == 4)
        {
            transform.position = new Vector2(150.0f, 2.0f);
        }
    }

    void OnCollisionStay2D(Collision2D collision)
    {
        if(collision.collider.tag == "Ground")
        {
            if(Input.GetKey(KeyCode.W))
            {
                rd2d.AddForce(new Vector2(0,4), ForceMode2D.Impulse);
            }
        }
    }
    void SetScoreText()
    {
        scoreText.text = "Score: " + score.ToString();

        if (score == 8)
        {
            musicSource.clip = musicClipOne;
            musicSource.Play();
            winTextObject.SetActive(true);
        }
    }
    void SetLivesText()
    {
        livesText.text = "Lives: " + lives.ToString();

        if (lives <= 0)
        {
            loseTextObject.SetActive(true);

            Destroy(gameObject);
        }
    }
}