using UnityEngine.UI;
using UnityEngine;
using System.Collections;

public class GestionUserInterface : MonoBehaviour
{
	public Player _player;
    public Farm _alliesFarm;

	public Text _moneyText;
	public Text _nbUnitFollowingText;
    public Text _nbHoldingText;
    public Text _nbWorkingText;
    public Text _nbHealPointRemaining;
    private bool first = true;
	
	// Update is called once per frame
	void Update ()
    {
        int nbHolders = 0;
        for(int i=0; i<_player.listOfHoldPositionUnits.Count; i++)
            for(int j=0; j<_player.listOfHoldPositionUnits[i].Count; j++)
            {
                nbHolders++;
            }
            
		_moneyText.text = "MONEY : " + _player._money;
        _nbUnitFollowingText.text = _player.listOfUnits.Count + " Following Units";
        _nbHoldingText.text = nbHolders + " Holding Units";
        _nbWorkingText.text = _player.listOfWorkerUnits.Count + " Working Units";
        _nbHealPointRemaining.text = _alliesFarm._healPoint + " Heal Points";
    }
}
