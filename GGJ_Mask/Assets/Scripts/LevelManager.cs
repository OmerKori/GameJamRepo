using UnityEngine;

public class LevelManager : MonoBehaviour
{
    Vector2 startingPos;
    GameObject player;

    MasksManager masksManager;
    private void Start()
    {
        player = FindFirstObjectByType<PlayerMovement>().gameObject;
        startingPos = player.transform.position;
        masksManager = FindObjectOfType<MasksManager>();

    }
    private void Update()
    {
        if (Input.GetKeyDown(KeyCode.R))
        {
            ResetLevel();
        }
    }
    public void ResetLevel()
    {
        player.transform.position = startingPos;
        if(masksManager.indicatedMaskIndex == 0)
        {
            masksManager.SwapMasks();
        }
        else  if (masksManager.currentMaskIndex != 0)
        {
            masksManager.IndicateMask(0);
            masksManager.SwapMasks();
        }
        else if(masksManager.currentMaskIndex != -1)
        {
            masksManager.RemoveIndicatedMask();
        }

    }

}
