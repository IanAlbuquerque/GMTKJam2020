using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CursorController : MonoBehaviour
{
    [SerializeField] GameObject missilePrefab;
    [SerializeField] GameObject missileLauncherInstance;

    [SerializeField] private Texture2D cursorTexture;
    private Vector2 cursorHotspot;

    private GameController myGameController;

    // Start is called before the first frame update
    void Start()
    {
        myGameController = GameObject.FindObjectOfType<GameController>();

        cursorHotspot = new Vector2(cursorTexture.width / 2f, cursorTexture.height / 2f);
        Cursor.SetCursor(cursorTexture, cursorHotspot, CursorMode.Auto);
    }

    // Update is called once per frame
    void Update()
    {
        if(Input.GetMouseButtonDown(0) && myGameController.currentMissilesLoaded > 0)
        {
            Instantiate(missilePrefab, missileLauncherInstance.transform.position, Quaternion.identity);
            //myGameController.playerMissilesLeft--;
            myGameController.PlayerFiredMissile();
        }
    }
}
