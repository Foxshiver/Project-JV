using UnityEngine;
using System.Collections;
using System.Collections.Generic;

[RequireComponent(typeof(Renderer))]
public class ChangeMaterialColorHighlight : Highlightable
{

    /// <summary>
    /// The color used for highlighting
    /// </summary>
    [Tooltip("The color used for highlighting. ForceAlpha or a transparent shader is required to use alpha component")]
    public Color HighlightColor;

    /// <summary>
    /// Use a specific material when highlighting. This can be usefull to have an alpha compatible material during highlight
    /// </summary>
    [Tooltip("Use a specific material when highlighting. This can be usefull to have an alpha compatible material during highlight")]
    public Material HighlightMaterial;


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
    }

    /// <summary>
    /// This method is called each frame while highlighting is enabled
    /// </summary>
    protected override void highlightUpdate()
    {

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
            if (HighlightMaterial != null)
            {
                materials[i] = HighlightMaterial;
            }
            materials[i].color = HighlightColor;
        }

        // this is needed so that the new material is taken into account
        if (HighlightMaterial != null)
        {
            GetComponent<Renderer>().materials = materials;
        }
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
