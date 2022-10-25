using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;


public class PlayerScripts : MonoBehaviour
{
    private Rigidbody2D rd2d;

    public float speed;
    public Text score;

    private int scoreValue = 0;
    Animator anim;
    public GameObject winTextObject;
    public GameObject loseTextObject;
    private bool facingRight = true;  
    private int lives;
    public Text Lives;
    public int livesCount = 3;
    public AudioSource musicSource;
    public AudioClip musicClipOne;
    public AudioClip musicClipTwo;

    // Start is called before the first frame update
    void Start()
    {
        rd2d = GetComponent<Rigidbody2D>();
        score.text = "Score: " + scoreValue.ToString();
        anim = GetComponent<Animator>();
        Lives.text = "Lives: " + livesCount.ToString();
        winTextObject.SetActive(false);
        loseTextObject.SetActive(false);
        musicSource.clip = musicClipOne;
        musicSource.Play();
        
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

    void Update()
    {
        if (Input.GetKey(KeyCode.D))
        {
          anim.SetInteger("State", 1);
        }
        if (Input.GetKeyUp(KeyCode.D))
        {
          anim.SetInteger("State", 0);
        }
        if (Input.GetKey(KeyCode.A))
        {
          anim.SetInteger("State", 1);
        }
        if (Input.GetKeyUp(KeyCode.A))
        {
          anim.SetInteger("State", 0);
        }
    }

    private void OnCollisionEnter2D(Collision2D collision)
    {
       if (collision.collider.tag == "Coin")
        {
            scoreValue += 1;
            score.text = "Score: " + scoreValue.ToString();
            Destroy(collision.collider.gameObject);
        }
        else if (collision.collider.tag == "Enemy")
        {
            livesCount -= 1;
            Lives.text = "Lives: " + livesCount.ToString();
            Destroy(collision.collider.gameObject);
        }
        if (livesCount <= 0)
        {
            Destroy(gameObject);
            speed = 0;
            loseTextObject.SetActive(true);
            
        }
        if (scoreValue == 4)
        {
            transform.position = new Vector2(45f, 0.29f);
            livesCount = 3;
            Lives.text = "Lives: " + livesCount.ToString();
        }
        if (scoreValue == 8)
        {
            winTextObject.SetActive(true);
            musicSource.clip = musicClipOne;
            musicSource.Stop();
            musicSource.clip = musicClipTwo;
            musicSource.Play();
            rd2d. constraints = RigidbodyConstraints2D. FreezeAll;
        }
    }

    private void OnCollisionStay2D(Collision2D collision)
    {
        if (collision.collider.tag == "Ground")
        {
            anim.SetInteger("State", 0);
            if (Input.GetKey(KeyCode.W))
            {
                rd2d.AddForce(new Vector2(0, 3), ForceMode2D.Impulse); //the 3 in this line of code is the player's "jumpforce," and you change that number to get different jump behaviors.  You can also create a public variable for it and then edit it in the inspector.
                anim.SetInteger("State", 2);

                    
            }
           
        } 
    }

    void Flip()
    {              
          facingRight = !facingRight;                
          Vector2 Scaler = transform.localScale;                
          Scaler.x = Scaler.x * -1;                
          transform.localScale = Scaler;
    }


}
