using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FieldOfView : MonoBehaviour
{
    private Vector3 GetVectorFromAngle(float angle)
    {
        float angleRad = angle * (Mathf.PI / 180f);
        return new Vector3(Mathf.Cos(angleRad), Mathf.Sin(angleRad));
    }

    private float GetAngleFromVectorFloat(Vector3 vector)
    {
        vector = vector.normalized;
        float n = Mathf.Atan2(vector.y, vector.x) * Mathf.Rad2Deg;
        if (n < 0)
            n += 360;
        return n;
    }



    [SerializeField] LayerMask layerMask;
    private Mesh mesh;
    private Vector3 origin;
    private float startinAngle;
    float fov;
    float viewDistance;

    private void Start()
    {
        mesh = new Mesh();
        GetComponent<MeshFilter>().mesh = mesh;
       /* viewDistance = 1.5f;
        fov = 90f;*/
    }
    private void Update()
    {
        
        origin = origin;
        int rayCount = 24;
        float angle = startinAngle;
        float angleIncrease = fov / rayCount;
        
        Vector3[] vertices = new Vector3[rayCount + 1 + 1];
        Vector2[] uv = new Vector2[vertices.Length];
        int[] triangles = new int[rayCount * 3];




        vertices[0] = origin;

        int vertexIndex = 1;
        int triangleIndex = 0;
        for(int i = 0; i <= rayCount; i++)
        {
            Vector3 angleVector = GetVectorFromAngle(angle);
            Vector3 vertex; 
            RaycastHit2D raycastHit2D = Physics2D.Raycast(origin, angleVector, viewDistance, layerMask);
            if(raycastHit2D.collider == null)
            {
                vertex = origin + angleVector * viewDistance;
            }
            else
            {
                vertex = raycastHit2D.point;
            }
            vertices[vertexIndex] = vertex;
            if (i > 0)
            {
                triangles[triangleIndex] = 0;
                triangles[triangleIndex + 1] = vertexIndex - 1;
                triangles[triangleIndex + 2] = vertexIndex;

                triangleIndex += 3;
            }
            vertexIndex++;
            angle -= angleIncrease;
        }

        mesh.vertices = vertices;
        mesh.uv = uv;
        mesh.triangles = triangles;

    }

    public void SetOrigin(Vector3 Origin)
    {
        this.origin = Origin;
    }
    public void SetAimDirection(Vector3 aimDirection)
    {
        startinAngle = GetAngleFromVectorFloat(aimDirection) + fov / 2f;
    }

    public void SetFov(float newFov)
    {
        fov = newFov;
    }
    public void SetViewDistance(float newViewDistance)
    {
        viewDistance = newViewDistance;
    }
}
