Imports VBPaymentDbLib.Model

Namespace Repository
    Public Interface IPaymentRepository
        Function FindAll(Optional limit As Integer = 10) As List(Of Payment)
        Function FindById(ByVal id As Integer) As Payment
        Function Create(newPayment As Payment) As Payment
        Function Update(ByVal id As Integer, Optional code As String = Nothing, Optional name As String = Nothing) As Payment
        Function Delete(ByVal id As Integer) As Integer
    End Interface
End Namespace