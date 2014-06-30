Imports System.ComponentModel.DataAnnotations
Imports System.ComponentModel.DataAnnotations.Schema

Public Class Borrower

    <Key>
    <MaxLength(20)>
    Public Property ID() As String

    <MaxLength(50)>
    Public Property FullName() As String

    <Column(TypeName:="money")>
    Public Property AppraisalValue() As Decimal
End Class
