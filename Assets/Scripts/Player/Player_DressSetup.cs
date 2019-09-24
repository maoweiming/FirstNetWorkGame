using UnityEngine;
using UnityEngine.Networking;
using System.Collections;

public class Player_DressSetup : NetworkBehaviour,IObserver
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

	[SyncVar]private int hatIndexServerSide = 0;
	[SyncVar]private int glassesIndexServerSide = 0;
	[SyncVar]private int clothesIndexServerSide = 0;

	void Start ()
	{
		hatIndex = PlayerPrefs.GetInt ("hatIndex");
		glassesIndex = PlayerPrefs.GetInt ("glassesIndex");
		clothesIndex = PlayerPrefs.GetInt ("clothesIndex");

		if (isLocalPlayer) {
			CmdSend2Server (hatIndex, glassesIndex, clothesIndex);
		}
	}

	IEnumerator doDress ()
	{
		yield return new WaitForSeconds (1f);
		CmdNotifyObserver ();
//		CmdUpdateDress ();
	}

	public override void OnStartLocalPlayer ()
	{
		CmdUpdateDress ();

		CmdAddObserver ();

		StartCoroutine (doDress ());
	}

	[Command]
	void CmdAddObserver ()
	{
		ServerObserversManager.getInstance ().addObserver (this);
	}

	[Command]
	void CmdNotifyObserver ()
	{
		ServerObserversManager.getInstance ().notify ();
	}

	public void notify ()
	{
		CmdUpdateDress ();
	}

	[Command]
	void CmdSend2Server (int hatIndex, int glassesIndex, int clothesIndex)
	{
		hatIndexServerSide = hatIndex;
		glassesIndexServerSide = glassesIndex;
		clothesIndexServerSide = clothesIndex;
	}

	[Command]
	void CmdUpdateDress ()
	{
		RpcUpdateDress ();
	}

	[ClientRpc]
	void RpcUpdateDress ()
	{
		playerHat.mesh = hatMeshArr [hatIndexServerSide];
		playerGlasses.mesh = glassesMeshArr [glassesIndexServerSide];
		playerClothes.mesh = clothesMeshArr [clothesIndexServerSide];
	}
}
