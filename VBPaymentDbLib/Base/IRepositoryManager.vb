Imports VBPaymentDbLib.Repository

Namespace Base
    Public Interface IRepositoryManager
        ReadOnly Property Bank As IBankRepository
        ReadOnly Property Payment As IPaymentRepository
        ReadOnly Property Account As IAccountRepository
        ReadOnly Property Transaction As ITransactionRepository
    End Interface
End Namespace