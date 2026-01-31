using UnityEngine;

public class LevelManager : MonoBehaviour
{
    Vector2 startingPos;
    GameObject player;

    MasksManager masksManager;
    private void Start()
    {
        player = FindObjectOfType<PlayerMovement>().gameObject;
        startingPos = player.transform.position;
        masksManager = FindObjectOfType<MasksManager>();
    }
    public void ResetLevel()
    {
        player.transform.position = startingPos;
        masksManager.IndicateMask(masksManager.startingMask);
        masksManager.SwapMasks();
    }
}
