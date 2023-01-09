Namespace Model
    Public Class PaymentTransact
        Private _id As Integer
        Private _trxNumber As String
        Private _debet As Decimal
        Private _credit As Decimal
        Private _type As String
        Private _note As String
        Private _modifiedDate As Date
        Private _orderNumber As String
        Private _sourceId As Integer
        Private _targetId As Integer
        Private _trxNumberRef As String
        Private _userId As Integer

        Public Sub New()
        End Sub

        Public Sub New(id As Integer, trxNumber As String, debet As Decimal, credit As Decimal, type As String, note As String, modifiedDate As Date, orderNumber As String, sourceId As Integer, targetId As Integer, trxNumberRef As String, userId As Integer)
            _id = id
            _trxNumber = trxNumber
            _debet = debet
            _credit = credit
            _type = type
            _note = note
            _modifiedDate = modifiedDate
            _orderNumber = orderNumber
            _sourceId = sourceId
            _targetId = targetId
            _trxNumberRef = trxNumberRef
            _userId = userId
        End Sub

        Public Property Id As Integer
            Get
                Return _id
            End Get
            Set(value As Integer)
                _id = value
            End Set
        End Property

        Public Property TrxNumber As String
            Get
                Return _trxNumber
            End Get
            Set(value As String)
                _trxNumber = value
            End Set
        End Property

        Public Property Debet As Decimal
            Get
                Return _debet
            End Get
            Set(value As Decimal)
                _debet = value
            End Set
        End Property

        Public Property Credit As Decimal
            Get
                Return _credit
            End Get
            Set(value As Decimal)
                _credit = value
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

        Public Property Note As String
            Get
                Return _note
            End Get
            Set(value As String)
                _note = value
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

        Public Property OrderNumber As String
            Get
                Return _orderNumber
            End Get
            Set(value As String)
                _orderNumber = value
            End Set
        End Property

        Public Property SourceId As Integer
            Get
                Return _sourceId
            End Get
            Set(value As Integer)
                _sourceId = value
            End Set
        End Property

        Public Property TargetId As Integer
            Get
                Return _targetId
            End Get
            Set(value As Integer)
                _targetId = value
            End Set
        End Property

        Public Property TrxNumberRef As String
            Get
                Return _trxNumberRef
            End Get
            Set(value As String)
                _trxNumberRef = value
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

        Public Overrides Function ToString() As String
            Return $"Transaction: {Id}, userId: {UserId}, OrderNumber: {OrderNumber}, Type: {Type} Modified: {ModifiedDate}"
        End Function
    End Class

End Namespace