Public Class Form1
    Dim piecex(3) As Integer
    Dim piecey(3) As Integer
    Dim data(9, 19) As Integer
    Dim full As Boolean
    Dim piecetype As Integer
    Dim pieceposition As Integer
    Dim score As Integer
    Private Sub Form1_Load(sender As Object, e As EventArgs) Handles MyBase.Load
        piecex(0) = 0
        piecey(0) = 4

        piecex(1) = 0
        piecey(1) = 5

        piecex(2) = 1
        piecey(2) = 4

        piecex(3) = 1
        piecey(3) = 5

        AxWindowsMediaPlayer1.URL = "C:\Users\shbo5\source\repos\Tetris\Tetris\bin\Debug\Tetris.mp3"

        newPiece()
        Randomize()
    End Sub

    Private Sub Form1_Paint(sender As Object, e As PaintEventArgs) Handles Me.Paint
        Dim g As Graphics = e.Graphics


        For i = 0 To 3
            If piecetype = 1 Then
                g.FillRectangle(Brushes.Yellow, piecex(i) * 20, piecey(i) * 20, 20, 20)
            End If

            If piecetype = 2 Then
                g.FillRectangle(Brushes.Cyan, piecex(i) * 20, piecey(i) * 20, 20, 20)
            End If

            If piecetype = 3 Then
                g.FillRectangle(Brushes.Blue, piecex(i) * 20, piecey(i) * 20, 20, 20)
            End If

            If piecetype = 4 Then
                g.FillRectangle(Brushes.Orange, piecex(i) * 20, piecey(i) * 20, 20, 20)
            End If
            If piecetype = 5 Then
                g.FillRectangle(Brushes.LimeGreen, piecex(i) * 20, piecey(i) * 20, 20, 20)
            End If
            If piecetype = 6 Then
                g.FillRectangle(Brushes.Purple, piecex(i) * 20, piecey(i) * 20, 20, 20)
            End If
            If piecetype = 7 Then
                g.FillRectangle(Brushes.Red, piecex(i) * 20, piecey(i) * 20, 20, 20)
            End If
        Next



        For y = 0 To 19
            For x = 0 To 9
                If data(x, y) = 1 Then
                    g.FillRectangle(Brushes.Yellow, x * 20, y * 20, 20, 20)
                End If
                If data(x, y) = 2 Then
                    g.FillRectangle(Brushes.Cyan, x * 20, y * 20, 20, 20)
                End If
                If data(x, y) = 3 Then
                    g.FillRectangle(Brushes.Blue, x * 20, y * 20, 20, 20)
                End If
                If data(x, y) = 4 Then
                    g.FillRectangle(Brushes.Orange, x * 20, y * 20, 20, 20)
                End If
                If data(x, y) = 5 Then
                    g.FillRectangle(Brushes.LimeGreen, x * 20, y * 20, 20, 20)
                End If
                If data(x, y) = 6 Then
                    g.FillRectangle(Brushes.Purple, x * 20, y * 20, 20, 20)
                End If
                If data(x, y) = 7 Then
                    g.FillRectangle(Brushes.Red, x * 20, y * 20, 20, 20)
                End If
                g.DrawRectangle(Pens.Black, x * 20, y * 20, 20, 20)

            Next
        Next



    End Sub

    Private Sub Timer1_Tick(sender As Object, e As EventArgs) Handles Timer1.Tick





        If canMoveDown() Then
            For i = 0 To 3
                piecey(i) = piecey(i) + 1
            Next
        Else
            For i = 0 To 3
                data(piecex(i), piecey(i)) = piecetype

                clearRows()
            Next
            newPiece()
        End If
        For i = 0 To 3
            If isEmpty(piecex(i), piecey(i)) = False Then
                Timer1.Enabled = False
                MsgBox("Game Over")
            End If
        Next
        Refresh()


    End Sub

    Private Sub Form1_KeyDown(sender As Object, e As KeyEventArgs) Handles Me.KeyDown

        If e.KeyCode = Keys.Left And canMoveLeft() Then
            For i = 0 To 3
                piecex(i) = piecex(i) - 1
            Next
        End If



        If e.KeyCode = Keys.Right And canMoveRight() Then
            For i = 0 To 3
                piecex(i) = piecex(i) + 1
            Next
        End If


        If e.KeyCode = Keys.Space Then

            While canMoveDown()
                score = score + 1
                lblScore.Text = "Score: " & score
                For i = 0 To 3
                    piecey(i) = piecey(i) + 1

                Next
            End While
            For i = 0 To 3
                data(piecex(i), piecey(i)) = piecetype
            Next
            newPiece()

            clearRows()


        End If

        If e.KeyCode = Keys.Up Then
            rotate()
        End If

        Refresh()
    End Sub

    Public Function isEmpty(x As Integer, y As Integer)
        If y > 19 Or y < 0 Then
            Return False
        End If

        If x > 9 Or x < 0 Then
            Return False
        End If

        If data(x, y) <> 0 Then
            Return False
        End If
        Return True
    End Function
    Public Sub clearRows()
        Dim numRows As Integer
        full = True
        For y = 0 To 19
            For x = 0 To 9
                If data(x, y) = 0 Then
                    full = False
                End If
            Next
            If full = True Then
                numRows = numRows + 1
                For i = 0 To y - 2
                    For j = 0 To 9
                        data(j, y - i) = data(j, y - i - 1)
                    Next
                Next
            End If
            full = True
        Next
        If numRows = 1 Then
            score = score + 40
        End If

        If numRows = 2 Then
            score = score + 100
        End If

        If numRows = 3 Then
            score = score + 300
        End If

        If numRows = 4 Then
            score = score + 1200
        End If

        lblScore.Text = "Score: " & score
    End Sub
    Function canMoveDown()
        If isEmpty(piecex(0), piecey(0) + 1) = True And isEmpty(piecex(1), piecey(1) + 1) = True And isEmpty(piecex(2), piecey(2) + 1) = True And isEmpty(piecex(3), piecey(3) + 1) = True Then
            Return True

        End If

        Return False
    End Function
    Function canMoveLeft()
        If isEmpty(piecex(0) - 1, piecey(0)) = True And isEmpty(piecex(1) - 1, piecey(1)) = True And isEmpty(piecex(2) - 1, piecey(2)) = True And isEmpty(piecex(3) - 1, piecey(3)) = True Then
            Return True
        End If

        Return False
    End Function
    Function canMoveRight()
        If isEmpty(piecex(0) + 1, piecey(0)) = True And isEmpty(piecex(1) + 1, piecey(1)) = True And isEmpty(piecex(2) + 1, piecey(2)) = True And isEmpty(piecex(3) + 1, piecey(3)) = True Then
            Return True
        End If

        Return False
    End Function
    Function newPiece()
        piecetype = Math.Floor(Rnd() * 7) + 1

        If piecetype = 1 Then
            piecex(0) = 4
            piecey(0) = 0

            piecex(1) = 4
            piecey(1) = 1

            piecex(2) = 5
            piecey(2) = 0

            piecex(3) = 5
            piecey(3) = 1
        End If

        If piecetype = 2 Then
            piecex(2) = 3
            piecey(2) = 0

            piecex(1) = 4
            piecey(1) = 0

            piecex(0) = 5
            piecey(0) = 0

            piecex(3) = 6
            piecey(3) = 0
        End If

        If piecetype = 3 Then
            piecex(2) = 3
            piecey(2) = 0

            piecex(1) = 3
            piecey(1) = 1

            piecex(0) = 4
            piecey(0) = 1

            piecex(3) = 5
            piecey(3) = 1
        End If

        If piecetype = 4 Then
            piecex(2) = 3
            piecey(2) = 1

            piecex(1) = 5
            piecey(1) = 1

            piecex(0) = 4
            piecey(0) = 1

            piecex(3) = 5
            piecey(3) = 0
        End If

        If piecetype = 5 Then
            piecex(2) = 3
            piecey(2) = 1

            piecex(1) = 4
            piecey(1) = 1

            piecex(0) = 4
            piecey(0) = 0

            piecex(3) = 5
            piecey(3) = 0
        End If

        If piecetype = 6 Then
            piecex(1) = 3
            piecey(1) = 1

            piecex(0) = 4
            piecey(0) = 1

            piecex(2) = 4
            piecey(2) = 0

            piecex(3) = 5
            piecey(3) = 1
        End If

        If piecetype = 7 Then
            piecex(1) = 3
            piecey(1) = 0

            piecex(0) = 4
            piecey(0) = 0

            piecex(2) = 4
            piecey(2) = 1

            piecex(3) = 5
            piecey(3) = 1
        End If
        pieceposition = 0
    End Function
    Function rotate()

        If piecetype = 2 Then
            If pieceposition = 0 Then
                If isEmpty(piecex(0) + 1, piecey(0) - 1) And isEmpty(piecex(0) + 2, piecey(0) - 2) And isEmpty(piecex(0) - 1, piecey(0) + 1) Then
                    piecex(2) = piecex(2) + 2
                    piecey(2) = piecey(2) - 2

                    piecex(1) = piecex(1) + 1
                    piecey(1) = piecey(1) - 1

                    piecex(3) = piecex(3) - 1
                    piecey(3) = piecey(3) + 1

                    pieceposition = 1
                End If
            Else
                If isEmpty(piecex(0) - 1, piecey(0) + 1) And isEmpty(piecex(0) - 2, piecey(0) + 2) And isEmpty(piecex(0) + 1, piecey(0) - 1) Then
                    piecex(2) = piecex(2) - 2
                    piecey(2) = piecey(2) + 2

                    piecex(1) = piecex(1) - 1
                    piecey(1) = piecey(1) + 1

                    piecex(3) = piecex(3) + 1
                    piecey(3) = piecey(3) - 1

                    pieceposition = 0
                End If
            End If
        End If

        If piecetype = 3 Then
            If pieceposition = 0 Then
                If isEmpty(piecex(0), piecey(0) - 1) And isEmpty(piecex(0) + 1, piecey(0) - 1) And isEmpty(piecex(0), piecey(0) + 1) Then
                    piecex(2) = piecex(2) + 2
                    piecey(2) = piecey(2)

                    piecex(1) = piecex(1) + 1
                    piecey(1) = piecey(1) - 1

                    piecex(3) = piecex(3) - 1
                    piecey(3) = piecey(3) + 1

                    pieceposition = 1
                End If
            Else
                If pieceposition = 1 Then
                    If isEmpty(piecex(0) + 1, piecey(0) + 1) And isEmpty(piecex(0) + 1, piecey(0)) And isEmpty(piecex(0) - 1, piecey(0)) Then
                        piecex(2) = piecex(2)
                        piecey(2) = piecey(2) + 2

                        piecex(1) = piecex(1) + 1
                        piecey(1) = piecey(1) + 1

                        piecex(3) = piecex(3) - 1
                        piecey(3) = piecey(3) - 1

                        pieceposition = 2
                    End If
                Else
                    If pieceposition = 2 Then
                        If isEmpty(piecex(0), piecey(0) + 1) And isEmpty(piecex(0) - 1, piecey(0) + 1) And isEmpty(piecex(0), piecey(0) - 1) Then
                            piecex(2) = piecex(2) - 2
                            piecey(2) = piecey(2)

                            piecex(1) = piecex(1) - 1
                            piecey(1) = piecey(1) + 1

                            piecex(3) = piecex(3) + 1
                            piecey(3) = piecey(3) - 1

                            pieceposition = 3
                        End If
                    Else
                        If isEmpty(piecex(0) - 1, piecey(0) - 1) And isEmpty(piecex(0) - 1, piecey(0)) And isEmpty(piecex(0) + 1, piecey(0)) Then

                            If pieceposition = 3 Then
                                piecex(2) = piecex(2)
                                piecey(2) = piecey(2) - 2

                                piecex(1) = piecex(1) - 1
                                piecey(1) = piecey(1) - 1

                                piecex(3) = piecex(3) + 1
                                piecey(3) = piecey(3) + 1

                                pieceposition = 0
                            End If
                        End If
                    End If
                End If
            End If
        End If

        If piecetype = 4 Then
            If pieceposition = 0 Then
                If isEmpty(piecex(0), piecey(0) + 1) And isEmpty(piecex(0) + 1, piecey(0) + 1) And isEmpty(piecex(0), piecey(0) - 1) Then
                    piecex(3) = piecex(3)
                    piecey(3) = piecey(3) + 2

                    piecex(1) = piecex(1) - 1
                    piecey(1) = piecey(1) + 1

                    piecex(2) = piecex(2) + 1
                    piecey(2) = piecey(2) - 1

                    pieceposition = 1
                End If
            Else
                If pieceposition = 1 Then
                    If isEmpty(piecex(0) - 1, piecey(0)) And isEmpty(piecex(0) - 1, piecey(0) + 1) And isEmpty(piecex(0) + 1, piecey(0)) Then
                        piecex(3) = piecex(3) - 2
                        piecey(3) = piecey(3)

                        piecex(1) = piecex(1) - 1
                        piecey(1) = piecey(1) - 1

                        piecex(2) = piecex(2) + 1
                        piecey(2) = piecey(2) + 1

                        pieceposition = 2
                    End If
                Else
                    If pieceposition = 2 Then
                        If isEmpty(piecex(0), piecey(0) - 1) And isEmpty(piecex(0) - 1, piecey(0) - 1) And isEmpty(piecex(0), piecey(0) + 1) Then
                            piecex(3) = piecex(3)
                            piecey(3) = piecey(3) - 2

                            piecex(1) = piecex(1) + 1
                            piecey(1) = piecey(1) - 1

                            piecex(2) = piecex(2) - 1
                            piecey(2) = piecey(2) + 1

                            pieceposition = 3
                        End If
                    Else
                        If pieceposition = 3 Then
                            If isEmpty(piecex(0) + 1, piecey(0)) And isEmpty(piecex(0) + 1, piecey(0) - 1) And isEmpty(piecex(0) - 1, piecey(0)) Then
                                piecex(3) = piecex(3) + 2
                                piecey(3) = piecey(3)

                                piecex(1) = piecex(1) + 1
                                piecey(1) = piecey(1) + 1

                                piecex(2) = piecex(2) - 1
                                piecey(2) = piecey(2) - 1

                                pieceposition = 0
                            End If
                        End If
                    End If
                End If
            End If
        End If

        If piecetype = 5 Then
            If pieceposition = 0 Then
                If isEmpty(piecex(0) - 1, piecey(0)) And isEmpty(piecex(0) - 1, piecey(0) - 1) Then
                    piecex(2) = piecex(2)
                    piecey(2) = piecey(2) - 2

                    piecex(1) = piecex(1) - 1
                    piecey(1) = piecey(1) - 1

                    piecex(3) = piecex(3) - 1
                    piecey(3) = piecey(3) + 1

                    pieceposition = 1
                End If

            Else
                If pieceposition = 1 Then
                    If isEmpty(piecex(0) - 1, piecey(0) + 1) And isEmpty(piecex(0) + 1, piecey(0)) Then

                        piecex(2) = piecex(2)
                        piecey(2) = piecey(2) + 2

                        piecex(1) = piecex(1) + 1
                        piecey(1) = piecey(1) + 1

                        piecex(3) = piecex(3) + 1
                        piecey(3) = piecey(3) - 1


                        pieceposition = 0
                    End If
                End If
            End If
        End If

        If piecetype = 6 Then
            If pieceposition = 0 Then
                If isEmpty(piecex(0), piecey(0) + 1) Then
                    piecex(1) = piecex(1) + 1
                    piecey(1) = piecey(1) - 1

                    piecex(2) = piecex(2) + 1
                    piecey(2) = piecey(2) + 1

                    piecex(3) = piecex(3) - 1
                    piecey(3) = piecey(3) + 1

                    pieceposition = 1
                End If
            Else
                If pieceposition = 1 Then
                    If isEmpty(piecex(0) - 1, piecey(0)) Then
                        piecex(1) = piecex(1) + 1
                        piecey(1) = piecey(1) + 1

                        piecex(2) = piecex(2) - 1
                        piecey(2) = piecey(2) + 1

                        piecex(3) = piecex(3) - 1
                        piecey(3) = piecey(3) - 1

                        pieceposition = 2
                    End If
                Else
                    If pieceposition = 2 Then
                        If isEmpty(piecex(0), piecey(0) - 1) Then
                            piecex(1) = piecex(1) - 1
                            piecey(1) = piecey(1) + 1

                            piecex(2) = piecex(2) - 1
                            piecey(2) = piecey(2) - 1

                            piecex(3) = piecex(3) + 1
                            piecey(3) = piecey(3) - 1

                            pieceposition = 3
                        End If
                    Else
                        If pieceposition = 3 Then
                            If isEmpty(piecex(0) + 1, piecey(0)) Then
                                piecex(1) = piecex(1) - 1
                                piecey(1) = piecey(1) - 1

                                piecex(2) = piecex(2) + 1
                                piecey(2) = piecey(2) - 1

                                piecex(3) = piecex(3) + 1
                                piecey(3) = piecey(3) + 1

                                pieceposition = 0
                            End If
                        End If
                    End If

                End If
            End If
        End If

        If piecetype = 7 Then
            If pieceposition = 0 Then
                If isEmpty(piecex(0) - 1, piecey(0) + 1) And isEmpty(piecex(0), piecey(0) - 1) Then
                    piecex(1) = piecex(1) + 1
                    piecey(1) = piecey(1) - 1

                    piecex(2) = piecex(2) - 1
                    piecey(2) = piecey(2) - 1

                    piecex(3) = piecex(3) - 2
                    piecey(3) = piecey(3)

                    pieceposition = 1
                End If
            Else
                If pieceposition = 1 Then
                    If isEmpty(piecex(0) + 1, piecey(0)) And isEmpty(piecex(0) - 1, piecey(0) - 1) Then
                        piecex(1) = piecex(1) - 1
                        piecey(1) = piecey(1) + 1

                        piecex(2) = piecex(2) + 1
                        piecey(2) = piecey(2) + 1

                        piecex(3) = piecex(3) + 2
                        piecey(3) = piecey(3)

                        pieceposition = 0
                    End If
                End If
            End If
        End If

    End Function
End Class
