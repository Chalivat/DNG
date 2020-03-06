using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class GameServerManager : MonoBehaviour
{
    public static GameServerManager instance;

    public GameObject serverPrefab;
    public GameObject clientPrefab;

    private void Start()
    {
        DontDestroyOnLoad(gameObject);
    }

    private void Awake()
    {
        if (instance == null)
        {
            instance = this;
        }
        else if (instance != this)
        {
            Debug.Log("Instance already exists, destroying object!");
            Destroy(this);
        }
    }

    public void CreateLocalServer()
    {
        GameObject server = Instantiate(serverPrefab);
        DontDestroyOnLoad(server);

        GameServer client_script = server.GetComponent<GameServer>();
        client_script.InitializeServer();
    }

    public void CreateClient(string _ip, int _id)
    {
        GameObject client = Instantiate(clientPrefab);
        DontDestroyOnLoad(client);

        GameClient.instance.StartClient(_ip, _id);
        UIManager.instance.label.text = _id.ToString();
    }

    public void LauncheGame()
    {
        StartCoroutine(LoadScene(1));
    }

    IEnumerator LoadScene(int index)
    {
        AsyncOperation operation = SceneManager.LoadSceneAsync(index);

        while(!operation.isDone)
        {
            yield return null;
        }

        GameClient.instance.SendMessageToServer("Launched\t" + GameClient.instance.id);
    }
}
