using System.Collections.Generic;
using UnityEngine;

public class LevelGenerator : MonoBehaviour
{
  [SerializeField] private List<Plane> _planes;
  [SerializeField] private List<Transform> _obstacles;
  [SerializeField] private Transform _target;

  private float _newPosition;

  private void Start() 
  {
    SetNewPositionPlane();
  }

  private void Update() 
  {
    SetNewPositionPlane();
  }

  private void SetNewPositionPlane()
  {
    foreach(Plane plane in _planes)
    {
      if(_target.transform.position.z > plane.transform.position.z + plane.planeStep)
      {
        plane.transform.position = plane.transform.forward * _newPosition;
        _newPosition += plane.planeStep;

        SetNewPositionObstacle(plane);
      }
    }
  }

  private void SetNewPositionObstacle(Plane plane)
  {   
      foreach(Transform obstacle in plane.currentObstacles)
      {
        _obstacles.Add(obstacle);
      }

      plane.currentObstacles.Clear();

      int countPoint = Random.Range(0, plane.obstaclePoint.Length+1);

      for(int i = 0; i < countPoint; i ++)
      {
        int index = Random.Range(i, _obstacles.Count);
        _obstacles[index].transform.position = new Vector3(_obstacles[index].transform.position.x, _obstacles[index].transform.position.y, plane.obstaclePoint[i].position.z);

        plane.currentObstacles.Add(_obstacles[index]);
        _obstacles.Remove(_obstacles[index]); 
      }
  }  
}
