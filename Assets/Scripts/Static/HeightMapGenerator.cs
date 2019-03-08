using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public static class HeightMapGenerator
{

    public static RenderTexture MakeMap(FluidPlane fp, Material heightMapMat, float maxHeight)
    {

        Camera cam = SetupCamera(fp,maxHeight);
        int width = Mathf.CeilToInt(cam.rect.width);
        int height = Mathf.CeilToInt(cam.rect.height);
        RenderTexture depthTexture = new RenderTexture(width,height,24,RenderTextureFormat.Depth);
        RenderTexture dummy = new RenderTexture(width,height,0);
        cam.SetTargetBuffers(dummy.colorBuffer, depthTexture.depthBuffer);
        cam.forceIntoRenderTexture = true;
        cam.Render();
        
        RenderTexture outVar = new RenderTexture(dummy);
        outVar.enableRandomWrite = true;
        outVar.Create();
        heightMapMat.SetTexture("_DepthTexture",depthTexture);
        Graphics.Blit(dummy, outVar,heightMapMat);
        MonoBehaviour.DestroyImmediate(cam.gameObject);
        return outVar;
    }
    private static Camera SetupCamera(FluidPlane plane,float maxHeight)
    {
        GameObject obj = new GameObject();
        Camera cam = obj.AddComponent<Camera>();
        cam.cullingMask = plane.GetCullingMask();
        cam.orthographic = true;
        cam.depthTextureMode = DepthTextureMode.Depth;
        cam.transform.rotation = Quaternion.Euler(90, 0, 0);
        cam.transform.position= new Vector3(plane.transform.position.x,plane.transform.position.y+maxHeight,plane.transform.position.z);
        cam.farClipPlane = maxHeight+10;
        cam.nearClipPlane = 1f;
        int width = plane.GetPixelDimension(0);
        int height = plane.GetPixelDimension(2);
        cam.aspect = (float)width / (float)height;
        cam.orthographicSize = plane.GetDimension(2) / 2;
        cam.rect = new Rect(0,0,(float)width,(float)height);
        return cam;
    }
    //private void OnRenderImage(RenderTexture source, RenderTexture destination)
    //{
    //    Debug.Log("rendered!");
    //    Graphics.Blit(source, destination, heightmapMaterial);
    //    cam.enabled = false;
        
    //}
    
}
