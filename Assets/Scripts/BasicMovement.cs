using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BasicMovement : MonoBehaviour {

	public Animator animator;
	public float speed = 2;
	public float attackRange;
	public int meleeDamage = 10;
    public int direction = 0;
	public Transform meleePoint;

    public Rigidbody2D rb;
	
	// Update is called once per frame
	void Update () 
	{
		animator.SetFloat ("Horizontal", Input.GetAxis ("Vertical"));

		Vector2 movement = new Vector2 (Input.GetAxisRaw ("Horizontal"), Input.GetAxisRaw ("Vertical"));

		animator.SetFloat ("Horizontal", movement.x);
		animator.SetFloat ("Vertical", movement.y);
        animator.SetFloat("Magnitude", movement.magnitude);
        GetDirection(movement);


        //transform.position = transform.position + movement * Time.deltaTime * speed;

        rb.MovePosition(rb.position + movement * Time.fixedDeltaTime * speed);

        if (Input.GetButtonDown ("Fire2")) 
		{
			animator.SetTrigger ("melee");
			Collider2D[] hitObjects = Physics2D.OverlapCircleAll (meleePoint.position, attackRange);
			if (hitObjects.Length > 1) 
			{
                if (hitObjects[0].tag == "Player")
                {
                    hitObjects[1].SendMessage("TakeDamage", meleeDamage, SendMessageOptions.DontRequireReceiver);
                    Debug.Log("Hit " + hitObjects[1].name);
                }
                else 
                {
                    hitObjects[0].SendMessage("TakeDamage", meleeDamage, SendMessageOptions.DontRequireReceiver);
                    Debug.Log("Hit " + hitObjects[0].name);
                }
			}
		}
        


    }

    void OnCollisionEnter(Collision collision)
    {
        if (gameObject.CompareTag("interObject"))
        {
            rb.velocity = Vector2.zero;
        }
    }

    void GetDirection(Vector2 movement)
    {
        if (movement.y < 0)
        {
            animator.SetInteger("Direction", 0);
        }
        if (movement.x < 0)
        {
            animator.SetInteger("Direction", 1);
        }
        if (movement.y > 0)
        {
            animator.SetInteger("Direction", 2);
        }
        if (movement.x > 0)
        {
            animator.SetInteger("Direction", 3);
        }
    }
}
