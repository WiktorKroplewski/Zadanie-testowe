using UnityEngine;

public class ShaderData : MonoBehaviour
{
    private void Update()
    {
        Shader.SetGlobalVector("_GlobalPlayerPos", transform.position);
    }
}
