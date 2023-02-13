using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
public class TransferMap : MonoBehaviour
{
    public string transferMapName; //�̵��� ���� �̸�
    private MovingObject1 thePlayer;
    public Transform target;
    public BoxCollider2D TargetBound;
    private CameraManager theCamera;
    void Start()
    {
        thePlayer = FindObjectOfType<MovingObject1>();
        theCamera = FindObjectOfType<CameraManager>();
    }
    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.gameObject.name == "Player")
        {
            thePlayer.currentMapName = transferMapName;
            theCamera.SetBound(TargetBound);
            theCamera.transform.position = new Vector3(target.transform.position.x, target.transform.position.y, theCamera.transform.position.z);
            //���̵� ���(����ȯ�̶� ����°� ��õ���� ����)
            thePlayer.transform.position = target.transform.position;
        }
    }
}