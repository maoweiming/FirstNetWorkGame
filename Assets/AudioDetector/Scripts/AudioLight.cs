using UnityEngine;
using System.Collections;

[RequireComponent (typeof(Light))]
public class AudioLight : MonoBehaviour
{

	public int bandIndex;
	public float minIntensity, maxIntensity;
	Light _light;
	Color curColor;

	[Range (0, 1)]
	public float redScale = 1f, greenScale = 1f, blueScale = 1f;

	void Start ()
	{
		_light = GetComponent<Light> ();
	}

	void Update ()
	{
//		if (AudioDetector._audioBandBuffer [bandIndex] == AudioDetector._audioBandBuffer [bandIndex]) {
		if(AudioDetector.isOn){
			_light.intensity = (AudioDetector._audioBandBuffer [bandIndex] * (maxIntensity - minIntensity)) + minIntensity;
			curColor = new Color (AudioDetector._audioBandBuffer [bandIndex] * redScale, AudioDetector._audioBandBuffer [bandIndex] * greenScale, AudioDetector._audioBandBuffer [bandIndex] * blueScale);

		} else {
			_light.intensity = minIntensity;
			curColor = new Color (redScale, greenScale, blueScale);
		}

		_light.color = (curColor);
	}
}
