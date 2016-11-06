using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public abstract class Highlightable : MonoBehaviour
{
    /// <summary>
    /// Toggle highlighting on at start
    /// </summary>
    [Tooltip("Toggle highlighting on at start")]
    public bool OnAtStart = false;

    /// <summary>
    /// is highlighting enabled ?
    /// </summary>
    bool highlightEnabled = false;

    public virtual void Start()
    {
        if (OnAtStart)
            Highlight(true);
    }


    // Update is called once per frame
    void Update()
    {
        if (highlightEnabled)
        {
            highlightUpdate();
        }
    }

    /// <summary>
    /// Toggle highlighting state (on if value = true, off otherwise)
    /// </summary>
    /// <param name="value">on if value = true, off otherwise</param>
    public void Highlight(bool value)
    {
        // starting highlight
        if (!highlightEnabled && value)
        {
            highlightEnabled = true;
            highlightStart();
        }
        if (highlightEnabled && !value)
        {
            highlightEnabled = false;
            highlightStop();
        }
    }

    /// <summary>
    /// Toggle highlighting state (on if value = true, off otherwise) for the given object
    /// </summary>
    /// <param name="obj">The parent object</param>
    /// <param name="value">on if value = true, off otherwise</param>
    public static void Highlight(GameObject obj, bool value)
    {
        Highlightable[] hList = obj.GetComponents<Highlightable>();

        foreach (Highlightable h in hList)
        {
            h.Highlight(value);
        }
    }

    /// <summary>
    /// Toggle highlighting state (on if value = true, off otherwise) for the given object and all its children
    /// </summary>
    /// <param name="obj">The parent object</param>
    /// <param name="value">on if value = true, off otherwise</param>
    public static void HighlightChildren(GameObject obj, bool value)
    {
        Highlightable[] hList = obj.GetComponentsInChildren<Highlightable>();

        foreach (Highlightable h in hList)
        {
            h.Highlight(value);
        }
    }

    /// <summary>
    /// Change highlighting state (toggle on if it was off and vice versa)
    /// </summary>
    [ContextMenu("SwitchHighlight")]
    public void SwitchHighlight()
    {
        Highlight(!highlightEnabled);
    }

    /// <summary>
    /// This method is called each frame while highlighting is enabled
    /// </summary>
    protected abstract void highlightUpdate();

    /// <summary>
    /// This method is called when highlighting is turned on
    /// </summary>
    protected abstract void highlightStart();

    /// <summary>
    /// This method is called when highlighting is turned off
    /// </summary>
    protected abstract void highlightStop();
}
