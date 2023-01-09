Namespace Model
    Public Class Bank
        Private _entityId As Integer
        Private _code As String
        Private _name As String
        Private _modifiedDate As Date

        Public Sub New()
        End Sub

        Public Sub New(entityId As Integer, code As String, name As String, modifiedDate As Date)
            _entityId = entityId
            _code = code
            _name = name
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

        Public Property Code As String
            Get
                Return _code
            End Get
            Set(value As String)
                _code = value
            End Set
        End Property

        Public Property Name As String
            Get
                Return _name
            End Get
            Set(value As String)
                _name = value
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
            Return $"Bank : {EntityId}, code: {Code}, name: {Name}, modified: {ModifiedDate}"
        End Function
    End Class
End Namespace