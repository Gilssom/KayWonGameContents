using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.EventSystems;

public class UIManager : MonoBehaviour
{
    private Camera m_Camera;

    [SerializeField]
    private Image[] m_ItemImage;

    [SerializeField]
    private GameObject[] m_LookObject;

    public Sprite[] m_Pictures;
    public Sprite[] m_LookPictures;
    public Sprite m_WaterCup;

    private int m_EquirItem;
    private int m_CurPanel;
    private int m_CurRoom;

    public Image[] m_FalsePicture;
    public Image[] m_LookFalsePicture;
    public GameObject[] m_Screws;

    bool isDry;
    bool isWater;

    private void Awake()
    {
        m_Camera = GameObject.Find("Main Camera").GetComponent<Camera>();
        m_EquirItem = 0;
        isDry = false;
        isWater = false;
    }

    void Update()
    {
        if(Input.GetKeyDown(KeyCode.Escape))
        {
            m_LookObject[m_CurPanel].SetActive(false);
        }
    }

    /// <summary>
    /// 0 = 관리실2로 /
    /// 1 = 관리실1로 /
    /// 2 = 전시관으로 /
    /// 3 = 전시관2로 /
    /// 4 = 화장실로 /
    /// 5 = 화장실2로
    /// </summary>
    /// <param name="RoomNumber"></param>
    public void RoomChangeButton(int RoomNumber)
    {
        m_CurRoom = RoomNumber;

        if (RoomNumber == 0)
        {
            m_Camera.transform.position = new Vector3(20, 0, -10);
        }
        else if (RoomNumber == 1)
        {
            m_Camera.transform.position = new Vector3(0, 0, -10);
        }
        else if (RoomNumber == 2)
        {
            m_Camera.transform.position = new Vector3(-20, 0, -10);
        }
        else if (RoomNumber == 3)
        {
            m_Camera.transform.position = new Vector3(-40, 0, -10);
        }
        else if (RoomNumber == 4)
        {
            m_Camera.transform.position = new Vector3(-60, 0, -10);
        }
        else if (RoomNumber == 5)
        {
            m_Camera.transform.position = new Vector3(-80, 0, -10);
        }
    }

    public void ItemCheck(bool isItem)
    {
        if(isItem)
        {
            GameObject ClickObject = EventSystem.current.currentSelectedGameObject;
            Image ClickImage = EventSystem.current.currentSelectedGameObject.GetComponent<Image>();
            if (m_EquirItem < 5)
                m_ItemImage[m_EquirItem].sprite = ClickImage.sprite;
            Destroy(ClickObject);
            m_EquirItem++;
        }
    }

    public void LookObject(int ObjNumber)
    {
        m_CurPanel = ObjNumber;
        m_LookObject[ObjNumber].SetActive(true);
    }

    public void ItemUse()
    {
        Image ClickObject = EventSystem.current.currentSelectedGameObject.GetComponent<Image>();
        if (ClickObject.sprite.name == "WaterCup" && m_CurPanel == 1 && isWater)
        {
            ClickObject.sprite = null;
            m_LookFalsePicture[0].GetComponent<Image>().sprite = m_LookPictures[0];
            m_FalsePicture[0].sprite = m_Pictures[0];
        }
        else if (ClickObject.sprite.name == "NoWaterCup" && m_CurRoom == 4)
        {
            ClickObject.sprite = m_WaterCup;
            isWater = true;
        }
        else if (ClickObject.sprite.name == "Fire" && m_CurPanel == 2)
        {
            ClickObject.sprite = null;
            m_LookFalsePicture[1].GetComponent<Image>().sprite = m_LookPictures[1];
            m_FalsePicture[1].sprite = m_Pictures[1];
        }
        else if (ClickObject.sprite.name == "Dirver" && m_CurPanel == 5)
        {
            ClickObject.sprite = null;
            isDry = true;
            m_LookFalsePicture[4].GetComponent<Image>().sprite = m_LookPictures[4];
            m_FalsePicture[4].sprite = m_Pictures[4];
            m_Screws[0].SetActive(false);
            m_Screws[1].SetActive(false);
            m_Screws[2].SetActive(false);
            m_Screws[3].SetActive(false);
        }
        else if (ClickObject.sprite.name == "HandDry" && m_CurPanel == 4 && isDry)
        {
            ClickObject.sprite = null;
            m_LookFalsePicture[3].GetComponent<Image>().sprite = m_LookPictures[3];
            m_FalsePicture[3].sprite = m_Pictures[3];
        }
        else if (ClickObject.sprite.name == "Soap" && m_CurPanel == 3)
        {
            ClickObject.sprite = null;
            m_LookFalsePicture[2].GetComponent<Image>().sprite = m_LookPictures[2];
            m_FalsePicture[2].sprite = m_Pictures[2];
        }
    }
}
