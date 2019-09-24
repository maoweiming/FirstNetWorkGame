using UnityEngine;
using System.Collections;

public class FreqBand : MonoBehaviour
{

	public int bandIndex;
	public float startScale, scaleMutiplier;
	Material material;
	public float redScale = 1f, greenScale = 1f, blueScale = 1f;

	public bool useBuffer;
	public bool useColor;

	void Start ()
	{
		MeshRenderer temp = GetComponent<MeshRenderer> ();

		if (temp == null) {
			material = GetComponentInChildren<MeshRenderer> ().material;
		} else {
			material = temp.material;
		}
	}

	void Update ()
	{
		if (useBuffer) {
			transform.localScale = new Vector3 (transform.localScale.x, (AudioDetector._audioBandBuffer [bandIndex] * scaleMutiplier) + startScale, transform.localScale.z);
			if (useColor) {
				Color color = new Color (AudioDetector._audioBandBuffer [bandIndex] * redScale, AudioDetector._audioBandBuffer [bandIndex] * greenScale, AudioDetector._audioBandBuffer [bandIndex] * blueScale);
				material.SetColor ("_EmissionColor", color);
			}
		} else {
			transform.localScale = new Vector3 (transform.localScale.x, (AudioDetector._audioBand [bandIndex] * scaleMutiplier) + startScale, transform.localScale.z);
			if (useColor) {
				Color color = new Color (AudioDetector._audioBand [bandIndex] * redScale, AudioDetector._audioBand [bandIndex] * greenScale, AudioDetector._audioBand [bandIndex] * blueScale);
				material.SetColor ("_EmissionColor", color);
			}
		}
	}
}
