Imports VBPaymentDbLib.Context
Imports VBPaymentDbLib.Repository

Namespace Base
    Public Class RepositoryManager
        Implements IRepositoryManager

        Private ReadOnly _repoContext As IRepositoryContext

        Private _bankRepository As IBankRepository
        Private _paymentRepository As IPaymentRepository
        Private _accountRepository As IAccountRepository
        Private _transactionRepository As ITransactionRepository

        Public Sub New(repoContext As IRepositoryContext)
            _repoContext = repoContext
        End Sub

        Public ReadOnly Property Bank As IBankRepository Implements IRepositoryManager.Bank
            Get
                If _bankRepository Is Nothing Then
                    _bankRepository = New BankRepository(_repoContext)
                End If
                Return _bankRepository
            End Get
        End Property

        Public ReadOnly Property Payment As IPaymentRepository Implements IRepositoryManager.Payment
            Get
                If _paymentRepository Is Nothing Then
                    _paymentRepository = New PaymentRepository(_repoContext)
                End If
                Return _paymentRepository
            End Get
        End Property

        Private ReadOnly Property Account As IAccountRepository Implements IRepositoryManager.Account
            Get
                If _accountRepository Is Nothing Then
                    _accountRepository = New AccountRepository(_repoContext)
                End If
                Return _accountRepository
            End Get
        End Property

        Public ReadOnly Property Transaction As ITransactionRepository Implements IRepositoryManager.Transaction
            Get
                If _transactionRepository Is Nothing Then
                    _transactionRepository = New TransactionRepository(_repoContext)
                End If
                Return _transactionRepository
            End Get
        End Property
    End Class
End Namespace