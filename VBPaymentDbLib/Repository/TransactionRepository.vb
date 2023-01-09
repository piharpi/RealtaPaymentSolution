Imports System.Data.SqlClient
Imports System.Security.Principal
Imports VBPaymentDbLib.Context
Imports VBPaymentDbLib.Model

Namespace Repository
    Public Class TransactionRepository
        Implements ITransactionRepository

        Private ReadOnly _context As IRepositoryContext
        Sub New(repoContext As IRepositoryContext)
            _context = repoContext
        End Sub

        Public Function FindAll(Optional limit As Integer = 10) As List(Of PaymentTransact) Implements ITransactionRepository.FindAll
            Dim transactions As New List(Of PaymentTransact)
            Dim query = "SELECT TOP (@limit) [patr_id]
                              ,[patr_trx_number]
                              ,[patr_debet]
                              ,[patr_credit]
                              ,[patr_type]
                              ,[patr_note]
                              ,[patr_modified_date]
                              ,[patr_order_number]
                              ,[patr_source_id]
                              ,[patr_target_id]
                              ,[patr_trx_number_ref]
                              ,[patr_user_id]
                         FROM [Payment].[payment_transaction]"

            Using conn As New SqlConnection With {.ConnectionString = _context.GetConnectionString}
                Using cmd As New SqlCommand With {.Connection = conn, .CommandText = query}
                    cmd.Parameters.AddWithValue("@limit", limit)

                    Try
                        conn.Open()
                        Dim reader = cmd.ExecuteReader()
                        While reader.Read()
                            transactions.Add(New PaymentTransact() With {
                                      .Id = reader.GetInt32(0),
                                      .TrxNumber = reader.GetString(1),
                                      .Debet = reader.GetDecimal(2),
                                      .Credit = reader.GetDecimal(3),
                                      .Type = reader.GetString(4),
                                      .Note = If(IsDBNull(reader(5)), "", reader.GetString(5)),
                                      .ModifiedDate = reader.GetDateTime(6),
                                      .OrderNumber = reader.GetString(7),
                                      .SourceId = reader.GetInt32(8),
                                      .TargetId = reader.GetInt32(9),
                                      .TrxNumberRef = reader.GetString(10),
                                      .UserId = reader.GetInt32(11)
                            })
                        End While
                        reader.Close()
                        conn.Close()
                    Catch ex As Exception
                        Console.WriteLine(ex.Message)
                    End Try

                End Using
            End Using

            Return transactions
        End Function

        Public Function FindById(id As Integer) As PaymentTransact Implements ITransactionRepository.FindById
            Dim transaction As New PaymentTransact()
            Dim query = "SELECT TOP 1 [patr_id]
                              ,[patr_trx_number]
                              ,[patr_debet]
                              ,[patr_credit]
                              ,[patr_type]
                              ,[patr_note]
                              ,[patr_modified_date]
                              ,[patr_order_number]
                              ,[patr_source_id]
                              ,[patr_target_id]
                              ,[patr_trx_number_ref]
                              ,[patr_user_id]
                         FROM [Payment].[payment_transaction]
                        WHERE [patr_id] = @id"

            Using conn As New SqlConnection With {.ConnectionString = _context.GetConnectionString}
                Using cmd As New SqlCommand With {.Connection = conn, .CommandText = query}
                    cmd.Parameters.AddWithValue("@id", id)

                    Try
                        conn.Open()
                        Dim reader = cmd.ExecuteReader()
                        If reader.HasRows Then
                            reader.Read()
                            transaction.Id = reader.GetInt32(0)
                            transaction.TrxNumber = reader.GetString(1)
                            transaction.Debet = reader.GetDecimal(2)
                            transaction.Credit = reader.GetDecimal(3)
                            transaction.Type = reader.GetString(4)
                            transaction.Note = If(IsDBNull(reader(5)), "", reader.GetString(5))
                            transaction.ModifiedDate = reader.GetDateTime(6)
                            transaction.OrderNumber = reader.GetString(7)
                            transaction.SourceId = reader.GetInt32(8)
                            transaction.TargetId = reader.GetInt32(9)
                            transaction.TrxNumberRef = reader.GetString(10)
                            transaction.UserId = reader.GetInt32(11)
                            reader.Close()
                        End If
                        conn.Close()
                    Catch ex As Exception
                        Console.WriteLine(ex.Message)
                    End Try
                End Using
            End Using

            Return transaction
        End Function


        'TODO : buat trxNumber generate otomatis
        Public Function Create(newTransaction As PaymentTransact) As PaymentTransact Implements ITransactionRepository.Create
            Dim transaction As New PaymentTransact()

            Dim query = "INSERT INTO [Payment].[payment_transaction]
                                    ([patr_trx_number]
                                    ,[patr_debet]
                                    ,[patr_credit]
                                    ,[patr_type]
                                    ,[patr_note]
                                    ,[patr_modified_date]
                                    ,[patr_order_number]
                                    ,[patr_source_id]
                                    ,[patr_target_id]
                                    ,[patr_trx_number_ref]
                                    ,[patr_user_id])
                                VALUES
                                    (@trxNumber
                                    ,@debet
                                    ,@credit
                                    ,@type
                                    ,@note
                                    ,GETDATE()
                                    ,@orderNumber
                                    ,@sourceId
                                    ,@targetId
                                    ,@trxNumberRef
                                    ,@userId);" &
                         "SELECT CAST(IDENT_CURRENT('Payment.payment_transaction') AS INT);"

            Using conn As New SqlConnection With {.ConnectionString = _context.GetConnectionString}
                Using cmd As New SqlCommand With {.Connection = conn, .CommandText = query}
                    cmd.Parameters.AddWithValue("@trxNumber", newTransaction.TrxNumber)
                    cmd.Parameters.AddWithValue("@debet", newTransaction.Debet)
                    cmd.Parameters.AddWithValue("@credit", newTransaction.Credit)
                    cmd.Parameters.AddWithValue("@type", newTransaction.Type)
                    cmd.Parameters.AddWithValue("@note", newTransaction.Note)
                    cmd.Parameters.AddWithValue("@orderNumber", newTransaction.OrderNumber)
                    cmd.Parameters.AddWithValue("@sourceId", newTransaction.SourceId)
                    cmd.Parameters.AddWithValue("@targetId", newTransaction.TargetId)
                    cmd.Parameters.AddWithValue("@trxNumberRef", newTransaction.TrxNumberRef)
                    cmd.Parameters.AddWithValue("@userId", newTransaction.UserId)

                    Try
                        conn.Open()
                        Dim trxId = Convert.ToInt32(cmd.ExecuteScalar())
                        transaction = FindById(trxId)
                        conn.Close()
                    Catch ex As Exception
                        Console.WriteLine(ex.Message)
                    End Try

                End Using
            End Using

            Return transaction
        End Function

        Public Function Update(id As Integer,
                               Optional trxNumber As String = Nothing,
                               Optional debet As Decimal = 0,
                               Optional credit As Decimal = 0,
                               Optional type As String = Nothing,
                               Optional note As String = Nothing,
                               Optional orderNumber As String = Nothing,
                               Optional sourceId As Integer = 0,
                               Optional targetId As Integer = 0,
                               Optional trxNumberRef As String = Nothing,
                               Optional userId As Integer = 0) As PaymentTransact Implements ITransactionRepository.Update

            Dim transaction As New PaymentTransact()
            Dim defaultValue = FindById(id)

            Dim query = "[Payment].[spUpdatePaymentTransaction] @id, @trxNumber, @debet, @credit, @type, @note, @orderNumber, @sourceId, @targetId, @trxNumberRef, @userId"

            trxNumber = If(trxNumber Is Nothing, defaultValue.TrxNumber, trxNumber)
            debet = If(debet.Equals(0), defaultValue.Debet, debet)
            credit = If(debet.Equals(0), defaultValue.Credit, credit)
            type = If(type Is Nothing, defaultValue.Type, type)
            note = If(note Is Nothing, defaultValue.Note, note)
            orderNumber = If(orderNumber Is Nothing, defaultValue.OrderNumber, orderNumber)
            sourceId = If(sourceId.Equals(0), defaultValue.SourceId, sourceId)
            targetId = If(targetId.Equals(0), defaultValue.TargetId, targetId)
            trxNumberRef = If(trxNumberRef Is Nothing, defaultValue.TrxNumberRef, trxNumberRef)
            userId = If(userId.Equals(0), defaultValue.UserId, userId)


            Using conn As New SqlConnection With {.ConnectionString = _context.GetConnectionString}
                Using cmd As New SqlCommand With {.Connection = conn, .CommandText = query}
                    cmd.Parameters.AddWithValue("@id", id)
                    cmd.Parameters.AddWithValue("@trxNumber", trxNumber)
                    cmd.Parameters.AddWithValue("@debet", debet)
                    cmd.Parameters.AddWithValue("@credit", credit)
                    cmd.Parameters.AddWithValue("@type", type)
                    cmd.Parameters.AddWithValue("@note", note)
                    cmd.Parameters.AddWithValue("@orderNumber", orderNumber)
                    cmd.Parameters.AddWithValue("@sourceId", sourceId)
                    cmd.Parameters.AddWithValue("@targetId", targetId)
                    cmd.Parameters.AddWithValue("@trxNumberRef", trxNumberRef)
                    cmd.Parameters.AddWithValue("@userId", userId)

                    Try
                        conn.Open()
                        If cmd.ExecuteNonQuery() Then
                            transaction = FindById(id)
                        End If
                        conn.Close()
                    Catch ex As Exception
                        Console.WriteLine(ex.Message)
                    End Try
                End Using
            End Using

            Return transaction
        End Function

        Public Function Delete(id As Integer) As Integer Implements ITransactionRepository.Delete
            Dim deleteRowAffected As Integer
            Dim isExistTrx = FindById(id)

            Dim query = "DELETE FROM [Payment].[payment_transaction] WHERE [patr_id] = @id"

            Using conn As New SqlConnection With {.ConnectionString = _context.GetConnectionString}
                Using cmd As New SqlCommand With {.Connection = conn, .CommandText = query}
                    cmd.Parameters.AddWithValue("@id", id)

                    Try
                        conn.Open()
                        If isExistTrx.Id.Equals(0) Then
                            Console.WriteLine("Id tidak ada di payment_transaction")
                        Else
                            deleteRowAffected = cmd.ExecuteNonQuery()
                        End If
                        conn.Close()
                    Catch ex As Exception
                        Console.WriteLine(ex.Message)
                    End Try

                End Using
            End Using

            Return deleteRowAffected
        End Function
    End Class
End Namespace