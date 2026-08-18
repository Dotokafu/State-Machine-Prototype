using Unity.Cinemachine;
using UnityEngine;


public class GameManager : MonoBehaviour
{
    [SerializeField] private Transform respawnPoint;
    [SerializeField] private GameObject player;
    [SerializeField] private float respawnTime;

    private CinemachineCamera CVC;

    private float respawnTimeStart;

    private bool respawn;

    private void Start()
    {
        CVC = GameObject.Find("CinemachineCamera").GetComponent<CinemachineCamera>();
    }
    private void Update()
    {
        CheckRespawn();
    }

    public void Respawn()
    {
        respawnTimeStart = Time.time;
        respawn = true;


    }

    private void CheckRespawn()
    {
        if (Time.time > respawnTimeStart + respawnTime && respawn)
        {
            var playerTemp =Instantiate(player, respawnPoint);
            CVC.Follow = playerTemp.transform;
            respawn = false;   
        }
    }
}
