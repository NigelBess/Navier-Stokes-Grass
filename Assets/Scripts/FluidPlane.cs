using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FluidPlane : MonoBehaviour
{
    [SerializeField] private float pixelWidth = 0.1f;
    [SerializeField] private LayerMask heightMapCullingMask;
    [SerializeField] private Material heightMapShader;
    [SerializeField] private Material navStokesShader;
    [SerializeField] private float maxHeight = 1000f;
    private RenderTexture map;
    private void Awake()
    {
        map = HeightMapGenerator.MakeMap(this, heightMapShader,maxHeight);
        Material selfMat = GetComponent<Renderer>().material;
        selfMat.SetFloat("_xCenter", transform.position.x);
        selfMat.SetFloat("_zCenter", transform.position.z);
        selfMat.SetFloat("_xWidth", GetDimension(0));
        selfMat.SetFloat("_zWidth", GetDimension(2));
        selfMat.SetTexture("_Overlay",map);
    }
    public float GetPixelWidth()
    {
        return pixelWidth;
    }
    public float GetDimension(int index)
    {
        return transform.localScale[index]*10f;//default plane has 10 width/height
    }
    public int GetPixelDimension(int index)
    {
        return Mathf.FloorToInt(GetDimension(index)/pixelWidth);
    }
    public LayerMask GetCullingMask()
    {
        return heightMapCullingMask;
    }
    public float GetZeroHeightDepth()
    {
        return maxHeight - transform.position.y;
    }
}
