using UnityEngine;
using System.Collections;

public class RandomRGBScale : MonoBehaviour
{

	public float randomRate = 1f;

	void Start ()
	{
		StartCoroutine (randomRGB ());
	}

	IEnumerator randomRGB ()
	{
		GetComponent<FreqBand> ().redScale = Random.Range (0, 11) / 10;
		GetComponent<FreqBand> ().greenScale = Random.Range (0, 11) / 10;
		GetComponent<FreqBand> ().blueScale = Random.Range (0, 11) / 10;

		yield return new WaitForSecondsRealtime (randomRate);
		StartCoroutine (randomRGB ());
	}
}
