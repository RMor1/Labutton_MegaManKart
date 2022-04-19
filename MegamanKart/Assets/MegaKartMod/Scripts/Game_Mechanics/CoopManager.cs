using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using Cinemachine;

public class CoopManager : MonoBehaviour
{
    public static CoopManager Instance;
    [SerializeField] private GameObject[] Camera;
    [SerializeField] private GameObject SpawnpointP1, SpawnpointP2, SpawnpointP3, SpawnpointP4;
    [SerializeField] private GameObject PrefabKartPadrao, PrefabKartBulky, PrefabKartBoost, PrefabKartSpeed;
    [SerializeField] private Transform PwUp_TopLeft, PwUp_TopRight, PwUp_BottomLeft, PwUp_BottomRight;
    [System.NonSerialized] public List<KartController> karts = new List<KartController>();
    [SerializeField] private Transform PlaceInRace_TopLeft, PlaceInRace_TopRight, PlaceInRace_BottomLeft, PlaceInRace_BottomRight;

    public GameObject checkPointParent;
    [System.NonSerialized] public CheckPoint[] checkPoints;
    private void Awake()
    {
        Instance = this;
    }
    private void Start()
    {
        checkPoints = new CheckPoint[checkPointParent.transform.childCount];
        for(int i = 0; i < checkPointParent.transform.childCount; i++)
        {
            checkPoints[i] = checkPointParent.transform.GetChild(i).GetComponent<CheckPoint>();
        }
        for(int i = 1; i < checkPoints.Length; i++)
        {
            for(int j = 0; j < checkPoints[i].transform.childCount; j++)
            {
                checkPoints[i].transform.GetChild(j).gameObject.SetActive(false);
            }
        }
        for (int i = 3; i >= SelectScreenConfigs.Instance.playerAmmount; i--)
        {
            Camera[i].SetActive(false);
        }
        switch (SelectScreenConfigs.Instance.playerAmmount)
        {
            case 1:
                Camera[0].transform.Find("CameraPlayer").GetComponent<Camera>().rect = new Rect(0, 0, 1, 1);
                break;
            case 2:
                Camera[0].transform.Find("CameraPlayer").GetComponent<Camera>().rect = new Rect(0, 0.5f, 1, 1);
                Camera[1].transform.Find("CameraPlayer").GetComponent<Camera>().rect = new Rect(0, -0.5f, 1, 1);
                break;
            case 3:
                Camera[0].transform.Find("CameraPlayer").GetComponent<Camera>().rect = new Rect(0, 0.5f, 1, 1);
                Camera[1].transform.Find("CameraPlayer").GetComponent<Camera>().rect = new Rect(-0.5f, -0.5f, 1, 1);
                Camera[2].transform.Find("CameraPlayer").GetComponent<Camera>().rect = new Rect(0.5f, -0.5f, 1, 1);
                break;
            case 4:
                Camera[0].transform.Find("CameraPlayer").GetComponent<Camera>().rect = new Rect(-0.5f, 0.5f, 1, 1);
                Camera[1].transform.Find("CameraPlayer").GetComponent<Camera>().rect = new Rect(0.5f, 0.5f, 1, 1);
                Camera[2].transform.Find("CameraPlayer").GetComponent<Camera>().rect = new Rect(-0.5f, -0.5f, 1, 1);
                Camera[3].transform.Find("CameraPlayer").GetComponent<Camera>().rect = new Rect(0.5f, -0.5f, 1, 1);
                break;
        }
        for (int i = 0; i < SelectScreenConfigs.Instance.playerAmmount; i++)
        {
            GameObject player = null;
            CinemachineVirtualCamera CVC;
            Vector3 spawnPos = new Vector3();
            switch (i)
            {
                case 0:
                    spawnPos = SpawnpointP1.transform.position;
                    break;
                case 1:
                    spawnPos = SpawnpointP2.transform.position;
                    break;
                case 2:
                    spawnPos = SpawnpointP3.transform.position;
                    break;
                case 3:
                    spawnPos = SpawnpointP4.transform.position;
                    break;
            }
            switch (SelectScreenConfigs.Instance.kartIDPlayer[i])
            {
                case 0:
                    player =
                    Instantiate(PrefabKartPadrao, spawnPos, Quaternion.identity);
                    break;
                case 1:
                    player =
                    Instantiate(PrefabKartSpeed, spawnPos, Quaternion.identity);
                    break;
                case 2:
                    player =
                    Instantiate(PrefabKartBulky, spawnPos, Quaternion.identity);
                    break;
                case 3:
                    player =
                    Instantiate(PrefabKartBoost, spawnPos, Quaternion.identity);
                    break;
            }
            CVC = Camera[i].transform.Find("CinemachineVirtualCamera").GetComponent<CinemachineVirtualCamera>();
            CVC.Follow = player.transform;
            CVC.LookAt = player.transform;
            KartController kartController = player.GetComponent<KartController>();
            kartController.axisRawVertical = "Vertical";
            kartController.axisRawHorizontal = "Horizontal";
            if (i != 0)
            {
                kartController.axisRawVertical += (i + 1).ToString();
                kartController.axisRawHorizontal += (i + 1).ToString();
            }
            switch (i)
            {
                case 0:
                    kartController.powerUpKey = KeyCode.E;
                    kartController.brakeKey = KeyCode.Space;
                    break;
                case 1:
                    kartController.powerUpKey = KeyCode.RightShift;
                    kartController.brakeKey = KeyCode.RightControl;
                    break;
                case 2:
                    kartController.powerUpKey = KeyCode.U;
                    kartController.brakeKey = KeyCode.M;
                    break;
                case 3:
                    kartController.powerUpKey = KeyCode.Keypad7;
                    kartController.brakeKey = KeyCode.Keypad1;
                    break;
            }
            karts.Add(kartController);
            kartController.playerID = i;
        }
        switch (SelectScreenConfigs.Instance.playerAmmount)
        {
            case 1:
                PwUp_TopRight.gameObject.SetActive(true);

                PwUp_TopLeft.gameObject.SetActive(false);
                PwUp_BottomLeft.gameObject.SetActive(false);
                PwUp_BottomRight.gameObject.SetActive(false);
                karts[0].GetComponent<KartPowerUpManager>().powerUpAtualImagem = PwUp_TopRight.GetComponent<Image>();

                //placeInRaceImage
                karts[0].placeInRaceImage = PlaceInRace_TopLeft.GetComponent<Image>();

                PlaceInRace_BottomLeft.gameObject.SetActive(false);
                PlaceInRace_BottomRight.gameObject.SetActive(false);
                PlaceInRace_TopRight.gameObject.SetActive(false);

                break;
            case 2:
                PwUp_TopRight.gameObject.SetActive(true);
                PwUp_BottomRight.gameObject.SetActive(true);

                PwUp_TopLeft.gameObject.SetActive(false);
                PwUp_BottomLeft.gameObject.SetActive(false);
                karts[0].GetComponent<KartPowerUpManager>().powerUpAtualImagem = PwUp_TopRight.GetComponent<Image>();
                karts[1].GetComponent<KartPowerUpManager>().powerUpAtualImagem = PwUp_BottomRight.GetComponent<Image>();

                //placeInRaceImage
                karts[0].placeInRaceImage = PlaceInRace_TopLeft.GetComponent<Image>();
                karts[1].placeInRaceImage = PlaceInRace_BottomLeft.GetComponent<Image>();

                PlaceInRace_BottomRight.gameObject.SetActive(false);
                PlaceInRace_TopRight.gameObject.SetActive(false);
                break;
            case 3:
                PwUp_TopRight.gameObject.SetActive(true);
                PwUp_BottomRight.gameObject.SetActive(true);
                PwUp_BottomLeft.gameObject.SetActive(true);

                PwUp_TopLeft.gameObject.SetActive(false);
                karts[0].GetComponent<KartPowerUpManager>().powerUpAtualImagem = PwUp_TopRight.GetComponent<Image>();
                karts[1].GetComponent<KartPowerUpManager>().powerUpAtualImagem = PwUp_BottomLeft.GetComponent<Image>();
                karts[2].GetComponent<KartPowerUpManager>().powerUpAtualImagem = PwUp_BottomRight.GetComponent<Image>();

                //placeInRaceImage
                karts[0].placeInRaceImage = PlaceInRace_TopLeft.GetComponent<Image>();
                karts[1].placeInRaceImage = PlaceInRace_BottomLeft.GetComponent<Image>();
                karts[2].placeInRaceImage = PlaceInRace_BottomRight.GetComponent<Image>();

                PlaceInRace_TopRight.gameObject.SetActive(false);
                break;
            case 4:
                PwUp_TopRight.gameObject.SetActive(true);
                PwUp_BottomRight.gameObject.SetActive(true);
                PwUp_BottomLeft.gameObject.SetActive(true);
                PwUp_TopLeft.gameObject.SetActive(true);

                karts[0].GetComponent<KartPowerUpManager>().powerUpAtualImagem = PwUp_TopLeft.GetComponent<Image>();
                karts[1].GetComponent<KartPowerUpManager>().powerUpAtualImagem = PwUp_TopRight.GetComponent<Image>();
                karts[2].GetComponent<KartPowerUpManager>().powerUpAtualImagem = PwUp_BottomLeft.GetComponent<Image>();
                karts[3].GetComponent<KartPowerUpManager>().powerUpAtualImagem = PwUp_BottomRight.GetComponent<Image>();

                //placeInRaceImage
                karts[0].placeInRaceImage = PlaceInRace_TopLeft.GetComponent<Image>();
                karts[1].placeInRaceImage = PlaceInRace_TopRight.GetComponent<Image>();
                karts[2].placeInRaceImage = PlaceInRace_BottomLeft.GetComponent<Image>();
                karts[3].placeInRaceImage = PlaceInRace_BottomRight.GetComponent<Image>();

                break;
        }
    }
    private IEnumerator checkPlayer()
    {
        while(true)
        {

            yield return new WaitForSeconds(0.1f);
        }
    }

}
