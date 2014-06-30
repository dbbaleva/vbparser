Imports System.Data.Entity

Public Class DataContext
    Inherits DbContext
    Public Property Borrowers As DbSet(Of Borrower)
End Class
