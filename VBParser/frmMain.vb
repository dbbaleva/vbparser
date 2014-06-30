Imports System
Imports System.IO

Public Class frmMain

    Private borrowers_List As List(Of Borrower)
    Private dataContext As New DataContext()

    Private Sub btnBrowse_Click(sender As Object, e As EventArgs) Handles btnBrowse.Click
        Dim dlg As OpenFileDialog = New OpenFileDialog()

        dlg.Filter = "Comma delimited files (*.csv) | *.csv"
        If dlg.ShowDialog() = Windows.Forms.DialogResult.OK Then
            Dim stream As StreamReader = New StreamReader(dlg.FileName)
            Dim line As String

            txtFileName.Text = dlg.FileName
            borrowers_List = New List(Of Borrower)

            Do
                line = stream.ReadLine()
                Parse(line)
            Loop Until line Is Nothing

            stream.Close()
            btnSave.Enabled = True
        End If
    End Sub



    Private Sub Parse(line As String)
        Dim temp() As String
        Dim borrower As Borrower

        If line Is Nothing Then Return

        Try

            temp = line.Split(",".ToCharArray())

            If temp.Any() Then
                borrower = New Borrower()

                With borrower
                    .ID = temp(BorrowerFileStructure.BorrowererID)
                    .FullName = temp(BorrowerFileStructure.BorrowerName)
                    .AppraisalValue = Decimal.Parse(temp(BorrowerFileStructure.Appraisal))
                End With

                borrowers_List.Add(borrower)
            End If

        Catch ex As Exception

        End Try
    End Sub

    Private Sub btnSave_Click(sender As Object, e As EventArgs) Handles btnSave.Click
        If borrowers_List.Any() Then

            dataContext.Borrowers.AddRange(borrowers_List)
            dataContext.SaveChanges()

            bindingSrc.DataSource = dataContext.Borrowers.ToList()
            btnSave.Enabled = False
        End If
    End Sub

    Private Sub frmMain_Load(sender As Object, e As EventArgs) Handles MyBase.Load
        bindingSrc.DataSource = dataContext.Borrowers.ToList()
    End Sub

    Private Sub btnSearch_Click(sender As Object, e As EventArgs) Handles btnSearch.Click

        bindingSrc.DataSource = (From borrower In dataContext.Borrowers
                      Where borrower.FullName.Contains(txtSearchBox.Text)
                      Select borrower).ToList()

    End Sub
End Class
