using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System.Text;
using System.IO;

public class ApplyForcesSyncope : MonoBehaviour {
    [SerializeField] private Rigidbody[] bodiesToApplyForce;
    [SerializeField] private Vector3[] forcesDirs;
    [SerializeField] private float[] forces;
    [SerializeField] private float timeToApply = 0.4f;
    [SerializeField] private ForceMode forceToUse;
    [SerializeField] private AnimationCurve[] forceCurves;

    [SerializeField] private Collider[] collidersToDeactivate;

    [SerializeField] private Vector2 minMaxFactorForce = new Vector2(0.9f, 1.1f);
    [SerializeField] private Vector2 minMaxFactorDir = new Vector2(0.8f, 1.2f);

    public bool Debugging;
    public bool useFixed = true;
    public bool deactivateColliders;

    public int fallID;

    private float[] currForces;
    private Vector3[] currDirs;

    private bool isApplying;
    private float eTime;

    // Add a reference to the tile grid
    public gridinstatiator tileGrid;

    // For CSV Export
    private StringBuilder csv = new StringBuilder();

    void Start() {
        GameManager.Instance.OnActivateRagdoll += StartApplying;
        currForces = new float[forces.Length];
        currDirs = new Vector3[forcesDirs.Length];

        // Initialize CSV with dynamic column names
        string[] columnNames = new string[tileGrid.rows * tileGrid.cols];
        for (int i = 0; i < columnNames.Length; i++)
            columnNames[i] = "s" + i;

        csv.AppendLine("fallID," + string.Join(",", columnNames) + ",class");
    }

    void Update() {
        // Removed unnecessary parts
    }

    private void FixedUpdate() {
        if (!isApplying) return;
        if (!useFixed) return;
        eTime += Time.fixedDeltaTime;
        ApplyForce();
        if (eTime > timeToApply) {
            isApplying = false;
            if (deactivateColliders) {
                for (int i = 0; i < collidersToDeactivate.Length; i++) {
                    collidersToDeactivate[i].enabled = true;
                }
            }
        }
    }

    private void StartApplying(bool applyForces) {
         StartCoroutine(RecordSensorDataCoroutine());
        // ...
        // Removed unnecessary parts
        // ...
        /*
        for (int i = 0; i < forces.Length; i++) {
            // ...

           // Collect sensor data from the tiles
            List<string> sensorData = new List<string>();
            foreach (var tile in tileGrid.tiles)
            {
                int tileForce = tile.GetSensorData();
                sensorData.Add(tileForce.ToString());
            }

            // CSV Data Collection
            csv.AppendLine($"{fallID},{string.Join(",", sensorData)},{1}");
            fallID++;
        }*/
    }
    
    IEnumerator RecordSensorDataCoroutine()
    {
        // This will be the duration of your fall
        float fallDuration = 2.0f;
        float timer = 0;

        while (timer < fallDuration)
        {
            // Collect sensor data from the tiles
            List<string> sensorData = new List<string>();
            foreach (var tile in tileGrid.tiles)
            {
                int tileForce = tile.GetSensorData();
                sensorData.Add(tileForce.ToString());
            }

            // CSV Data Collection
            csv.AppendLine($"{fallID},{string.Join(",", sensorData)},{1}");

            // Wait for the next sample
            yield return new WaitForSeconds(0.01f);  // 10ms intervals
            timer += 0.01f;
        }

        fallID++;
    }

    private void ApplyForce() {
        // Removed unnecessary parts
    }

    private void OnApplicationQuit() {
        File.WriteAllText(Application.dataPath + "/CSV/fallData.csv", csv.ToString());
    }
}
