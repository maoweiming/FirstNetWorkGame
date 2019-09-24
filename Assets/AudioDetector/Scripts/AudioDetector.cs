using UnityEngine;
using System.Collections;

[RequireComponent (typeof(AudioSource))]
public class AudioDetector : MonoBehaviour
{

	AudioSource _audioSource;
	public static bool isOn;

	public static float[] _samples = new float[512];
	float[] _freqBand = new float[8];
	float[] _bandBuffer = new float[8];
	float[] _bufferDerease = new float[8];


	float[] _freqBandHighest = new float[8];
	public static float[] _audioBand = new float[8];
	public static float[] _audioBandBuffer = new float[8];

	void Start ()
	{
		_audioSource = GetComponent<AudioSource> ();
	}

	void Update ()
	{
		if (_audioSource.isPlaying) {
			isOn = true;

			GetSpectrumAudioSource ();
			GetFreqBand ();
			GetBandBuffer ();

			CreateAudioBands ();

		} else
			isOn = false;
	}

	void GetSpectrumAudioSource ()
	{
		_audioSource.GetSpectrumData (_samples, 0, FFTWindow.Blackman);
	}

	void GetFreqBand ()
	{
		int count = 0;

		for (int i = 0; i < 8; i++) {

			float average = 0;
			int sampleCount = (int)Mathf.Pow (2, i) * 2;

			if (i == 7) {
				sampleCount += 2;
			}

			for (int j = 0; j < sampleCount; j++) {
				average += _samples [count] * (count + 1);
				count++;
			}

			average /= count;

			_freqBand [i] = average * 10;

		}
	}

	void GetBandBuffer ()
	{

		for (int i = 0; i < 8; i++) {

			if (_freqBand [i] > _bandBuffer [i]) {
				
				_bandBuffer [i] = _freqBand [i];
				_bufferDerease [i] = 0.005f;

			} else if (_freqBand [i] < _bandBuffer [i]) {

				_bandBuffer [i] -= _bufferDerease [i];
				_bufferDerease [i] *= 1.2f;

			}

		}

	}

	void CreateAudioBands ()
	{

		for (int i = 0; i < 8; i++) {
			if (_freqBand [i] > _freqBandHighest [i]) {
				_freqBandHighest [i] = _freqBand [i];
			}

			_audioBand [i] = _freqBand [i] / _freqBandHighest [i];
			_audioBandBuffer [i] = _bandBuffer [i] / _freqBandHighest [i];
		}

	}
}
