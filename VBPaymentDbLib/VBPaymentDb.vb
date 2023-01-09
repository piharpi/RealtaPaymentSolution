Imports VBPaymentDbLib.Base
Imports VBPaymentDbLib.Context

Public Class VBPaymentDb
    Implements IVBPaymentDb

    Private ReadOnly _repoContext As IRepositoryContext
    Private _repoManager As IRepositoryManager

    Sub New(ByVal connString As String)
        If _repoContext Is Nothing Then
            _repoContext = New RepositoryContext(connString)
        End If
    End Sub

    Public ReadOnly Property RepositoryManager As IRepositoryManager Implements IVBPaymentDb.RepositoryManager
        Get
            If _repoManager Is Nothing Then
                _repoManager = New RepositoryManager(_repoContext)
            End If
            Return _repoManager
        End Get
    End Property
End Class