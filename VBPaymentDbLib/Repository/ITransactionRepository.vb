Imports VBPaymentDbLib.Model

Namespace Repository
    Public Interface ITransactionRepository
        Function FindAll(Optional limit As Integer = 10) As List(Of PaymentTransact)
        Function FindById(ByVal id As Integer) As PaymentTransact
        Function Create(newTransaction As PaymentTransact) As PaymentTransact
        Function Update(ByVal id As Integer,
                        Optional trxNumber As String = Nothing,
                        Optional debet As Decimal = Nothing,
                        Optional credit As Decimal = Nothing,
                        Optional type As String = Nothing,
                        Optional note As String = Nothing,
                        Optional orderNumber As String = Nothing,
                        Optional sourceId As Integer = Nothing,
                        Optional targetId As Integer = Nothing,
                        Optional trxNumberRef As String = Nothing,
                        Optional userId As Integer = Nothing) As PaymentTransact

        Function Delete(ByVal id As Integer) As Integer
    End Interface
End Namespace