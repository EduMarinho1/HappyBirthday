using UnityEngine;
using System.Collections;

public class PushTextMeshToFrontScript : MonoBehaviour 
{
    public string layerToPushTo;

	void Start () 
    {
        layerToPushTo = "Objects";
        GetComponent<Renderer>().sortingLayerName = layerToPushTo;
        //Debug.Log(GetComponent<Renderer>().sortingLayerName);
	}
}