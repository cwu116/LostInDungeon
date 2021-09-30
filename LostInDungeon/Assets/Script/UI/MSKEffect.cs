using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MSKEffect : MonoBehaviour
{

    public Material mat;

    [Range(1,100)]
    public int TileSize = 1;
    // Start is called before the first frame update

    // Update is called once per frame
    void Update()
    {
        if (mat)
            mat.SetInt("_TileSize", TileSize); 
    }

    private void OnRenderImage(RenderTexture source, RenderTexture destination)
    {
        if(mat) 
        Graphics.Blit(source, destination, mat);
    }
}
