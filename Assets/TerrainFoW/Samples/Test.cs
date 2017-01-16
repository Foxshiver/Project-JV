/*--------------------------------------------------------------*/
//Test
//Created by Rafael Batista
//Sample
/*--------------------------------------------------------------*/
using UnityEngine;
using System.Collections;

public class Test : MonoBehaviour
{
	public static bool OverGUI = false;
	public static Cor CurrentColor = Cor.RED;

    public Transform MyCharacter;
	
	void Update ()
	{

        TerrainFoW.Current.ExploreArea(MyCharacter.position, TerrainFoW.Current.ExplorationSize);

        //if (!Test.OverGUI) {
        //	if (Input.GetMouseButtonDown (0)) {
        //		TerrainFoW.Current.ExploreArea (Input.mousePosition, TerrainFoW.Current.ExplorationSize);
        //	}
        //	if (Input.GetMouseButtonDown (1)) {
        //		TerrainFoW.Current.PaintTerrain (Input.mousePosition, TerrainFoW.Current.ExplorationSize, CurrentColor);
        //	}
        //}
    }
}
