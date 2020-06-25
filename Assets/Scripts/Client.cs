using System;
using System.Collections;
using System.Collections.Generic;
using System.Net.Sockets;
using System.Text;
using System.Threading;
using UnityEngine;

public class Client : MonoBehaviour
{
    private TcpClient socket;
    private Thread clientReceiveThread;
    public static Client instance { get; private set; }

    private void Awake()
    {
        if (instance == null)
        {
            instance = this;
            DontDestroyOnLoad(gameObject);
        } else
        {
            Destroy(gameObject);
        }
    }


    // Start is called before the first frame update
    void Start()
    {
        ConnectToTcpServer();
        
    }

    // Update is called once per frame
    void Update()
    {
        
        
    }

    // Inicializar thread para recibir mensajes del servidor
    private void ConnectToTcpServer()
    {
        try
        {
            clientReceiveThread = new Thread(new ThreadStart(ListenForData));
            clientReceiveThread.IsBackground = true;
            clientReceiveThread.Start();
        } catch (Exception e)
        {
            Debug.Log("On client connect exception " + e);
        }
    }

    // Escuchar mensajes del servidor
    private void ListenForData()
    {
        try
        {
            socket = new TcpClient("localhost", 27015);
            Byte[] bytes = new byte[1024];
            while (true)
            {
                using (NetworkStream stream = socket.GetStream())
                {
                    int length;
                    while ((length = stream.Read(bytes,0,bytes.Length)) != 0)
                    {
                        var incomingData = new byte[length];
                        Array.Copy(bytes,0,incomingData,0,length);

                        string serverMessage = Encoding.ASCII.GetString(incomingData);
                        Debug.Log("server message received as: " + serverMessage);
                    }
                }
            }
        } catch (SocketException socketException)
        {
            Debug.Log("Socket exception: " + socketException);
        }
    }

    // Envia mensaje al servidor
    public void SendPlayerMessage(float x, float y)
    {
        if (socket == null)
        {
            return;
        }
        try
        {
            NetworkStream stream = socket.GetStream();
            if (stream.CanWrite)
            {
                string clientMessage = "Player X: " + x.ToString() + " , Player Y: " + y.ToString() + "";
                byte[] clientMessageAsByteArray = Encoding.ASCII.GetBytes(clientMessage);


                stream.Write(clientMessageAsByteArray, 0, clientMessageAsByteArray.Length);
                Debug.Log("Client sent his message - should be received by server");
            }
        } catch (SocketException socketException)
        {
            Debug.Log("Socket exception: " + socketException);
        }
    }

    public void SendEnemyMessage(float x, float y)
    {
        if (socket == null)
        {
            return;
        }
        try
        {
            NetworkStream stream = socket.GetStream();
            if (stream.CanWrite)
            {
                string clientMessage = "Enemy X: " + x.ToString() + " , Enemy Y: " + y.ToString() + "";
                byte[] clientMessageAsByteArray = Encoding.ASCII.GetBytes(clientMessage);


                stream.Write(clientMessageAsByteArray, 0, clientMessageAsByteArray.Length);
                Debug.Log("Client sent his message - should be received by server");
            }
        }
        catch (SocketException socketException)
        {
            Debug.Log("Socket exception: " + socketException);
        }
    }
}
