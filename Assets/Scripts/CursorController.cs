using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CursorController : MonoBehaviour
{
    [SerializeField] GameObject missilePrefab;
    [SerializeField] GameObject missileLauncherInstance;
    [SerializeField] GameObject missileLauncherInstance2;
    [SerializeField] GameObject missileLauncherInstance3;

    private DestroyController _destroyController1;
    private DestroyController _destroyController2;
    private DestroyController _destroyController3;

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

        this._destroyController1 = this.missileLauncherInstance.GetComponent<DestroyController>();
        this._destroyController2 = this.missileLauncherInstance2.GetComponent<DestroyController>();
        this._destroyController3 = this.missileLauncherInstance3.GetComponent<DestroyController>();
    }

    // Update is called once per frame
    void Update()
    {
        if (Input.GetMouseButtonDown(0) && this.HasEnoughAmmo() && !GameController.IsPaused)
            Fire();
    }

    private void Fire()
    {
        if (this._destroyController1.IsDestroyed && this._destroyController2.IsDestroyed &&
            this._destroyController3.IsDestroyed)
            return;
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

        if (this._destroyController1.IsDestroyed)
            dist0 = Mathf.Infinity;
        if (this._destroyController2.IsDestroyed)
            dist1 = Mathf.Infinity;
        if (this._destroyController3.IsDestroyed)
            dist2 = Mathf.Infinity;

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
