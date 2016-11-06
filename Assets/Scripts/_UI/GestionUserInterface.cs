using UnityEngine.UI;
using UnityEngine;
using System.Collections;

public class GestionUserInterface : MonoBehaviour {

	public PlayerUnit _player;

	public Text _moneyText;
	public Text _nbUnitText;
	private bool first = true;

	// Use this for initialization
	void Start () {
		//_player = FindObjectOfType<PlayerUnit> ();

	}
	
	// Update is called once per frame
	void Update () {
		if (first) {
			_player = FindObjectOfType<PlayerUnit> ();
			first = false;
		}

		_moneyText.text = "MONEY : " + _player._money;
		_nbUnitText.text = "nbUNITS : " + _player.listOfUnits.Count;
	
	}
}
