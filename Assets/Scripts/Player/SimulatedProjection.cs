using UnityEngine.SceneManagement;
using UnityEngine;

public class SimulatedProjection : MonoBehaviour
{
	private Scene simulationScene;
	private PhysicsScene physicsScene;

	[SerializeField] private Transform projectileSpawnPoint;
	[SerializeField] private LineRenderer trajectoryLine;
	[SerializeField] private Transform obstacleWallParent;
	[SerializeField] private int maxPhysicsFrameIterations = 100;

	private void Start()
	{
		if(obstacleWallParent != null)
		{
			CreatePhysicsScene();
		}
		else
		{
			CreatePhysicsScene();
		}
	}

	private void CreatePhysicsScene()
	{
		simulationScene = SceneManager.CreateScene("Simulation", new CreateSceneParameters(LocalPhysicsMode.Physics3D));
		physicsScene = simulationScene.GetPhysicsScene();

		foreach(Transform obj in obstacleWallParent) 
		{
			var simulatedObj = Instantiate(obj.gameObject, obj.position, obj.rotation);
			simulatedObj.GetComponent<Renderer>().enabled = false;
			SceneManager.MoveGameObjectToScene(simulatedObj, simulationScene);
		}
	}

	public void SimulateTrajectory(Projectile projectilePrefab, Vector3 spawnPos, Vector3 velocity)
	{
		var simulatedObj = Instantiate(projectilePrefab, spawnPos, Quaternion.identity);
		SceneManager.MoveGameObjectToScene(simulatedObj.gameObject, simulationScene);

		simulatedObj.Init(velocity, true);

		trajectoryLine.positionCount = maxPhysicsFrameIterations;

		for(int i = 0; i < maxPhysicsFrameIterations; i++) 
		{
			//Simulating maxPhysicsFrameIterations into the future
			physicsScene.Simulate(Time.fixedDeltaTime);
			trajectoryLine.SetPosition(i, simulatedObj.transform.position);
		}

		Destroy(simulatedObj.gameObject);
	}

	public void ResetSimulationAndTrajectory()
	{
		//Clearing the line renderer
		trajectoryLine.positionCount = 0;
	}
}
