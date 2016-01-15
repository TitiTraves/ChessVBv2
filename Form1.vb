Imports System.Net.Sockets
Imports System.Net
Imports System.IO
Imports System.Threading

Public Class Form1


#Region "Les Images PNG"
    Dim wp As New Bitmap(My.Resources.wp)
    Dim wr As New Bitmap(My.Resources.wr)
    Dim wn As New Bitmap(My.Resources.wn)
    Dim wb As New Bitmap(My.Resources.wb)
    Dim wq As New Bitmap(My.Resources.wq)
    Dim wk As New Bitmap(My.Resources.wk)

    Dim bp As New Bitmap(My.Resources.bp)
    Dim br As New Bitmap(My.Resources.br)
    Dim bn As New Bitmap(My.Resources.bn)
    Dim bb As New Bitmap(My.Resources.bb)
    Dim bq As New Bitmap(My.Resources.bq)
    Dim bk As New Bitmap(My.Resources.bk)

    Dim bboard As New Bitmap(My.Resources.board90)
    Dim bHaut As New Bitmap(My.Resources.bHaut)
    Dim bCote As New Bitmap(My.Resources.bcote)

    Dim greenCircle As New Bitmap(My.Resources.vert)
    Dim redCircle As New Bitmap(My.Resources.rouge)
    Dim blueCircle As New Bitmap(My.Resources.bleu)
    Dim GreenCross As New Bitmap(My.Resources.pg)
#End Region


    Private Board10x10(100) As Char 'pour correspondre avec mon objFenMoves

    Dim PieceSize As Integer
    Dim couleur As String
    Dim backBuffer As New Bitmap(My.Resources.board90)
    Dim g As Graphics = Graphics.FromImage(backBuffer)
    Dim serveur As ServeurTCP
    Dim sqFrom As String
    Dim sqTo As String
    Dim EffaceNoir As Boolean = False   'une rustine de dernière minute
    Public ThePOS As ObjFenMoves
    Dim monClient As TcpClient

#Region "fonctions en doubles avec objMoves :-("
    Private Function ToBoard(ByVal FEN_Champ1 As String) As Byte
        Dim lignes() As String
        Dim NumLigne As Integer
        Dim NumCol As Integer
        Dim LaLigne As String
        Dim champs() As String

        champs = FEN_Champ1.Split(" ")


        'on place des bords partout
        For NumLigne = 0 To 99
            Board10x10(NumLigne) = "*"
        Next

        lignes = champs(0).Split("/")

        If lignes.Count <> 8 Then
            Return 10
        End If

        For NumLigne = 0 To 7

            LaLigne = lignes(NumLigne)

            LaLigne = LaLigne.Replace("1", " ")
            LaLigne = LaLigne.Replace("2", "  ")
            LaLigne = LaLigne.Replace("3", "   ")
            LaLigne = LaLigne.Replace("4", "    ")
            LaLigne = LaLigne.Replace("5", "     ")
            LaLigne = LaLigne.Replace("6", "      ")
            LaLigne = LaLigne.Replace("7", "       ")
            LaLigne = LaLigne.Replace("8", "        ")

            If LaLigne.Length = 8 Then
                For NumCol = 0 To 7
                    Board10x10((8 - NumLigne) * 10 + (NumCol + 1)) = LaLigne.Substring(NumCol, 1)
                Next
            Else
                Return 11 + NumLigne
            End If

        Next



        Return 0
    End Function

    Private Function SquareIndex(ByVal sqName As String) As Byte
        Dim lettre As Char
        Dim colonne As Byte
        Dim ligne As Byte

        If sqName.Length <> 2 Then Return 0

        lettre = sqName.Substring(0, 1) 'recupere la lettre
        colonne = Asc(lettre) - 96 'recupere le numero de colonne

        If colonne < 1 Or colonne > 8 Then Return 0

        ligne = sqName.Substring(1, 1) 'recupere le numero de ligne

        If ligne < 1 Or ligne > 8 Then Return 0

        Return ligne * 10 + colonne

    End Function
#End Region


#Region "Fonction de dessin"

    Private Function bmpPiece(ByVal name As Char) As Bitmap
        Select Case name
            Case "P"
                Return wp
            Case "R"
                Return wr
            Case "N"
                Return wn
            Case "B"
                Return wb
            Case "Q"
                Return wq
            Case "K"
                Return wk

            Case "p"
                Return bp
            Case "r"
                Return br
            Case "n"
                Return bn
            Case "b"
                Return bb
            Case "q"
                Return bq
            Case "k"
                Return bk

            Case "1"
                Return redCircle
            Case "2"
                Return greenCircle
            Case "3"
                Return blueCircle
            Case "4"
                Return GreenCross


        End Select
    End Function

    Private Function Xsqi(ByVal sqi As Byte) As Integer
        Dim colonne As Byte
        Dim ligne As Byte

        colonne = sqi Mod 10
        ligne = sqi \ 10

        Return 18 + (colonne - 1) * PieceSize

    End Function

    Private Function Ysqi(ByVal sqi As Byte) As Integer
        Dim colonne As Byte
        Dim ligne As Byte

        colonne = sqi Mod 10
        ligne = sqi \ 10

        Return 18 + (8 - ligne) * PieceSize

    End Function

    Private Sub PutPiece(ByVal sqIndex As Byte, ByVal Initiale As Char)
        Dim rect As Rectangle
        rect.X = Xsqi(sqIndex)
        rect.Y = Ysqi(sqIndex)
        rect.Width = PieceSize
        rect.Height = PieceSize

        g.DrawImage(bmpPiece(Initiale), rect)


    End Sub

    Private Sub PutSymbol(ByVal sqIndex As Byte, ByVal Initiale As Char)
        Dim rect As Rectangle
        Dim p As Graphics = PictureBox1.CreateGraphics
        rect.X = Xsqi(sqIndex)
        rect.Y = Ysqi(sqIndex)
        rect.Width = PieceSize
        rect.Height = PieceSize

        p.DrawImage(bmpPiece(Initiale), rect)

    End Sub

    Private Sub DrawPiece()
        Dim rect As Rectangle
        Dim pt As Point

        PictureBox1.Height = Me.ClientSize.Height - 20
        PictureBox1.Width = Me.ClientSize.Height - 20

        PictureBox1.Image = New Bitmap(PictureBox1.Width, PictureBox1.Height)

        backBuffer = New Bitmap(PictureBox1.Width, PictureBox1.Height)
        g = Graphics.FromImage(backBuffer)

        pt.X = 1 : pt.Y = 1 : g.DrawImage(bHaut, pt)                        'dessine le bord haut
        pt.X = 1 : pt.Y = PictureBox1.Height - 17 : g.DrawImage(bHaut, pt)  'dessine le bord bas
        pt.X = 1 : pt.Y = 1 : g.DrawImage(bCote, pt)                        'dessine le bord gauche
        pt.X = PictureBox1.Width - 17 : pt.Y = 1 : g.DrawImage(bCote, pt)   'dessine le bord droit

        rect.X = 0 : rect.Y = 0
        rect.Width = PictureBox1.Width - 1 : rect.Height = PictureBox1.Height - 1
        g.DrawRectangle(Pens.Brown, rect)                                   'dessine le filet exterieur 

        rect.X = 17 : rect.Y = 17
        rect.Width = PictureBox1.Width - 35 : rect.Height = PictureBox1.Height - 35
        g.DrawRectangle(Pens.Black, rect)                                   'dessine le filet intérieur

        rect.X = 18 : rect.Y = 18
        rect.Width = PictureBox1.Width - 36 : rect.Height = PictureBox1.Height - 36
        g.DrawImage(bboard, rect)                                           'dessine l'échiquier

        PieceSize = (PictureBox1.Height - 36) / 8

        lvMoves.Top = 10
        lvMoves.Left = PictureBox1.Left + PictureBox1.Width + 10
        lvMoves.Height = PictureBox1.Height

        PictureBox1.Image = backBuffer

        ToBoard(ThePOS.GetFEN)
        ' Board10x10 = EnvoyerEchiquier(Board10x10)
        Dim LaPiece As Char
        For i = 11 To 88
            LaPiece = Board10x10(i)
            If LaPiece <> " " And LaPiece <> "*" Then
                PutPiece(i, LaPiece)
            End If
        Next

    End Sub
    Private Sub DrawPiece(Board10x10 As String)
        Dim rect As Rectangle
        Dim pt As Point

        PictureBox1.Height = Me.ClientSize.Height - 20
        PictureBox1.Width = Me.ClientSize.Height - 20

        PictureBox1.Image = New Bitmap(PictureBox1.Width, PictureBox1.Height)

        backBuffer = New Bitmap(PictureBox1.Width, PictureBox1.Height)
        g = Graphics.FromImage(backBuffer)

        pt.X = 1 : pt.Y = 1 : g.DrawImage(bHaut, pt)                        'dessine le bord haut
        pt.X = 1 : pt.Y = PictureBox1.Height - 17 : g.DrawImage(bHaut, pt)  'dessine le bord bas
        pt.X = 1 : pt.Y = 1 : g.DrawImage(bCote, pt)                        'dessine le bord gauche
        pt.X = PictureBox1.Width - 17 : pt.Y = 1 : g.DrawImage(bCote, pt)   'dessine le bord droit

        rect.X = 0 : rect.Y = 0
        rect.Width = PictureBox1.Width - 1 : rect.Height = PictureBox1.Height - 1
        g.DrawRectangle(Pens.Brown, rect)                                   'dessine le filet exterieur 

        rect.X = 17 : rect.Y = 17
        rect.Width = PictureBox1.Width - 35 : rect.Height = PictureBox1.Height - 35
        g.DrawRectangle(Pens.Black, rect)                                   'dessine le filet intérieur

        rect.X = 18 : rect.Y = 18
        rect.Width = PictureBox1.Width - 36 : rect.Height = PictureBox1.Height - 36
        g.DrawImage(bboard, rect)                                           'dessine l'échiquier

        PieceSize = (PictureBox1.Height - 36) / 8

        lvMoves.Top = 10
        lvMoves.Left = PictureBox1.Left + PictureBox1.Width + 10
        lvMoves.Height = PictureBox1.Height

        PictureBox1.Image = backBuffer

        ToBoard(ThePOS.GetFEN)
        ' Board10x10 = EnvoyerEchiquier(Board10x10)
        Dim LaPiece As Char
        For i = 11 To 88
            LaPiece = Board10x10(i)
            If LaPiece <> " " And LaPiece <> "*" Then
                PutPiece(i, LaPiece)
            End If
        Next

    End Sub

    Public Sub DrawMove()

        Dim sqMoves() As String
        Dim sq As String
        Dim txtMoves As String

        txtMoves = ThePOS.GetMoves(sqFrom)

        If txtMoves.Length > 0 Then
            sqMoves = txtMoves.Split(" ")

            For i = 0 To sqMoves.Count - 1
                sq = sqMoves(i)
                If sq.Substring(0, 1) <> "x" Then
                    If Not ThePOS.IsValidMove(sqFrom & sq) Then
                        PutSymbol(SquareIndex(sq), "3")
                    Else
                        PutSymbol(SquareIndex(sq), "2")
                    End If
                Else
                    sq = sqMoves(i).Substring(1, 2)
                    If Not ThePOS.IsValidMove(sqFrom & sq) Then
                        PutSymbol(SquareIndex(sq), "3")
                    Else
                        PutSymbol(SquareIndex(sq), "4")
                    End If
                End If
            Next
        End If

        Dim txtCanTake As String = ThePOS.WhoCanTake(sqFrom)

        If txtCanTake.Length > 0 Then
            sqMoves = txtCanTake.Split(" ")
            For i = 0 To sqMoves.Count - 1
                sq = sqMoves(i)
                PutSymbol(SquareIndex(sq), "1")
            Next
        End If

    End Sub


#End Region

#Region "Evenement de la form1"




    Private Sub Form1_FormClosing(ByVal sender As Object, ByVal e As System.Windows.Forms.FormClosingEventArgs) Handles Me.FormClosing
        wp.Dispose()
        wr.Dispose()
        wn.Dispose()
        wb.Dispose()
        wq.Dispose()
        wk.Dispose()

        bp.Dispose()
        br.Dispose()
        bn.Dispose()
        bb.Dispose()
        bq.Dispose()
        bk.Dispose()

        bboard.Dispose()
        bHaut.Dispose()
        bCote.Dispose()

        greenCircle.Dispose()
        redCircle.Dispose()
        blueCircle.Dispose()
        GreenCross.Dispose()
    End Sub


    Private Sub Form1_Load(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles MyBase.Load
        PictureBox1.Top = 10        'Place le bord
        PictureBox1.Left = 10       'Place le bord
        ThePOS = New ObjFenMoves()  'instancie ObjFenMoves
        ThePOS.LocalPiece = "TCFDR"
    End Sub

    Private Sub Form1_Resize(ByVal sender As Object, ByVal e As System.EventArgs) Handles Me.Resize
        Static OnEstMaximised As Boolean
        If Me.WindowState = FormWindowState.Maximized Then
            DrawPiece()
            OnEstMaximised = True
        Else
            If OnEstMaximised Then
                DrawPiece()
                OnEstMaximised = False
            End If
        End If
    End Sub

    Private Sub Form1_ResizeEnd(ByVal sender As Object, ByVal e As System.EventArgs) Handles Me.ResizeEnd
        DrawPiece()
    End Sub
#End Region

#Region "Evenement de la picturebox1"
    Private Sub PictureBox1_MouseDown(ByVal sender As Object, ByVal e As System.Windows.Forms.MouseEventArgs) Handles PictureBox1.MouseDown


        sqFrom = Chr(97 + Math.Truncate((e.X - 18) / (PictureBox1.Width - 36) * 8)) _
            + (8 - Math.Truncate((e.Y - 18) / (PictureBox1.Height - 36) * 8)).ToString

        DrawMove()

    End Sub

    Private Sub PictureBox1_MouseUp(ByVal sender As Object, ByVal e As System.Windows.Forms.MouseEventArgs) Handles PictureBox1.MouseUp
        sqTo = Chr(97 + Math.Truncate((e.X - 18) / (PictureBox1.Width - 36) * 8)) _
            + (8 - Math.Truncate((e.Y - 18) / (PictureBox1.Height - 36) * 8)).ToString

        DoMove()
        DrawPiece()

    End Sub

#End Region

#Region "Evenement listView"

    Private Sub lvMoves_MouseClick(ByVal sender As Object, ByVal e As System.Windows.Forms.MouseEventArgs) Handles lvMoves.MouseClick
        On Error Resume Next
        If e.X > lvMoves.Columns(0).Width + lvMoves.Columns(2).Width Then
            ThePOS.SetFEN(lvMoves.SelectedItems(0).SubItems(2).Tag)
            EffaceNoir = False
            DrawPiece()
        Else
            ThePOS.SetFEN(lvMoves.SelectedItems(0).SubItems(1).Tag)
            EffaceNoir = True
            DrawPiece()
        End If
    End Sub

#End Region

#Region "Gestion des mouvements et de la ListView"

    Public Sub deletenextitem()
        On Error GoTo err

        If lvMoves.SelectedIndices(0) <> lvMoves.Items.Count - 1 Then
            While lvMoves.SelectedIndices(0) <> lvMoves.Items.Count - 1
                lvMoves.Items.RemoveAt(lvMoves.Items.Count - 1)
            End While
            If EffaceNoir Then
                If lvMoves.Items(lvMoves.Items.Count - 1).SubItems.Count = 3 Then
                    lvMoves.Items(lvMoves.Items.Count - 1).SubItems.RemoveAt(2)
                End If
            End If
        End If
err:

    End Sub

    Private Sub DoMove()
        Dim lvi As New ListViewItem

        If sqFrom <> sqTo Then

            If Not ThePOS.IsValidMove(sqFrom & sqTo) Then
                Beep()
            Else

                deletenextitem()

                If ThePOS.WhiteToPlay Then
                    lvi.Text = ThePOS.MovesPlayed.ToString & "."
                    lvi.SubItems.Add(ThePOS.PGNmove(sqFrom & sqTo))
                    ThePOS.MakeMove(sqFrom & sqTo)
                    lvi.SubItems(1).Tag = ThePOS.GetFEN()
                    lvMoves.Items.Add(lvi)

                Else
                    lvi = lvMoves.Items(lvMoves.Items.Count - 1)
                    lvi.SubItems.Add(ThePOS.PGNmove(sqFrom & sqTo))
                    ThePOS.MakeMove(sqFrom & sqTo)
                    lvi.SubItems(2).Tag = ThePOS.GetFEN()
                End If

                lvMoves.Items(lvMoves.Items.Count - 1).Selected = True



            End If
        End If
        EnvoiCoup(sqFrom & sqTo)
    End Sub
    Private Sub DoMove(coup As String)
        Dim lvi As New ListViewItem



        If Not ThePOS.IsValidMove(coup) Then
            Beep()
        Else

            deletenextitem()

            If ThePOS.WhiteToPlay Then
                lvi.Text = ThePOS.MovesPlayed.ToString & "."
                lvi.SubItems.Add(ThePOS.PGNmove(coup))
                ThePOS.MakeMove(coup)

                lvi.SubItems(1).Tag = ThePOS.GetFEN()
                lvMoves.Items.Add(lvi)

            Else
                lvi = lvMoves.Items(lvMoves.Items.Count - 1)
                lvi.SubItems.Add(ThePOS.PGNmove(coup))
                ThePOS.MakeMove(coup)
                lvi.SubItems(2).Tag = ThePOS.GetFEN()
            End If

            lvMoves.Items(lvMoves.Items.Count - 1).Selected = True



        End If

    End Sub





#End Region


#Region "Serveur"

    Private Sub Button2_Click(sender As Object, e As EventArgs) Handles Button2.Click
        DrawPiece()
        DemarrerClient()

    End Sub
    Public Sub DemarrerClient()



        monClient = New TcpClient("127.0.0.1", 8080)
        Dim MonFlux As NetworkStream = monClient.GetStream()
        Dim MonReader As StreamReader = New StreamReader(MonFlux)
        Dim MonWriter As StreamWriter = New StreamWriter(MonFlux)

        ' Dim ThreadClient As New Thread(AddressOf TraitementConnexionClient)
        '
        'ThreadClient.Start()
        couleur = MonReader.ReadLine()
        If couleur = "Blanc" Then
        Else RecoitCoup()
        End If


    End Sub

    'Public Function EnvoyerEchiquier(echiquier As String)

    '    Dim monClient As TcpClient = New TcpClient("127.0.0.1", 8080)
    '    Dim MonFlux As NetworkStream = monClient.GetStream()
    '    Dim MonReader As StreamReader = New StreamReader(MonFlux)
    '    Dim MonWriter As StreamWriter = New StreamWriter(MonFlux)


    '    MonWriter.WriteLine(echiquier)
    '    MonWriter.Flush()
    '    echiquier = MonReader.ReadLine()

    '    Return echiquier

    'End Function





    'Sub TraitementConnexionClient()
    '    MsgBox("Connexion à la partie réussie !")
    '    Console.WriteLine("Thread client lancé. ")
    '    Dim MonFlux As NetworkStream = monClient.GetStream()
    '    Dim MonReader As StreamReader = New StreamReader(MonFlux)

    '    While (monClient.Connected)
    '        Dim Recu As String

    '        Try

    '            Recu = MonReader.ReadLine()
    '            'Message reçu
    '        Catch

    '        End Try
    '        'If ThePOS.WhiteToPlay Then
    '        'DoMove(Recu) 'Diffuse le message à tout le monde 
    '        'DrawPiece()
    '        'End If
    '    End While
    '    'Dim MonThread As Thread
    '    'Change les statuts des contrôles
    '    'Me.Button1.Enabled = False
    '    'Me.Button2.Enabled = False


    '    'MonThread = New Thread(AddressOf ThreadLecture)
    '    ' MonThread.Start()
    'End Sub
    Sub EnvoiCoup(ByVal coup As String)
        Dim MonFlux As NetworkStream = monClient.GetStream()
        Dim MonReader As StreamReader = New StreamReader(MonFlux)
        Dim MonWriter As StreamWriter = New StreamWriter(MonFlux)

        MonWriter.WriteLine(coup)
        MonWriter.Flush()

        RecoitCoup()
    End Sub
    Sub RecoitCoup()
        Dim MonFlux As NetworkStream = monClient.GetStream()
        Dim MonReader As StreamReader = New StreamReader(MonFlux)
        Dim MonWriter As StreamWriter = New StreamWriter(MonFlux)

        Dim Recu As String

        Recu = MonReader.ReadLine()
        DoMove(Recu)
        DrawPiece()


    End Sub


    'Sub ThreadLecture()
    '    Dim MonFlux As NetworkStream = monClient.GetStream()
    '    Dim MonReader As StreamReader = New StreamReader(MonFlux)
    '    Dim MonWriter As StreamWriter = New StreamWriter(MonFlux)
    '    While (monClient.Connected) 'Tant qu'on est connecté au serveur
    '        Dim Recu As String
    '        Try
    '            Recu = MonReader.ReadLine()
    '            MonReader.DiscardBufferedData()

    '        Catch ex As Exception 'Erreur si fermeture du socket pendant la réception
    '            MsgBox("Connexion perdue, arrêt de la réception des données ...")
    '    End Try
    '        If Recu <> "" Then
    '            DoMove(Recu)
    '            DrawPiece()
    '        End If
    '    End While
    'End Sub
#End Region
End Class
