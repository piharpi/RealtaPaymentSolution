Namespace Model
    Public Class Account
        Private _entityId As Integer
        Private _userId As Integer
        Private _accountNumber As String
        Private _saldo As Decimal
        Private _type As String
        Private _expMonth As Byte
        Private _expYear As Short
        Private _modifiedDate As Date

        Public Sub New()
        End Sub

        Public Sub New(entityId As Integer, userId As Integer, accountNumber As String, saldo As Decimal, type As String, expMonth As Byte, expYear As Short, modifiedDate As Date)
            _entityId = entityId
            _userId = userId
            _accountNumber = accountNumber
            _saldo = saldo
            _type = type
            _expMonth = expMonth
            _expYear = expYear
            _modifiedDate = modifiedDate
        End Sub

        Public Property EntityId As Integer
            Get
                Return _entityId
            End Get
            Set(value As Integer)
                _entityId = value
            End Set
        End Property

        Public Property UserId As Integer
            Get
                Return _userId
            End Get
            Set(value As Integer)
                _userId = value
            End Set
        End Property

        Public Property AccountNumber As String
            Get
                Return _accountNumber
            End Get
            Set(value As String)
                _accountNumber = value
            End Set
        End Property

        Public Property Saldo As Decimal
            Get
                Return _saldo
            End Get
            Set(value As Decimal)
                _saldo = value
            End Set
        End Property

        Public Property Type As String
            Get
                Return _type
            End Get
            Set(value As String)
                _type = value
            End Set
        End Property

        Public Property ExpMonth As Byte
            Get
                Return _expMonth
            End Get
            Set(value As Byte)
                _expMonth = value
            End Set
        End Property

        Public Property ExpYear As Short
            Get
                Return _expYear
            End Get
            Set(value As Short)
                _expYear = value
            End Set
        End Property

        Public Property ModifiedDate As Date
            Get
                Return _modifiedDate
            End Get
            Set(value As Date)
                _modifiedDate = value
            End Set
        End Property

        Public Overrides Function ToString() As String
            Return $"UserAccount : {EntityId}, userId: {UserId}, AccountNumber: {AccountNumber}, Saldo: {Saldo} Modified: {ModifiedDate}"
        End Function
    End Class
End Namespace