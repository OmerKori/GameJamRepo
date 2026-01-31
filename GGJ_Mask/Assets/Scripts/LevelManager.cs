using UnityEngine;

public class LevelManager : MonoBehaviour
{
    Vector2 startingPos;
    [SerializeField] GameObject player;

    MasksManager masksManager;
    private void Start()
    {
        startingPos = player.transform.position;
        masksManager = FindObjectOfType<MasksManager>();
    }
    public void ResetLevel()
    {
        player.transform.position = startingPos;
        masksManager.IndicateMask(0);
        masksManager.SwapMasks();
    }
}
