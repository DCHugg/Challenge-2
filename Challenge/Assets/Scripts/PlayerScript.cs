using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class PlayerScript : MonoBehaviour
{
    private Rigidbody2D rd2d;

    public float speed;

    public Text score;

    private int scoreValue = 0;

    private int livesValue = 3;

    public Text lives;

    public Text winText;

    public AudioSource musicSource;

    public AudioClip musicClipOne;

    public AudioClip musicClipTwo;

    Animator anim; 

    private bool facingRight = true;


    // Start is called before the first frame update
    void Start()
    {

        rd2d = GetComponent<Rigidbody2D>();
        anim = GetComponent<Animator>();
        score.text = "Score: " +  scoreValue.ToString();
        lives.text = "Lives: " + livesValue.ToString();
        winText.text = "";
        musicSource.clip = musicClipOne;
        musicSource.Play();
    
    }

    // Update is called once per frame
    void Update()
    {

        float hozMovement = Input.GetAxis("Horizontal");
        float vertMovement = Input.GetAxis("Vertical");
        rd2d.AddForce(new Vector2(hozMovement * speed, vertMovement * speed));


        if (Input.GetKeyDown(KeyCode.W))

        {

         anim.SetInteger("State", 3);

         }

     if (Input.GetKeyUp(KeyCode.W))

        {

         anim.SetInteger("State", 0);

         }

     if (Input.GetKeyDown(KeyCode.A))

        {

         anim.SetInteger("State", 2);

         }

     if (Input.GetKeyUp(KeyCode.A))

        {

  	 anim.SetInteger("State", 0);

         }

     if (Input.GetKeyDown(KeyCode.D))

        {

	anim.SetInteger("State",2 );
          
         }

     if (Input.GetKeyUp(KeyCode.D))

        {

          anim.SetInteger("State",0 );

        }

        if (facingRight == false && hozMovement > 0)
   {
     Flip();
   }
else if (facingRight == true && hozMovement < 0)
   {
     Flip();
   }

   if (Input.GetKey("escape"))
{
Application.Quit();
}

    }




    private void OnCollisionEnter2D(Collision2D collision)
    {
       if (collision.collider.tag == "Coin")
        {
            scoreValue += 1;
            SetScoreText ();
            Destroy(collision.collider.gameObject);
            AdvanceStage();
            
        }

         else if (collision.collider.tag =="Enemy")
     {
         livesValue -= 1;
         SetLivesText();
          Destroy(collision.collider.gameObject);
     }

    }

    private void OnCollisionStay2D(Collision2D collision)
    {
        if (collision.collider.tag == "Ground")
        {
            if (Input.GetKey(KeyCode.W))
            {
                rd2d.AddForce(new Vector2(0, 3), ForceMode2D.Impulse);
            }
        }
    }

    private void AdvanceStage()
    {
        if (scoreValue == 4)
        {
            transform.position = new Vector2(23.0f,0.0f);
            livesreset();
        }
    }


    void livesreset()
    {
        livesValue = 3;
        SetLivesText();
    }


    void SetScoreText()
    {
        score.text =  "Score: " +  scoreValue.ToString();

        if(scoreValue >=8)
        {
            musicSource.Stop();
            musicSource.clip = musicClipTwo;
            musicSource.Play();
            winText.text= "You Win! Game by DariAsh Huggins DIG3480";
            
        }
    }

    void SetLivesText()
    {
    lives.text = "Lives: " + livesValue.ToString();

        if(livesValue ==0)
        {
            Destroy(gameObject);
            winText.text = "You lose Game Over! by DariAsh Huggins DIG3480";
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



