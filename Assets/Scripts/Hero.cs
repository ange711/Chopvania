using UnityEngine;
using System.Collections;

public class Hero : MonoBehaviour
{
	float runSpeed = 8f;
	float climbSpeed = 5f;
	public float hurtSpeed = -2f;
	public float jumpForce = 800f;
	public LayerMask groundMask;
	public GameObject bullet;
	public GameObject deathParticle;
	public bool isAlive = true;

	Rigidbody2D rigid;
	bool canClimb = false;
	bool isFacingRight = false;
	BoxCollider2D boxCollider;
	bool isGrounded = false;
	float attackDelay = 0.25f;
	public float hurtTime = 0;
	public float invincibleTime = 0;
	SpriteRenderer spriteRenderer;
	bool isLanded = false;


	void Awake()
	{

		boxCollider = GetComponent<BoxCollider2D>();
		spriteRenderer = GetComponent<SpriteRenderer>();
	}

	void Start()
	{
		Flip();
	}

	void Update()
	{
		UpdateInvincibility();
		Vector2 pointA = transform.position;
		pointA += new Vector2(-boxCollider.size.x/2f, -boxCollider.size.y/2f);
		Vector2 pointB = pointA;
		pointB += new Vector2(boxCollider.size.x,  -0.1f);
		isGrounded = Physics2D.OverlapArea(pointA, pointB, groundMask);

		if (isGrounded && !isLanded)
		{
			isLanded = true;
		}
		hurtTime = Mathf.Max(0, hurtTime - Time.deltaTime);
		if (hurtTime > 0)
		{
			rigidbody2D.velocity = new Vector2(Orient(hurtSpeed), 0);
			return;
		}
		if (canClimb) {
			float moveLadder = Input.GetAxis("Vertical");
			if(moveLadder != 0){
				rigidbody2D.gravityScale = 0;
				rigidbody2D.velocity = new Vector2(rigidbody2D.velocity.x, moveLadder * climbSpeed);
			}
		}

		float move = Input.GetAxis("Horizontal");
		rigidbody2D.velocity = new Vector2(move * runSpeed, rigidbody2D.velocity.y);
		if ((move > 0 && !isFacingRight) || (move < 0 && isFacingRight))
			Flip();
		if (isGrounded && Input.GetButtonDown("Jump"))
		{
			rigidbody2D.gravityScale = 3;
			rigidbody2D.AddForce(new Vector2(0, jumpForce));
		}
		else if (Input.GetButtonUp("Jump") && rigidbody2D.velocity.y > 0)
		{
			rigidbody2D.velocity = new Vector2(rigidbody2D.velocity.x, 0);
		}
		if (!isGrounded)
			isLanded = false;
		UpdateShooting();
	}


	void ApplyDamage(int damage)
	{
		if (invincibleTime > 0)
			return;
		hurtTime = 0.5f;
		invincibleTime = 2f;
	}

	void Flip()
	{
		isFacingRight = !isFacingRight;
		Vector3 nextScale = transform.localScale;
		nextScale.x *= -1f;
		transform.localScale = nextScale;
	}

	float Orient(float value)
	{
		return isFacingRight ? value : -value;
	}

	void UpdateInvincibility()
	{
		invincibleTime = Mathf.Max(0, invincibleTime - Time.deltaTime);
		Color nextColor = spriteRenderer.color;
		if (invincibleTime > 0)
		{
			bool isFlashStrong = ((int)(invincibleTime * 15) %2) == 0;
			nextColor.a = isFlashStrong ? 0.75f : 0.25f;
		}
		else
		{
			nextColor.a = 1f;
		}
		spriteRenderer.color = nextColor;
	}

	void UpdateShooting()
	{
		attackDelay = Mathf.Max(0, attackDelay - Time.deltaTime);
		if (Input.GetButtonDown("Fire1") && attackDelay <= 0.0f)
		{
		}
	}

	public bool IsInvincible()
	{
		return invincibleTime > 0;
	}

	public void Kill()
	{
		if (!isAlive)
			return;
		Die();
	}

	public void Die()
	{
		isAlive = false;
		Instantiate(deathParticle, transform.position, Quaternion.identity);
		spriteRenderer.enabled = false;
		rigidbody2D.velocity = Vector2.zero;
		this.enabled = false;
	}

	public void climbMode(){
		canClimb = true;
	}

	public void climbOff(){
		canClimb = false;
		rigidbody2D.gravityScale = 3;
	}
}
