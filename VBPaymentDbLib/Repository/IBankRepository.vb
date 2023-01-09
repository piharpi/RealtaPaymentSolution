Imports VBPaymentDbLib.Context
Imports VBPaymentDbLib.Model

Namespace Repository
    Public Interface IBankRepository
        Function FindAll(Optional limit As Integer = 10) As List(Of Bank)
        Function FindById(ByVal id As Integer) As Bank
        Function Create(newBank As Bank) As Bank
        Function Update(ByVal id As Integer, Optional code As String = Nothing, Optional name As String = Nothing) As Bank
        Function Delete(ByVal id As Integer) As Integer
    End Interface
End Namespace