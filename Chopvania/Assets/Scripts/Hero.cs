﻿using UnityEngine;
using System.Collections;

public class Hero : MonoBehaviour
{
	public GameObject lifebar;
	float runSpeed = 8f;
	float climbSpeed = 5f;
	public float hurtSpeed = -2f;
	public float jumpForce = 800f;
	public LayerMask groundMask;
	public bool isAlive = true;

	bool canClimb = false;
	public bool isFacingRight = false;
	BoxCollider2D boxCollider;
	bool isGrounded = false;
	float attackDelay = 0.5f;
	public float hurtTime = 0;
	public float invincibleTime = 0;
	SpriteRenderer spriteRenderer;
	bool isLanded = false;
	Animator animator;

	int health = 10;

	//Weapon List
	int weaponType = 0;
	int Ammo = 0;
	int weaponsClose = 0;
	bool canDrop = false;
	float dropTimer = 0.5f;
	public GameObject footObj;
	public GameObject barObj;
	public GameObject droppedBar;
	public GameObject knifeObj;
	public GameObject droppedKnife;
	public GameObject skilletObj;
	public GameObject droppedSkillet;
	public GameObject TURKEYObj;
	public GameObject droppedTURKEY;

	//Animations
	bool climbing = false;
	bool attacking = false;
	float move = 0f;
	bool jumping = false;
	bool eatPie = false;

	//Sounds
	public AudioSource donePie;
	public AudioSource death;
	public AudioSource eat;
	public AudioSource kick;
	public AudioSource hurt;
	public AudioSource jump;
	public AudioSource Run;
	public AudioSource smack;
	public AudioSource throwing;


	void Awake()
	{
		animator = GetComponent<Animator>();
		boxCollider = GetComponent<BoxCollider2D>();
		spriteRenderer = GetComponent<SpriteRenderer>();
	}

	void Start()
	{
		Flip();
	}


	

	void Update()
	{

		Vector2 pointA = transform.position;
		pointA += new Vector2((-boxCollider.size.x/2f)*5f, (-boxCollider.size.y/2f)*5f);
		Vector2 pointB = pointA;
		pointB += new Vector2(boxCollider.size.x *5f,  -0.1f);
		isGrounded = Physics2D.OverlapArea(pointA, pointB, groundMask);
		jumping = !isGrounded;
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
			float moveLadder = Input.GetAxis ("Vertical");
			if (moveLadder != 0) {
				rigidbody2D.gravityScale = 0;
				rigidbody2D.velocity = new Vector2 (rigidbody2D.velocity.x, moveLadder * climbSpeed);
				climbing = true;
			}
		}
		else
			climbing = false;

		move = Input.GetAxis("Horizontal");
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
		//Add for each weapon that has ammo, with 
		if (Ammo == 0 && (weaponType != 0)) {
			weaponType = 0;
			animator.SetInteger("WeaponNumber", 0);
			canDrop = false;
			}

		if (Input.GetButtonDown ("Fire2") && weaponType != 0 && canDrop && isLanded && !canClimb && weaponsClose == 0) {
			DropWeapon ();
			canDrop = false;
		}

		UpdateAttack();
		UpdateAnimation ();
		UpdateInvincibility();
	}


	void ApplyDamage(int damage)
	{
		if (invincibleTime > 0)
			return;
		hurtTime = 0.5f;
		invincibleTime = 2f;
		health -= damage;
		lifebar.GetComponent<Lifemeter>().DecrementLife(damage);
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
		spriteRenderer.enabled = false;
		rigidbody2D.velocity = Vector2.zero;
		this.enabled = false;
	}

	void UpdateAnimation(){
		animator.SetBool ("Climbing", climbing);
		animator.SetBool ("Attacking", attacking);
		animator.SetFloat ("Moving", Mathf.Abs (move));
		animator.SetBool ("Jumping", jumping);
		animator.SetBool ("GotPie", eatPie);
	}

	public void EatPie(){
		eatPie = true;
		health += 2;
		lifebar.GetComponent<Lifemeter>().IncrementLife(2);
		Invoke ("PieReseter", 0.25f);
	}

	public void PieReseter(){
		eatPie = false;
	}

	//Ladder Climbing
	public void climbMode(){
		canClimb = true;
	}

	public void climbOff(){
		canClimb = false;
		rigidbody2D.gravityScale = 3;
	}


	//Weapon Methods
	public void weaponReset(){
		attacking = false;
	}

	public void pickUpBar(int ammo){
		weaponType = 1;
		Ammo = ammo;
		animator.SetInteger ("WeaponNumber", weaponType);
		Invoke ("canDropReset", dropTimer);
	}

	public void pickUpKnife(int ammo){
		weaponType = 2;
		Ammo = ammo;
		animator.SetInteger ("WeaponNumber", weaponType);
		Invoke ("canDropReset", dropTimer);
	}

	void canDropReset(){
		canDrop = true;
	}

	void UpdateAttack()
	{
		attackDelay = Mathf.Max(0, attackDelay - Time.deltaTime);
		if (Input.GetButtonDown("Fire1") && attackDelay <= 0.0f){
			Quaternion rotater;
			attacking = true;
			switch(weaponType){
			case 0:
				Vector3 footPosition = transform.position + new Vector3(Orient (0.9f), -0.3f, 0);
				GameObject Foot = (GameObject)Instantiate(footObj, footPosition, transform.rotation );
				Invoke("weaponReset", 0.1f);
				break;

			case 1:
				Vector3 barPosition = transform.position + new Vector3(Orient (1.5f), -0.5f, 0);
				GameObject Bar = (GameObject)Instantiate(barObj, barPosition, transform.rotation);
				Ammo--;
				Invoke("weaponReset", 0.2f);
				break;

			case 2:
				Vector3 knifePosition = transform.position + new Vector3(Orient (1.5f), -0.5f, 0);
				if(!isFacingRight)
					rotater = Quaternion.Euler(0, 0, 180);
				else
					rotater = Quaternion.identity;
				
				GameObject Knife = (GameObject)Instantiate(knifeObj, knifePosition, transform.rotation * rotater);
				if(isFacingRight)
					Knife.rigidbody2D.AddForce(transform.right*800);
				else
					Knife.rigidbody2D.AddForce(-transform.right*800);
				Ammo--;
				Invoke("weaponReset", 0.2f);
				break;
			
			}
			attackDelay = 0.5f;
		}
	}

	void DropWeapon(){
		Vector3 weaponTransform = new Vector3 (transform.position.x, transform.position.y - 0.5f, transform.position.z);
			switch(weaponType){
			case 1:
			GameObject Bar = (GameObject)Instantiate(droppedBar, weaponTransform , Quaternion.identity);
				Bar.SendMessage ("setAmmo",Ammo);
				Ammo = 0;
				animator.SetInteger ("WeaponNumber", 0);
				break;
			case 2:
			GameObject Knife = (GameObject)Instantiate(droppedKnife, weaponTransform , Quaternion.identity);
				Knife.SendMessage ("setAmmo",Ammo);
				Ammo = 0;
				animator.SetInteger ("WeaponNumber", 0);
				break;
			case 3:
			GameObject Skillet = (GameObject)Instantiate(droppedKnife, weaponTransform , Quaternion.identity);
				Skillet.SendMessage ("setAmmo",Ammo);
				Ammo = 0;
				animator.SetInteger ("WeaponNumber", 0);
				break;
			}
	}

	public int getWeaponType(){
		return weaponType;
	}

	public int getWeaponsCloseBy(){
		return weaponsClose;
	}

	public void weaponInRange(){
		weaponsClose++;
	}

	public void weaponOutOfRange(){
		weaponsClose--;
	}

}
