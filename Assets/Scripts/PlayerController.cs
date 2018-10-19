using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class PlayerController : MonoBehaviour {
    public Rigidbody2D myRB;
    public float maxSpeed;
    public float jumpPower;
    public GameStart gameStart;
    SpriteRenderer myRenderer;
    Animator myAnim;
    public bool facingRight = true;
    bool grounded = false;
    float groundCheckRadius = 0.2f;
    public LayerMask groundLayer;
    public Transform groundCheck;
    public GameObject ContextPopup;
    public GameObject ContextPopupText;
    public GameObject PlayerBullet;

    public GameObject HealthPopup;
    public GameObject HealthText;

    bool touchJumpPad = false;
    bool jumpJumpPad = false;
    public float jumpPadPower;
    bool damaged;
    bool gameFinished;

    int health;
    float timeSinceLastShot = 0f;
    public float reloadTime;

    public GameObject GameFinished;
    public GameObject GameFinishedText;

    public GameObject transferPlatform;
    public GameObject console;


    GameObject[] platforms;

	// Use this for initialization
	void Start () {
        GameFinished.SetActive(false);
        ContextPopup.SetActive(false);
        myRB = GetComponent<Rigidbody2D>();
        myRenderer = GetComponent<SpriteRenderer>();
        myAnim = GetComponent<Animator>();
        GameFinished.GetComponent<Button>().interactable = false;
        health = 100;
        myRenderer.sortingOrder = 0;
        PlayerBullet.GetComponent<SpriteRenderer>().sortingOrder = 4;
	}

    public void Reset()
    {
        platforms = GameObject.FindGameObjectsWithTag("Level 2 Platform");
        for (int i = 0; i < platforms.Length; i++)
        {
            platforms[i].GetComponent<BoxCollider2D>().enabled = false;
        }
        platforms = GameObject.FindGameObjectsWithTag("Level 1 Platform");
        for (int i = 0; i < platforms.Length; i++)
        {
            platforms[i].GetComponent<BoxCollider2D>().enabled = true;
        }

        myRenderer.sortingOrder = 0;
        PlayerBullet.GetComponent<SpriteRenderer>().sortingOrder = 4;
        ContextPopup.SetActive(false);
        HealthPopup.SetActive(true);
        myRB.velocity = Vector2.zero;
        //myRB.position = new Vector2(20.3f, -93.9f);
        transform.position = new Vector3(17.7f, -93.1f, 0);
        myRB.angularVelocity = 0;
        myRB.rotation = 0;
        myRB.freezeRotation = true;
        health = 100;
        transferPlatform.transform.position = new Vector3(transferPlatform.transform.position.x,transferPlatform.transform.position.y,0);
        console.transform.position = new Vector3(console.transform.position.x, console.transform.position.y, 0);

    }

    // Update is called once per frame
    void FixedUpdate () {
        float shoot = Input.GetAxis("Fire1");

        if(shoot>0&&Time.fixedTime-timeSinceLastShot>reloadTime)
        {
            if(myRenderer.flipX)
            {
                Rigidbody2D bulletClone = Instantiate(PlayerBullet, transform.position+new Vector3(-2.07f,1.43f,0), transform.rotation).GetComponent<Rigidbody2D>();
                bulletClone.velocity = new Vector2(-50, 0);
            }
            else
            {
                Rigidbody2D bulletClone = Instantiate(PlayerBullet, transform.position+new Vector3(2.07f, 1.43f,0), transform.rotation).GetComponent<Rigidbody2D>();
                bulletClone.velocity = new Vector2(50, 0);
            }

            timeSinceLastShot = Time.fixedTime;
        }
        if (grounded && Input.GetAxis("Jump") > 0)
        {
            if (touchJumpPad && !jumpJumpPad)
            {
                myRB.velocity = new Vector2(myRB.velocity.x, 0f);
                myRB.AddForce(new Vector2(0, jumpPadPower), ForceMode2D.Impulse);
                jumpJumpPad = true;
            }
            if(!jumpJumpPad)
            {
                myRB.velocity = new Vector2(myRB.velocity.x, 0f);
                myRB.AddForce(new Vector2(0, jumpPower), ForceMode2D.Impulse);
                grounded = false;
            }


        }

        grounded = Physics2D.OverlapCircle(groundCheck.position, groundCheckRadius, groundLayer);

        float move = Input.GetAxis("Horizontal");

        if (move > 0 && !facingRight)
            Flip();
        else if (move < 0 && facingRight)
            Flip();

        myRB.velocity = new Vector2(move * maxSpeed, myRB.velocity.y);
        myAnim.SetFloat("moveSpeed", Mathf.Abs(move));




        //update health
        HealthText.GetComponent<Text>().text = "Health: "+health;


        //check if player is dead
        if(health<=0||myRB.position.y<-113)
        {
            ContextPopup.SetActive(false);
            HealthPopup.SetActive(false);
            myRB.freezeRotation = false;
            myRenderer.sortingOrder = -1;
            health = 100;
            myRB.angularVelocity = 20;
            gameStart.StartAppear();
            enabled = false;
            myAnim.SetFloat("moveSpeed", 0);

        }

        if(gameStart.startUp)
        {
            myRB.velocity = Vector2.zero;
        }
	}

    void Flip()
    {
        facingRight = !facingRight;
        myRenderer.flipX = !myRenderer.flipX;
    }

    //collide with jump pad, display context pop-up
    void OnTriggerEnter2D(Collider2D col)
    {
        if (col.gameObject.tag == "Jump Pad")
        {
            touchJumpPad = true;
            ContextPopup.SetActive(true);
            ContextPopupText.GetComponent<Text>().text = "Press space to use the jump pad";
        }
        if (col.gameObject.tag == "Enemy Bullet Small"&&!damaged)
        {
            GameObject.Destroy(col.gameObject);
            health = health - 5;
            damaged = true;
            StartCoroutine(FlashPlayer());
        }
        if (col.gameObject.name == "titan_eye")
        {
            gameFinished = true;
            HealthText.SetActive(false);
            HealthPopup.SetActive(false);
            myRB.gravityScale = 0;
            myRB.velocity = Vector2.zero;
            health = 10000000;
            StartCoroutine(FinishGame());

        }
    }

    void OnTriggerExit2D(Collider2D col)
    {
        if (col.gameObject.tag == "Jump Pad")
        {
            touchJumpPad = false;
            jumpJumpPad = false;
            ContextPopup.SetActive(false);
        }
    }

    IEnumerator FlashPlayer()
    {
        for (int i = 0; i < 5;i++)
        {
            myRenderer.color = new Color(255, 0, 0);
            yield return new WaitForSeconds(0.1f);
            myRenderer.color = new Color(100, 100, 100);
            yield return new WaitForSeconds(0.1f);   
        }
        damaged = false;
        yield break;

    }

    IEnumerator FinishGame()
    {

        GameFinished.SetActive(true);
        while (GameFinished.GetComponent<Image>().color.a < 1)
        {
            var tempColor = GameFinished.GetComponent<Image>().color;
            tempColor.a = GameFinished.GetComponent<Image>().color.a + 0.04f;
            GameFinished.GetComponent<Image>().color = tempColor;
            yield return null;
        }
        GameFinishedText.SetActive(true);
        GameFinished.GetComponent<Button>().interactable = true;

    }




}
