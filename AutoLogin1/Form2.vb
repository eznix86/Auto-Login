Imports System.IO
Public Class Form2
    Dim drag As Boolean
    Dim mousex As Integer
    Dim mousey As Integer
    Dim NoNeed As String
    Dim save As String

    Private Sub Form2_MouseDown(ByVal sender As Object, ByVal e As System.Windows.Forms.MouseEventArgs) Handles Me.MouseDown
        drag = True
        mousex = Cursor.Position.X - Me.Left
        mousey = Cursor.Position.Y - Me.Top
    End Sub
    Private Sub Form2_MouseMove(ByVal sender As Object, ByVal e As System.Windows.Forms.MouseEventArgs) Handles Me.MouseMove
        If drag Then
            Me.Top = Cursor.Position.Y - mousey
            Me.Left = Cursor.Position.X - mousex
        End If
    End Sub

    Private Sub Form2_MouseUp(ByVal sender As Object, ByVal e As System.Windows.Forms.MouseEventArgs) Handles Me.MouseUp
        drag = False
    End Sub

    Private Sub Button2_Click(sender As Object, e As EventArgs) Handles Button2.Click
        Me.Close()

    End Sub

    Private Sub Button1_Click(sender As Object, e As EventArgs) Handles Button1.Click

        If TextBox1.TextLength = 0 Or TextBox2.TextLength = 0 Then
            MessageBox.Show("Please Enter Your Username or Password Correctly")

            Exit Sub
        End If

        Dim bw As BinaryWriter



        bw = New BinaryWriter(New FileStream("mydata.myt", FileMode.Create))

        'writing into the file

        bw.Write(TextBox1.Text)
        bw.Write(TextBox2.Text)
        If CheckBox1.CheckState = 1 Then
            bw.Write("true")
        Else
            bw.Write("false")
        End If


        bw.Close()

        'Form1.Opacity = 0

        'Do Until Form1.Opacity = 100
        '    Form1.Opacity += 1
        'Loop
        'Form1.Show()
        Timer1.Start()

    End Sub

    Private Sub Timer1_Tick(sender As Object, e As EventArgs) Handles Timer1.Tick

        Me.Opacity = Me.Opacity - 0.15


        If Me.Opacity <= 0 Then

            Form1.Show()
            Form1.Opacity = Form1.Opacity + 0.25
            If Form1.Opacity = 1 Then
                Timer1.Stop()
                Me.Hide()
            End If
        End If

    End Sub

    Private Sub Form2_Load(sender As Object, e As EventArgs) Handles MyBase.Load
        Form1.Opacity = 0
        Dim br As BinaryReader
        If Not File.Exists("mydata.myt") Then
            Exit Sub
        End If
        br = New BinaryReader(New FileStream("mydata.myt", FileMode.Open))

        NoNeed = br.ReadString
        NoNeed = br.ReadString
        save = br.ReadString

        br.Close()

        If save = "true" Then
            Me.Opacity = 0
            Timer1.Start()
        End If
    End Sub

End Class