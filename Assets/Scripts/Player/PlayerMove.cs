using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;
using UnityEngine.EventSystems;

public class Player : MonoBehaviour
{
    private NavMeshAgent playerAgent;
    // Start is called before the first frame update
    void Start()
    {
        // 初始化导航组件(获取在场景中的导航组件)
        // 开销较大的调用，通常在Start中调用
        playerAgent = GetComponent<NavMeshAgent>();
        playerAgent.acceleration = 40.0f;
    }

    // Update is called once per frame
    void Update()
    {
        // 检测鼠标点击位置
        if (Input.GetMouseButtonDown(0) && EventSystem.current.IsPointerOverGameObject() == false)
        {
            // 对鼠标落点进行射线检测，从落点开始发射一条射线，查看它落在什么物体的什么位置
            Ray ray = Camera.main.ScreenPointToRay(Input.mousePosition);
            // 创建落点
            RaycastHit hit;
            // 检测落点是否有效
            bool isCollide = Physics.Raycast(ray, out hit);
            if (isCollide)
            {
                if (hit.collider.CompareTag("Ground"))
                {
                    // 如果落点有效，则将角色位置和方向移向落点
                    playerAgent.stoppingDistance = 0;
                    playerAgent.SetDestination(hit.point);
                } else if (hit.collider.CompareTag("Interactable"))
                {
                    hit.collider.GetComponent<InteractableObject>().OnClick(playerAgent);
                }


            }
        }
    }
}
