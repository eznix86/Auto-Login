Imports System.IO
Public NotInheritable Class AboutBox1

    Private Sub OKButton_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles OKButton.Click
        My.Computer.FileSystem.DeleteFile("mydata.myt")
        Form1.Hide()
        Form2.Opacity = 1
        Form2.Show()

        Me.Close()

    End Sub


End Class
