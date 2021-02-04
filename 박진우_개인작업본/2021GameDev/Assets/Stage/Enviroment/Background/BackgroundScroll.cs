using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BackgroundScroll : MonoBehaviour
{
    // 배경화면 무한 스크롤링 코드
    private MeshRenderer render;
    public float speed;
    private float offset;
    // Start is called before the first frame update
    void Start()
    {
        render = GetComponent<MeshRenderer>();
    }

    // Update is called once per frame
    void Update()
    {
        offset += Time.deltaTime * speed;      
        render.material.mainTextureOffset = new Vector2(offset, 0);
    }
}
