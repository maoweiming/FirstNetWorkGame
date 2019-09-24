using UnityEngine;
using UnityEngine.UI;
using System.Collections;
using UnityEngine.SceneManagement;

public class MenuManager : MonoBehaviour
{

	public MeshFilter playerHat;
	public MeshFilter playerGlasses;
	public MeshFilter playerClothes;

	public Mesh[] hatMeshArr;
	public Mesh[] glassesMeshArr;
	public Mesh[] clothesMeshArr;

	private int hatIndex = 0;
	private int glassesIndex = 0;
	private int clothesIndex = 0;

	public void OnHatButtonClicked ()
	{
		hatIndex++;
		hatIndex %= hatMeshArr.Length;
		playerHat.mesh = hatMeshArr [hatIndex];
	}

	public void OnGlassesButtonClicked ()
	{
		glassesIndex++;
		glassesIndex %= glassesMeshArr.Length;
		playerGlasses.mesh = glassesMeshArr [glassesIndex];
	}

	public void OnClothesButtonClicked ()
	{
		clothesIndex++;
		clothesIndex %= clothesMeshArr.Length;
		playerClothes.mesh = clothesMeshArr [clothesIndex];
	}

	void Start ()
	{
		hatIndex = PlayerPrefs.GetInt ("hatIndex");
		glassesIndex = PlayerPrefs.GetInt ("glassesIndex");
		clothesIndex = PlayerPrefs.GetInt ("clothesIndex");
		playerHat.mesh = hatMeshArr [hatIndex];
		playerGlasses.mesh = glassesMeshArr [glassesIndex];
		playerClothes.mesh = clothesMeshArr [clothesIndex];


		playerName = PlayerPrefs.GetString ("playerName");
		if (playerName == "") {//!PlayerPrefs.HasKey ("playerName")) {
			print ("A");
			playerName = "Customer";
			PlayerPrefs.SetString ("playerName", playerName);
		} 

		foreach (WelcomeTip oneTip in playerWelcomeTip) {
			oneTip.updateToolTip ("Hello " + playerName);
		}
	}

	public InputField playerNameField;
	public WelcomeTip[] playerWelcomeTip;
	private string playerName;

	public void OnNameChanged ()
	{
		if (playerNameField.text != null)
			playerName = playerNameField.text;
	}

	public void savePrefabs ()
	{
		PlayerPrefs.SetInt ("hatIndex", hatIndex);
		PlayerPrefs.SetInt ("glassesIndex", glassesIndex);
		PlayerPrefs.SetInt ("clothesIndex", clothesIndex);
		PlayerPrefs.SetString ("playerName", playerName);

		foreach (WelcomeTip oneTip in playerWelcomeTip) {
			oneTip.updateToolTip ("Hello " + playerName);
		}
	}

	public void cancleSelect ()
	{
		hatIndex = PlayerPrefs.GetInt ("hatIndex");
		glassesIndex = PlayerPrefs.GetInt ("glassesIndex");
		clothesIndex = PlayerPrefs.GetInt ("clothesIndex");
		playerHat.mesh = hatMeshArr [hatIndex];
		playerGlasses.mesh = glassesMeshArr [glassesIndex];
		playerClothes.mesh = clothesMeshArr [clothesIndex];

		playerName = PlayerPrefs.GetString ("playerName");
		foreach (WelcomeTip oneTip in playerWelcomeTip) {
			oneTip.updateToolTip ("Hello " + playerName);
		}
	}

	public void OnStartButtonClicked ()
	{
		SceneManager.LoadScene ("Main");
	}

	public void OnQuitButtonClicked ()
	{
		Application.Quit ();
	}

}
