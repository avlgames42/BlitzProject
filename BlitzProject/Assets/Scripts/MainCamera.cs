using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MainCamera : MonoBehaviour {

    Transform target;
    float topLeftX, topLeftY, downRightX, downRightY;

    public GameObject targetMap;

    Vector2 velocity;
    public float smoothTime;

    void Awake() {
        //pega a posicao do transform do alvo a ser seguido
        target = GameObject.FindGameObjectWithTag("Player").transform;
        //GetComponent<Camera>().aspect = 1;
    }

    // Use this for initialization
    void Start () {

        setBounds(targetMap);
	}
	
	// Update is called once per frame
	void Update () {
        //atualiza gradualmente a posicao entre os objetos
        float posX = Mathf.Round(Mathf.SmoothDamp(transform.position.x, target.position.x, ref velocity.x, smoothTime) * 100) / 100;
        float posY = Mathf.Round(Mathf.SmoothDamp(transform.position.y, target.position.y, ref velocity.y, smoothTime) * 100) / 100;

        //faz a movimentacao da camera em direcao ao alvo
        transform.position = new Vector3(
            Mathf.Clamp(posX, topLeftX, downRightX),
            Mathf.Clamp(posY, downRightY, topLeftY),
            transform.position.z
            );

	}

    public void setBounds(GameObject map)
    {
        //pega as propriedades do tiledmap para caputrar os limites da camera
        Tiled2Unity.TiledMap config = map.GetComponent<Tiled2Unity.TiledMap>();
        float cameraSize = Camera.main.orthographicSize;

        //calcula os limites da camera, nos dois pontos canto superior esquerdo e canto inferiror direito
        
        topLeftX = map.transform.position.x + (2*cameraSize) + 0.7f; // +1
        //topLeftX = map.transform.position.x + cameraSize; // +1
        topLeftY = map.transform.position.y - cameraSize; //-1
        downRightX = map.transform.position.x + (config.NumTilesWide) - (2*cameraSize) - 0.7f;//-1
        //downRightX = map.transform.position.x + (config.NumTilesWide) - cameraSize;//-1
        downRightY = map.transform.position.y - (config.NumTilesHigh) + cameraSize;//-1

        fastMove();
        
    }

    //corrige atraso quando muda de mapa, seta a camera na posicao doplayer
    public void fastMove()
    {
        transform.position = new Vector3(
            target.position.x,
            target.position.y,
            transform.position.z
            );
    }
}
