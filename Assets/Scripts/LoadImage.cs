using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LoadImage : MonoBehaviour {
    public string url = "http://images.earthcam.com/ec_metros/ourcams/fridays.jpg";
    
    IEnumerator Start()
    {
        // Start a download of the given URL
        using (WWW www = new WWW(url))
        {
            // Wait for download to complete
            print("Loading Image");
            yield return www;
            print("Done loading Image");
            // assign texture
            Renderer renderer = this.gameObject.GetComponent<Renderer>();
            renderer.material.mainTexture = www.texture;
            
        }
    }
    // Update is called once per frame
    void Update () {
		
	}
}
