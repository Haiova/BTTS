using UnityEngine;
using UnityEngine.UI;
using System.Collections;

public class PlayerController : MonoBehaviour {

	public float speed;
	public Text countText;
	public Text winText;

	private Rigidbody rb;
	private int count;

	public bool onGround; // Sphere grounded or not

	void Start ()
	{
		rb = GetComponent<Rigidbody>();
		count = 0;
		SetCountText ();
		winText.text = "";
		onGround = true;
	}

	void Update()
	{
		if (onGround == true) // If the sphere is grounded
		{
			if (Input.GetKeyDown(KeyCode.Space)) // And if the player press Space
			{
				rb.AddForce(0f, 5f, 0f, ForceMode.Impulse); // The sphere will jump
				onGround = false; // The sphere can't jump if it's in the air
			}
		}
	}

	void FixedUpdate ()
	{
		float moveHorizontal = Input.GetAxis ("Horizontal");
		float moveVertical = Input.GetAxis ("Vertical");

		Vector3 movement = new Vector3 (moveHorizontal, 0.0f, moveVertical);

		rb.AddForce (movement * speed);

	}

	void OnCollisionEnter(Collision other)
	{
		if (other.gameObject.CompareTag("Ground")) // If the sphere is touching the floor (tagged Ground)
		{
			onGround = true; // The sphere can jump again
		}
	}

	void OnTriggerEnter(Collider other) 
	{
		if (other.gameObject.CompareTag ( "Pick Up"))
		{
			other.gameObject.SetActive (false);
			count = count + 1;
			SetCountText ();
		}
	}

	void SetCountText ()
	{
		countText.text = "Pontos: " + count.ToString ();
		if (count >= 15)
		{
			winText.text = "OK";
		}
	}
}