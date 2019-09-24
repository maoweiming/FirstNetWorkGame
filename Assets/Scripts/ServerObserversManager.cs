using UnityEngine;
using System.Collections;

public interface IObserver
{
	void notify ();
}

public class ServerObserversManager
{
	public static ServerObserversManager _instance;

	private ArrayList observerList = new ArrayList ();

	public ServerObserversManager ()
	{
		if (_instance == null) {
			_instance = this;
		}
	}

	public static ServerObserversManager getInstance ()
	{
		if (_instance == null) {
			new ServerObserversManager ();
		}
		return _instance;
	}

	public void addObserver (IObserver addWhat)
	{
		observerList.Add (addWhat);
	}

	public void notify ()
	{
		foreach (IObserver oneObserver in observerList) {
			oneObserver.notify ();
		}
	}

}
