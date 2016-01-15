Imports System.Net.Sockets
Imports System.Net
Imports System.IO

Public Class ServeurTCP



    Dim monListener As TcpListener = New TcpListener(IPAddress.Parse("127.0.0.1"), 8080)
    Dim SocketClient As Socket
    Dim monClientCoteServeur As TcpClient
    Dim message As String
    Dim monClient As TcpClient


    Public Sub DemarrerServeur()
        monListener.Start()
        Console.WriteLine("Socket serveur initialisé.")

        While True 'Boucle à l'infini
            Console.WriteLine("En attente d'un client.")
            'Se met en attente de connexion et appelle TraitementConnexion() lors d'une connexion.
            monClient = monListener.AcceptTcpClient() 'Bloquant tant que pas de connexion
            TraitementConnexion(monClient)
        End While


    End Sub




    Sub TraitementConnexion(ByVal SocketEnvoi As TcpClient)
        Console.WriteLine("Socket client connecté, envoi de l'heure.")
        Try

            Dim MonFlux As NetworkStream = SocketEnvoi.GetStream()
            Dim MonReader As StreamReader = New StreamReader(MonFlux)
            Dim MonWriter As StreamWriter = New StreamWriter(MonFlux)

            message = MonReader.ReadLine()
            If message = "test" Then
                MonWriter.WriteLine("OK")
                MonWriter.Flush()
            End If


        Catch ex As Exception
            Console.WriteLine("Erreur lors de l'envoi du message au socket. " & ex.ToString)
        End Try
    End Sub


    Public Sub DemarrerClient()
        monClient = New TcpClient("127.0.0.1", 8080)


    End Sub

    Public Sub EnvoyerEchiquier(echiquier As String)
        Dim MonFlux As NetworkStream = monClient.GetStream()
        Dim MonReader As StreamReader = New StreamReader(MonFlux)
        Dim MonWriter As StreamWriter = New StreamWriter(MonFlux)


        MonWriter.WriteLine(echiquier)
        MonWriter.Flush()


    End Sub



End Class
