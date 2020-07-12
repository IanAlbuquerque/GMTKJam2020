using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CursorController : MonoBehaviour
{
    [SerializeField] GameObject missilePrefab;
    [SerializeField] GameObject missileLauncherInstance;
    [SerializeField] GameObject missileLauncherInstance2;
    [SerializeField] GameObject missileLauncherInstance3;

    [SerializeField] private Texture2D cursorTexture;
    private Vector2 cursorHotspot;

    private GameController myGameController;
    private PlayerMissileController myMissileControler;

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
        if (Input.GetMouseButtonDown(0) && this.HasEnoughAmmo())
            Fire();
    }

    private void Fire()
    {
        var missileLauncherToUse = this.GetMissileLauncherToUse();
        this.FireWithMissleLauncher(missileLauncherToUse);
    }

    private void FireWithMissleLauncher(GameObject missileLauncher)
    {
        Instantiate(missilePrefab, missileLauncher.transform.position, Quaternion.identity);
        myGameController.PlayerFiredMissile();
    }

    private bool HasEnoughAmmo()
    {
        return myGameController.currentMissilesLoaded > 0;
    }

    private GameObject GetMissileLauncherToUse()
    {
        Vector3 aimPosition = this.GetAimPosition();

        float dist0 = (aimPosition - missileLauncherInstance.transform.position).magnitude;
        float dist1 = (aimPosition - missileLauncherInstance2.transform.position).magnitude;
        float dist2 = (aimPosition - missileLauncherInstance3.transform.position).magnitude;

        if (dist0 < dist1 && dist0 < dist2)
            return missileLauncherInstance;
        else if (dist1 < dist0 && dist1 < dist2)
            return missileLauncherInstance2;
        else
            return missileLauncherInstance3;
    }

    private Vector3 GetAimPosition()
    {
        return Camera.main.ScreenToWorldPoint(Input.mousePosition);
    }
}
