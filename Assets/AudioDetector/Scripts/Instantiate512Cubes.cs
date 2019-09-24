using UnityEngine;
using System.Collections;

public class Instantiate512Cubes : MonoBehaviour
{

	public GameObject cubePrefab;
	GameObject[] cubeArr = new GameObject[512];
	Material[] materialArr = new Material[512];

	public float cubeRange = 100f;
	public float maxScale;
	public bool useColor;
	public float redScale = 1f, greenScale = 1f, blueScale = 1f;

	void Start ()
	{
	
		for (int i = 0; i < 512; i++) {
			GameObject oneCube = Instantiate (cubePrefab) as GameObject;
			oneCube.transform.position = this.transform.position;
			oneCube.transform.parent = this.transform;
			oneCube.name = "Cube" + i;
			this.transform.eulerAngles = new Vector3 (0, -0.703125f * i, 0);
			oneCube.transform.position = Vector3.forward * cubeRange;
			cubeArr [i] = oneCube;
			materialArr [i] = oneCube.GetComponent<MeshRenderer> ().material;
		}

	}

	public bool useScale = false;
	public float preScale = 50f;

	void Update ()
	{
		for (int i = 0; i < 256; i++) {
			if (cubeArr [i] != null) {
				if (useScale) {
					cubeArr [i].transform.localScale = new Vector3 (1, AudioDetector._samples [i] * maxScale, 1);
				} else {
					cubeArr [i].transform.localScale = new Vector3 (1, preScale, 1);
				}

				if (useColor) {
					Color color = new Color (AudioDetector._samples [i] * redScale, AudioDetector._samples [i] * greenScale, AudioDetector._samples [i] * blueScale);
					materialArr [i].SetColor ("_EmissionColor", color);
				}
			}
		}
		for (int i = 511; i > 256; i--) {
			if (cubeArr [i] != null) {
				if (useScale) {
					cubeArr [i].transform.localScale = new Vector3 (1, AudioDetector._samples [511 - i] * maxScale, 1);
				} else {
					cubeArr [i].transform.localScale = new Vector3 (1, preScale, 1);
				}

				if (useColor) {
					Color color = new Color (AudioDetector._samples [511 - i] * redScale, AudioDetector._samples [511 - i] * greenScale, AudioDetector._samples [511 - i] * blueScale);
					materialArr [i].SetColor ("_EmissionColor", color);
				}
			}
		}
	}
}
