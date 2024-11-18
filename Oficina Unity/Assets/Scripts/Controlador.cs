using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;
using UnityEditor;
using UnityEngine.SceneManagement;

public class Controlador : MonoBehaviour
{
    public TMP_Text placarTexto, placarGO;
    public Transform brotador;
    private Animator anim;
    public GameObject Gcoin,Scoin,Gdiamond,Bdiamond,Rdiamond, bomba;
    public List<GameObject> inGame,endGame = new List<GameObject>();
    public float tempo;

    public int placar;

    public void Replay()
    {
        placar = 0;
        placarTexto.SetText(placar.ToString());
    }
    public void Start()
    {
        anim = GetComponent<Animator>();
        placar = 0;
        placarTexto.SetText(placar.ToString());
        StartCoroutine(AparecerObjetos());
    }

    public void EncostouBomba()
    {
        anim.SetTrigger("Shake");
    }

    public void EncostouFruta(int pontos)
    {
        placar += pontos;
        placarTexto.SetText(placar.ToString());
    }
    public void ExitGame()
    {
        Debug.Log("Saindo do Jogo");
#if UNITY_EDITOR
        // Para funcionar no Editor do Unity
        EditorApplication.isPlaying = false;
#else
        // Para builds (jogo publicado)
        Application.Quit();
#endif
        
    }

    IEnumerator AparecerObjetos()
    {
        while (true)
        {
            yield return new WaitForSeconds(tempo);

            float posicao = Random.Range(-8f, 8f);
            float item = Random.Range(0,10);
            if (item > 8f)
            {
                Instantiate(bomba, new Vector3(posicao, brotador.position.y, 0), Quaternion.identity);
            }
            else if(item > 7.5f)
            {
                Instantiate(Rdiamond, new Vector3(posicao, brotador.position.y, 0), Quaternion.identity);
            }
            else if (item > 6.25f)
            {
                Instantiate(Bdiamond, new Vector3(posicao, brotador.position.y, 0), Quaternion.identity);
            }
            else if (item > 5f)
            {
                Instantiate(Gdiamond, new Vector3(posicao, brotador.position.y, 0), Quaternion.identity);
            }
            else if (item > 3f)
            {
                Instantiate(Gcoin, new Vector3(posicao, brotador.position.y, 0), Quaternion.identity);
            }
            else if (item > 0f)
            {
                Instantiate(Scoin, new Vector3(posicao, brotador.position.y, 0), Quaternion.identity);
            }
        }
    }

    public void GameOverF()
    {
        foreach(GameObject obj in inGame)
        {
            obj.SetActive(false);
        }
    }
    public void GameOverT()
    {

        foreach (GameObject obj in inGame)
        {
            obj.SetActive(true);
        }
        placarGO.SetText("Placar: " + (placar.ToString()));
    }
}
