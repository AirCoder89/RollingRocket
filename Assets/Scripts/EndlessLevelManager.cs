using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EndlessLevelManager : MonoBehaviour {
    
    [SerializeField] private List<GameObject> EasyPhases;
    [SerializeField] private List<GameObject> MeduimPhases;
    [SerializeField] private List<GameObject> HardPhases;
    private float DistanceBetweenGeneration = 40f;
    private int PhaseIndex = -1;

    void Start () {
        PhaseIndex = -1;
        EventHandler.onStartGame += FirstPhase;
        EventHandler.onShipDieEvent += resetMe;
        EventHandler.onGeneratePhase += GenerateEasyPhase;
    }

    public void resetMe()
    {
        EventHandler.onShipDieEvent -= resetMe;
        EventHandler.onGeneratePhase -= GenerateEasyPhase;
    }
    public void FirstPhase()
    {
        EventHandler.onStartGame -= FirstPhase;
        StartCoroutine(GenerateFirstPhase());
    }
    IEnumerator GenerateFirstPhase()
    {
        yield return new WaitForSeconds(0.5f);
        GenerateEasyPhase();
    }

    public void GenerateEasyPhase()
    {
       if(EasyPhases.Count > 0)
        {
            print("Generate Easy Phase");
            PhaseIndex = Mathf.RoundToInt(Random.Range(0, EasyPhases.Count));

            Instantiate(EasyPhases[PhaseIndex], new Vector3(ShipController.Instance.GetTransform().position.x + DistanceBetweenGeneration, -6.41f, 0), Quaternion.identity);
            EasyPhases.RemoveAt(PhaseIndex);
        }
        else
        {
            GenerateMeduimPhase();
        }
    }

    private void GenerateMeduimPhase()
    {
        if (MeduimPhases.Count > 0)
        {
            print("Generate Meduim Phase");
            PhaseIndex = Mathf.RoundToInt(Random.Range(0, MeduimPhases.Count));

            Instantiate(MeduimPhases[PhaseIndex], new Vector3(ShipController.Instance.GetTransform().position.x + DistanceBetweenGeneration, -6.41f, 0), Quaternion.identity);
            MeduimPhases.RemoveAt(PhaseIndex);
        }
        else
        {
            GenerateHardPhase();
        }
    }

    private void GenerateHardPhase()
    {
        if (HardPhases.Count > 0)
        {
            print("Generate hard Phase");
            PhaseIndex = Mathf.RoundToInt(Random.Range(0, HardPhases.Count));

            Instantiate(HardPhases[PhaseIndex], new Vector3(ShipController.Instance.GetTransform().position.x + DistanceBetweenGeneration, -6.41f, 0), Quaternion.identity);
            HardPhases.RemoveAt(PhaseIndex);
        }
        else
        {
            EndlessComplete();
        }
    }
    private void EndlessComplete()
    {
        print("ENDLESS LEVEL COMPLETE !!");
    }
	
}
