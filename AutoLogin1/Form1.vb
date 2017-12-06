Imports System.IO
Public Class Form1
    Dim Completed = False, nextPage = False, erreur = False
    Dim Pourcentage As Double
    Dim drag As Boolean
    Dim mousex As Integer
    Dim mousey As Integer
    Dim username As String = "myt/36421861-1"
    Dim password As String = "F7G9E"

    Private Sub read()
        Dim br As BinaryReader
        br = New BinaryReader(New FileStream("mydata.myt", FileMode.Open))

        username = br.ReadString
        password = br.ReadString
        br.Close()
    End Sub

    '***********************************************************************************
    Protected Overrides Sub OnLoad(ByVal e As System.EventArgs)
        Dim scr = Screen.FromPoint(Me.Location)
        Me.Location = New Point(scr.WorkingArea.Right - Me.Width - 10, scr.WorkingArea.Top + 20)
        MyBase.OnLoad(e)
    End Sub

    Private Sub Form2_Paint(sender As Object, e As PaintEventArgs) Handles Me.Paint
        DrawProgress(e.Graphics, New Rectangle(10, 10, 100, 100), Pourcentage)

    End Sub

    Private Sub DrawProgress(g As Graphics, rect As Rectangle, percentage As Single)
        'work out the angles for each arc
        Dim progressAngle = CSng(360 / 100 * percentage)
        Dim remainderAngle = 360 - progressAngle

        'create pens to use for the arcs
        Using progressPen As New Pen(Color.FromArgb(93, 82, 247), 5), remainderPen As New Pen(Color.FromArgb(50, 50, 50), 5)
            'set the smoothing to high quality for better output
            g.SmoothingMode = Drawing2D.SmoothingMode.AntiAlias
            'draw the blue and white arcs
            g.DrawArc(progressPen, rect, -90, progressAngle)
            g.DrawArc(remainderPen, rect, progressAngle - 90, remainderAngle)
        End Using

        'draw the text in the centre by working out how big it is and adjusting the co-ordinates accordingly
        Using fnt As New Font(Me.Font.FontFamily, 14)
            Dim text As String = percentage.ToString + "%"
            Dim textSize = g.MeasureString(text, fnt)
            Dim textPoint As New Point(CInt(rect.Left + (rect.Width / 2) - (textSize.Width / 2)), CInt(rect.Top + (rect.Height / 2) - (textSize.Height / 2)))
            'now we have all the values draw the text
            g.DrawString(text, fnt, Brushes.Black, textPoint)
        End Using
    End Sub

    Private Sub Form1_MouseDown(ByVal sender As Object, ByVal e As System.Windows.Forms.MouseEventArgs) Handles Me.MouseDown
        drag = True
        mousex = Cursor.Position.X - Me.Left
        mousey = Cursor.Position.Y - Me.Top
    End Sub
    Private Sub Form1_MouseMove(ByVal sender As Object, ByVal e As System.Windows.Forms.MouseEventArgs) Handles Me.MouseMove
        If drag Then
            Me.Top = Cursor.Position.Y - mousey
            Me.Left = Cursor.Position.X - mousex
        End If
    End Sub

    Private Sub Form1_MouseUp(ByVal sender As Object, ByVal e As System.Windows.Forms.MouseEventArgs) Handles Me.MouseUp
        drag = False
    End Sub



    '********************************************************
    Private Sub Form1_Load(sender As Object, e As EventArgs) Handles MyBase.Load
        WebBrowser1.Navigate("https://internetaccount.orange.mu/alepowsrc/L7ExSNbPC4sb6TPJDblCAkN0baRJxw3qqt9ErkZgoetbexguZOJ1K13kJjowRDi9zus9pCmpMedELy99QFKjgA/L7E59/JDb97/goebc;jsessionid=58F3400196FB0B6B5F8CEEAABCC3968D")
        Timer1.Start()
        Timer2.Start()
        Me.BackColor = Color.FromArgb(43, 41, 42)
        Label2.Text = "Loading"


    End Sub
    Private Sub AutoLoginF()

        Try
            read()

            WebBrowser1.Document.GetElementById("signInForm.username").SetAttribute("value", username)
            WebBrowser1.Document.GetElementById("signInForm.password").SetAttribute("value", password)
            WebBrowser1.Document.GetElementById("signInContainer:submit").InvokeMember("click")
            WebBrowser1.ScriptErrorsSuppressed = True

        Catch ex As Exception

        End Try

    End Sub

    Private Sub Button2_Click(sender As Object, e As EventArgs) Handles Button2.Click

        Me.Close()
    End Sub

    Private Sub Button1_Click(sender As Object, e As EventArgs) Handles Button1.Click
        AboutBox1.Show()

    End Sub

    'Private Sub Timer2_Tick(sender As Object, e As EventArgs) Handles Timer2.Tick
    '    If ProgressBar1.Value < ProgressBar1.Maximum Then
    '        ProgressBar1.Value = Pourcentage
    '    End If

    'End Sub

    Private Sub WebBrowser1_DocumentCompleted(sender As Object, e As WebBrowserDocumentCompletedEventArgs) Handles WebBrowser1.DocumentCompleted
        nextPage = True
        erreur = True

        If Completed = False Then
            AutoLoginF()
            Completed = True

        End If
        If WebBrowser1.DocumentText.Contains("feedbackPanelERROR") Then
            MessageBox.Show("Error, Please Check Your Credentials")
            Form2.Opacity = 1
            Form2.Show()
            Me.Close()
            Completed = False
            erreur = True
        Else
            erreur = False
        End If

        If WebBrowser1.DocumentText.Contains("Mb") Or WebBrowser1.DocumentText.Contains("GB") And WebBrowser1.DocumentText.Contains("Hello") Then
            Dim ending As String = ">"
            Dim Restant As String = ""

            Dim nombre As Integer = WebBrowser1.DocumentText.IndexOf("Mb") - 1
            Do Until ending = WebBrowser1.DocumentText(nombre).ToString

                Restant += WebBrowser1.DocumentText(nombre)
                nombre -= 1
            Loop
            If IsNumeric(StrReverse(Restant)) Then
                Label2.Text = Pourcentage.ToString + "MB"

                Pourcentage = (StrReverse(Restant) / 76800) * 100

            End If
        End If
    End Sub

    Private Sub Timer1_Tick(sender As Object, e As EventArgs) Handles Timer1.Tick
        If WebBrowser1.ReadyState = WebBrowser1.ReadyState.Complete And erreur = False And nextPage = True Then
            WebBrowser1.Stop()
            WebBrowser1.Navigate("https://internetaccount.orange.mu/alepowsrc/L7ExSNbPC4sb6TPJDblCAkN0baRJxw3qqt9ErkZgoetbexguZOJ1K13kJjowRDi9zus9pCmpMedELy99QFKjgA/L7E59/JDb97/goebc;jsessionid=58F3400196FB0B6B5F8CEEAABCC3968D")
            nextPage = False
            Completed = False
        End If
    End Sub
End Class
