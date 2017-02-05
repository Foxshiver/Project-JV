using UnityEngine.UI;
using UnityEngine;
using System.Collections;

public class GestionUserInterface : MonoBehaviour {

	Player _player;

	public Text _moneyText;
	public Text _nbUnitFollowingText;
    public Text _nbHoldingText;
    public Text _nbWorkingText;
    private bool first = true;

	// Use this for initialization
	void Start () {
		//_player = FindObjectOfType<PlayerUnit> ();

	}
	
	// Update is called once per frame
	void Update () {
		if (first) {
			_player = FindObjectOfType<Player> ();
			first = false;
		}

		_moneyText.text = "MONEY : " + _player._money;
        _nbUnitFollowingText.text = _player.listOfUnits.Count + " Following Units";
        _nbHoldingText.text = _player.listOfHoldPositionUnits.Count + " Holding Units";
        _nbWorkingText.text = _player.listOfWorkerUnits.Count + " Working Units";

    }
}
