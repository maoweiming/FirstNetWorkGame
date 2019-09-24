using UnityEngine;
using UnityEngine.UI;
using System.Collections;

public class WelcomeTip : MonoBehaviour
{

	public Text outlineText;
	public Text contentText;

	public void updateToolTip(string text){
		outlineText.text = text;
		contentText.text = text;
	}

}
