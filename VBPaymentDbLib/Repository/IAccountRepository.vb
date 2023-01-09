Imports VBPaymentDbLib.Model

Namespace Repository
    Public Interface IAccountRepository
        Function FindAll(Optional limit As Integer = 10) As List(Of Account)
        Function FindById(ByVal entityId As Integer, ByVal userId As Integer) As Account
        Function Create(newAccount As Account) As Account
        Function Update(ByVal entityId As Integer, ByVal userId As Integer,
                        Optional newEntityId As Integer = Nothing,
                        Optional newUserId As Integer = Nothing,
                        Optional accountNumber As String = Nothing,
                        Optional saldo As Decimal = Nothing,
                        Optional type As String = Nothing,
                        Optional expMonth As Byte = Nothing,
                        Optional expYear As Int16 = Nothing) As Account

        Function Delete(ByVal entityId As Integer, userId As Integer) As Integer
    End Interface
End Namespace