using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BackgroundAlien : MonoBehaviour
{
    Rigidbody2D myRB;
    public Rigidbody2D player;

    SpriteRenderer myRenderer;
    Animator animator;
    public int maxSpeed;
    int movement = 0;
    Vector2 newPosition;
    Vector2 randomPosition;

    int health = 20;

    // Use this for initialization
    void Start()
    {
        myRB = GetComponent<Rigidbody2D>();
        myRenderer = GetComponent<SpriteRenderer>();
        animator = GetComponent<Animator>();
    }

    // Update is called once per frame
    void FixedUpdate()
    {

        if ((Time.fixedTime) % 1 == 0)
        {
            randomPosition = RandomVector();
            movement = Random.Range(0, 7);
        }

        newPosition = Vector2.MoveTowards(myRB.position, randomPosition, 10 * Time.fixedDeltaTime);
        myRB.MovePosition(newPosition);





        //newPosition = Vector2.MoveTowards(myRB.position, player.position, 10 * Time.deltaTime);
        //myRB.MovePosition(newPosition);

        //flip sprite towards player
        if (player.position.x > myRB.position.x)
        {
            myRenderer.flipX = true;
        }
        if (player.position.x < myRB.position.x)
        {
            myRenderer.flipX = false;
        }



    }



    private Vector2 RandomVector()
    {
        var x = Random.Range(player.position.x - 100, player.position.x + 100);
        var y = Random.Range(player.position.y - 100, player.position.y + 100);
        return new Vector2(x, y);
    }
}
