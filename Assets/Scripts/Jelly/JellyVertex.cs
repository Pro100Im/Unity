using UnityEngine;

public class JellyVertex 
{
   public int verticeIndex;
   public Vector3 initialVertexPosition;
   public Vector3 currentVertexPosition;
   public Vector3 currentVelosity;

    public JellyVertex(int verticeIndex, Vector3 initialVertexPosition, Vector3 currentVertexPosition, Vector3 currentVelosity)
    {
        this.verticeIndex = verticeIndex;
        this.initialVertexPosition = initialVertexPosition;
        this.currentVertexPosition = currentVertexPosition;
        this.currentVelosity = currentVelosity;
    }

    public Vector3 GetCurrentDisplacement()
    {
        return currentVertexPosition - initialVertexPosition;
    }

    public void UpdateVelocity(float bounceSpeed)
    {
        currentVelosity = currentVelosity - GetCurrentDisplacement() * bounceSpeed * Time.deltaTime;
    }

    public void Settle(float stiffness)
    {
        currentVelosity *= 1f - stiffness * Time.deltaTime;
    }

    public void ApplyPressureToVertex(Transform tr, Vector3 position, float pressure)
    {
        Vector3 distanceVerticePoint = currentVertexPosition - tr.InverseTransformPoint(position);
        float adaptedPressure = pressure / (1f + distanceVerticePoint.sqrMagnitude);
        float velocity = adaptedPressure * Time.deltaTime;
        currentVelosity += distanceVerticePoint.normalized * velocity;
    }

}
