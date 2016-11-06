using UnityEngine;
using System.Collections;
using System.Collections.Generic;

[RequireComponent(typeof(Renderer))]
public class BlinkMaterialColorHighlight : Highlightable
{

    /// <summary>
    /// The color used for highlighting
    /// </summary>
    [Tooltip("The color used for highlighting. ForceAlpha or a transparent shader is required to use alpha component")]
    public Color HighlightColor;

    /// <summary>
    /// The fade time in second (i.e. half highlight period)
    /// </summary>
    [Tooltip("The fade time in second (i.e. half highlight period)")]
    public float FadeTime;

    /// <summary>
    /// Use a specific material when highlighting. This can be usefull to have an alpha compatible material during highlight
    /// </summary>
    [Tooltip("Use a specific material when highlighting. This can be usefull to have an alpha compatible material during highlight")]
    public Material HighlightFadeMaterial;

    /// <summary>
    /// The start time of the period in seconds
    /// </summary>
    float highlightStartTime = -1;

    /// <summary>
    /// Initial materials
    /// </summary>
    List<Material> initialMaterials = new List<Material>();

    /// <summary>
    /// Current materials to change
    /// </summary>
    Material[] materials;

    public override void Start()
    {
        base.Start();

        // we set time at -1 at start because we can't ensure that every cluster node calls the start method at the exact same time, leading to inconsistency in the blinking 
        highlightStartTime = -1;
    }

    /// <summary>
    /// This method is called each frame while highlighting is enabled
    /// </summary>
    protected override void highlightUpdate()
    {

		float time = Time.time;
        for (int i = 0; i < materials.Length; ++i)
        {
            Color materialColor = initialMaterials[i].color;
            materials[i].color = Color.Lerp(materialColor, HighlightColor, Mathf.PingPong(time-(highlightStartTime>=0? highlightStartTime : 0), FadeTime) / FadeTime);
        }
    }


    /// <summary>
    /// This method is called when highlighting is turned on
    /// </summary>
    protected override void highlightStart()
    {
        materials = GetComponent<Renderer>().materials;
        initialMaterials.Clear();
        for (int i = 0; i < materials.Length; ++i)
        {
            initialMaterials.Add(new Material(materials[i]));
            if (HighlightFadeMaterial != null)
            {
                materials[i] = HighlightFadeMaterial;
            }
        }

        // this is needed so that the new material is taken into account
        if (HighlightFadeMaterial != null)
        {
            GetComponent<Renderer>().materials = materials;
        }
		highlightStartTime = Time.time;
    }

    /// <summary>
    /// This method is called when highlighting is turned off
    /// </summary>
    protected override void highlightStop()
    {
        for (int i = 0; i < materials.Length; ++i)
            materials[i] = initialMaterials[i];

        // this is needed so that the new material is taken into account
        GetComponent<Renderer>().materials = materials;
    }

}
