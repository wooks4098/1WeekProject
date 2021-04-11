using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class ShootManager : MonoBehaviour
{

    [SerializeField]
    private GameObject EndPanel;
    [SerializeField]
    private Text EndScore_Text;

    [SerializeField]
    private Text Score_Text;
    [SerializeField]
    private GameObject Score_obj;
    private int Score;

    [SerializeField]
    private Slider Power_slider;
    [SerializeField]
    private GameObject Power_slider_obj;

    [SerializeField]
    private float Min_Power;
    [SerializeField]
    private float Max_Power;
    [SerializeField]
    private float Power;

    private bool isPowerUP = true;

    public GameObject Cube_Base;
    [SerializeField]
    private GameObject Block;
    [SerializeField]
    private GameObject wall;

    [SerializeField]
    private bool CanShoot = true;

    bool isGameEnd = false;

    private static ShootManager instance = null;

    void Awake()
    {
        if (null == instance)
        {
            instance = this;
            DontDestroyOnLoad(this.gameObject);
        }
        else
        {
            Destroy(this.gameObject);
        }
        Power = Min_Power;
        Score = 0;
        EndPanel.SetActive(false);
        Score_obj.SetActive(true);
        Power_slider_obj.SetActive(true);
    }

    public static ShootManager Instance
    {
        get
        {
            if (null == instance)
            {
                return null;
            }
            return instance;
        }
    }
    private void Update()
    {

        if (Input.GetKeyUp(KeyCode.Space) && CanShoot)
        {
            GameObject block = Instantiate(Block, gameObject.transform);
            block.transform.parent = Cube_Base.transform;
            Shoot block_shoot = block.GetComponent<Shoot>();
            block_shoot.shoot(Power);
            gameObject.transform.position += new Vector3(0, 0.5f, 0);
            CanShoot = false;

        }
    }

    private void FixedUpdate()
    {
        Power_slider.value = 1 - ((Max_Power - Power) / (Max_Power - Min_Power));

        if (Input.GetKey(KeyCode.Space) && CanShoot)
        {
            if (isPowerUP)
            {
                Power+= 50;
                if (Power >= Max_Power)
                    isPowerUP = false;
            }
            else
            {
                Power -= 50;
                if (Power <= Min_Power)
                    isPowerUP = true;
            }

        }

    }

    public void EndGame()
    {
        isGameEnd = true;
        EndPanel.SetActive(true);
        Score_obj.SetActive(false);
        Power_slider_obj.SetActive(false);

        EndScore_Text.text = Score_Text.text;
        Debug.Log("게임끝");
    }

    public void SceneReload()
    {
        for(int i = Cube_Base.transform.childCount-1; i>-1; i--)
        {
            GameObject child = Cube_Base.transform.GetChild(i).gameObject;
            Destroy(Cube_Base.transform.GetChild(i).gameObject);
        }

        EndPanel.SetActive(false);
        Score_obj.SetActive(true);
        Power_slider_obj.SetActive(true);

        gameObject.transform.position = new Vector3(0, 0.02f, -15f);
        Score = 0;
        Score_Text.text = "Score: " + Score.ToString();

    }

    public void Bolck_Move_Stop(Transform _BlockPos)
    {
        wall.transform.position = new Vector3(_BlockPos.position.x, _BlockPos.position.y, _BlockPos.position.z-2.5f);
        Power = Min_Power;
        Power_slider.value = 0f;
        CanShoot = true;
        Score++;
        Score_Text.text = "Score: " + Score.ToString();
    }
}
