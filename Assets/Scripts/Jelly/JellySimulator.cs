using UnityEngine;

public class JellySimulator : MonoBehaviour
{
    [SerializeField] private float _bounceSpeed;
    [SerializeField] private float _fallForce;
    [SerializeField] private float _stiffness;

    private MeshFilter _meshFilter;
    private Mesh _mesh;
    private JellyVertex[] _jellyVertices;
    private Vector3[] _currentMeshVertices;

    
    private void Start()
    {
        _meshFilter = GetComponent<MeshFilter>();
        _mesh = _meshFilter.mesh;

        GetVertices();
    }

    private void GetVertices()
    {
        _jellyVertices = new JellyVertex[_mesh.vertices.Length];
        _currentMeshVertices = new Vector3[_mesh.vertices.Length];

        for(int i = 0; i < _mesh.vertices.Length; i++)
        {
            _jellyVertices[i] = new JellyVertex(i, _mesh.vertices[i], _mesh.vertices[i], Vector3.zero);
            _currentMeshVertices[i] = _mesh.vertices[i];
        }
    }

    private void Update() 
    {
        UpdateVartices();
    }

    private void UpdateVartices()
    {
        for(int i = 0; i < _jellyVertices.Length; i++)
        {
            _jellyVertices[i].UpdateVelocity(_bounceSpeed);
            _jellyVertices[i].Settle(_stiffness);

            _jellyVertices[i].currentVertexPosition += _jellyVertices[i].currentVelosity * Time.deltaTime;
            _currentMeshVertices[i] = _jellyVertices[i].currentVertexPosition;
        }

        _mesh.RecalculateBounds();
        _mesh.RecalculateNormals();
        _mesh.RecalculateTangents();
    }

    public void OnCollisionEnter(Collision other) 
    {
        Debug.Log("Work");
        ContactPoint[] collisionPoints = other.contacts;

        for(int i = 0; i < collisionPoints.Length; i++)
        {
            Vector3 inputPoint = collisionPoints[i].point;
            ApplyPressureToPoint(inputPoint, _fallForce);
        }
    }

    private void ApplyPressureToPoint(Vector3 point, float pressure)
    {
        for(int i = 0; i < _jellyVertices.Length; i ++)
        {
            _jellyVertices[i].ApplyPressureToVertex(transform, point, pressure);
        }
    }
}
