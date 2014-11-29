using UnityEngine;
using System.Collections;

public class MiniShroom : MonoBehaviour {

    float minX;
    float maxX;
	GameObject player;
	GameObject boss;
   public GameObject stopPoint;
	CircleCollider2D circlecollider;
	bool jumpCD = true;
    float index;
    float counter;


    bool onTheGround;


    float groundBase = -18;
    float minXpoint = -25;
    float maxXpoint = 0;
    bool moveLeft = true;

    float x = 0;
    float y = 0;
    float jumpHT = 3;
    float startTime;
    bool isJumping;
	void Awake()
	{
        counter = 0;
        onTheGround = false;
        isJumping = false;
        startTime = Time.deltaTime;
         minX = 0.0f;
        maxX = -27.0f;
		player = GameObject.FindGameObjectWithTag("Player");
		boss = GameObject.FindGameObjectWithTag("Boss");
	}
	
	void OnTriggerEnter2D(Collider2D col){
		GameObject collisionObject = col.gameObject;
		if (collisionObject.tag == "PlayerWeapon"){
			boss.GetComponent<Boss>().MinionMinus();
			Destroy(gameObject);
		}
		if(collisionObject.tag == "SkilletWall" && collisionObject.GetComponent<Skillet> ().getCollider()){
			Destroy (gameObject);
		}
		if (collisionObject.tag == "Player") 
		{
			collisionObject.SendMessage ("ApplyDamage", 1);
		}
	}
	
	void Update(){


         if (Mathf.Abs(rigidbody2D.velocity.y) == 0)
         {
             onTheGround = true;
         }

        if (onTheGround == true)
        {
            if (moveLeft == true)
            {
                movingLeft();
            }
            else
                if (moveLeft == false)
                {
                    movingRight();
                }
        }
    }


    void OnCollisionEnter2D(Collision2D col)
    {
       if(col.gameObject.tag == "Boss")
       {
           moveLeft = true;
       }

        if(col.gameObject.tag == "Wall")
        {
            Debug.Log("hitting wall");
            moveLeft = false;
        }

        
    }

    void movingLeft()
    {
        x = transform.position.x - 0.1f;
        y = Mathf.Cos(counter);

        counter += 0.1f;
        if (y == stopPoint.transform.position.y)
        {
            counter = 0f;
        }
        Vector3 xPos = new Vector3(x, y + groundBase, 0);

        transform.position = xPos;
    }


    void movingRight()
    {
        x = transform.position.x + 0.1f;
        y = Mathf.Cos(counter);

        counter += 0.1f;
        if (y == groundBase)
        {
            counter = 0f;
        }
        Vector3 xPos = new Vector3(x, y + stopPoint.transform.position.y, 0);
        transform.position = xPos;
    }
	void JumpReset(){
		jumpCD = true;
	}
}
