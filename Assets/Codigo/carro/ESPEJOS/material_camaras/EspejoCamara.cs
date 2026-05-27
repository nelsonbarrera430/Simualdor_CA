using UnityEngine;

[ExecuteInEditMode]
public class EspejoCamara : MonoBehaviour
{
    private Camera cam;

    void OnPreRender()
    {
        cam = GetComponent<Camera>();
        cam.ResetWorldToCameraMatrix();
        cam.ResetProjectionMatrix();
        
        cam.projectionMatrix = cam.projectionMatrix * Matrix4x4.Scale(new Vector3(-1, 1, 1));
    }

    void OnPreCull()
    {
        GL.invertCulling = true;
    }

    void OnPostRender()
    {
        GL.invertCulling = false;
    }
}