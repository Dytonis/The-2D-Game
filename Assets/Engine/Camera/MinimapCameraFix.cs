using UnityEngine;
using System.Collections;

public class MinimapCameraFix : MonoBehaviour {

	// Use this for initialization
	void Awake()
    {
        gameObject.GetComponent<Camera>().useOcclusionCulling = true;
        StartCoroutine("wait");
        gameObject.GetComponent<Camera>().useOcclusionCulling = false;
    }

    IEnumerable wait()
    {
        yield return new WaitForEndOfFrame();
    }
}
