using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Background : MonoBehaviour
{
    // Start is called before the first frame update

    private List<Parallax> backgroundLayers;

    void Start()
    {
        backgroundLayers = new List<Parallax>();
        foreach (Transform child in transform)
        {
            backgroundLayers.Add(child.GetComponent<Parallax>());
        }
    }

    // Update is called once per frame
    void Update()
    {

    }

    public void StartParallax()
    {
        foreach(Parallax layer in backgroundLayers)
        {
            layer.StartParallax();
        }
    }
    public void StopParallax()
    {
        foreach(Parallax layer in backgroundLayers)
        {
            layer.StopParallax();
        }
    }
}
