using UnityEngine;
using UnityEngine.UI;
using System.Collections;

public class Vacuum : MonoBehaviour
{
    [SerializeField]
    float mSpeed;
    [SerializeField]
    float mAngularSpeed;

	private int TotalDirtPieces;
	const string DirtTag = "Dirt";

	[SerializeField]
	Text CounterText;

	const string FixedString = "Dirt Pieces Left : ";

	void Awake ()
	{
		GameObject[] DirtPieces = GameObject.FindGameObjectsWithTag (DirtTag);
		TotalDirtPieces = DirtPieces.Length;
		UpdateCounterText (TotalDirtPieces);
	}

    void Update ()
    {
        float horizontal = Input.GetAxis ("Horizontal");
        float vertical = Input.GetAxis ("Vertical");

        if (!Mathf.Approximately (vertical, 0.0f) || !Mathf.Approximately (horizontal, 0.0f))
        {
            Vector3 direction = new Vector3 (0.0f, 0.0f, vertical);
            direction = Vector3.ClampMagnitude (direction, 1.0f);
            transform.Translate (direction * mSpeed * Time.deltaTime);
            transform.RotateAround(transform.position, Vector3.up, horizontal * mAngularSpeed * Time.deltaTime);
        }
    }

	void UpdateCounterText(int NumberOfDirtPieces)
	{
		CounterText.text = FixedString + NumberOfDirtPieces;
	}
		

	void OnTriggerEnter(Collider col){
		if (col.gameObject.CompareTag(DirtTag))	
		{
			Destroy (col.gameObject);
			TotalDirtPieces--;
			UpdateCounterText (TotalDirtPieces);
		}
	}

	// TODO: Write the necessary code to modify the Counter
}
